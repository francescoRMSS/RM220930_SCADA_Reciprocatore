using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CookComputing.XmlRpc;
using fairino; // Namespace che contiene le tue struct e l'interfaccia ICallSupervisor

// Definiamo delle eccezioni personalizzate per una gestione degli errori più chiara
public class RobotConnectionException : Exception { public RobotConnectionException(string message, Exception inner) : base(message, inner) { } }
public class RobotCommandException : Exception { public RobotCommandException(string message) : base(message) { } }

public class FairinoRobotClient : IDisposable
{
    #region Campi Privati

    private readonly ICallSupervisor _proxy;
    private readonly string _robotIp;
    private CancellationTokenSource _internalCts;
    private Task _stateMonitoringTask;

    // Definiamo delle proprietà pubbliche per esporre lo stato in modo sicuro
    public bool IsConnected { get; private set; }
    public ROBOT_STATE_PKG CurrentState { get; private set; } // La struct che contiene tutti i dati di stato

    // Evento per notificare i cambiamenti di stato
    public event EventHandler<ROBOT_STATE_PKG> StateChanged;

    #endregion

    #region Costruttore e Ciclo di Vita

    public FairinoRobotClient(string ipAddress)
    {
        _robotIp = ipAddress;
        _proxy = XmlRpcProxyGen.Create<ICallSupervisor>();
        _proxy.Url = $"http://{_robotIp}:20003/RPC2";
        _proxy.Timeout = 5000; // Impostiamo un timeout ragionevole per le chiamate RPC
    }

    /// <summary>
    /// Si connette al robot in modo asincrono e avvia il monitoraggio dello stato.
    /// </summary>
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected) return;

        _internalCts = new CancellationTokenSource();

        try
        {
            // Eseguiamo la chiamata RPC semplice per verificare la connessione.
            await Task.Run(() =>
            {
                // 1. Chiamiamo il metodo proxy SENZA parametri, come definito nell'interfaccia.
                object[] result = _proxy.GetSoftwareVersion();

                // 2. Controlliamo il codice di errore restituito (solitamente il primo elemento).
                int errorCode = (int)result[0];
                if (errorCode != 0)
                {
                    // Se c'è un errore, lanciamo un'eccezione.
                    throw new RobotCommandException($"Impossibile ottenere la versione del software. Codice errore: {errorCode}");
                }

                // 3. (Opzionale) Se vuoi, puoi estrarre i dati e loggarli.
                string robotModel = (string)result[1];
                string webVersion = (string)result[2];
                string controllerVersion = (string)result[3];
                // Esempio: log.Info($"Connesso a Robot {robotModel}, WebApp: {webVersion}");

            }, cancellationToken);

            // Se la chiamata ha successo, avviamo il nostro task di monitoraggio
            _stateMonitoringTask = Task.Run(() => StateMonitoringLoop(_internalCts.Token), _internalCts.Token);

            IsConnected = true;
        }
        catch (Exception ex)
        {
            Dispose(); // Pulizia in caso di fallimento
            throw new RobotConnectionException($"Connessione al robot {_robotIp} fallita.", ex);
        }
    }

    /// <summary>
    /// Pulisce le risorse e ferma il monitoraggio.
    /// </summary>
    public void Dispose()
    {
        if (_internalCts != null && !_internalCts.IsCancellationRequested)
        {
            _internalCts.Cancel();
        }
        _stateMonitoringTask?.Wait(1000); // Aspetta un po' che il task finisca
        _internalCts?.Dispose();
        IsConnected = false;
    }

    #endregion

    #region Metodi RPC "Belli" (Esempi)
    /*
    /// <summary>
    /// Esegue un movimento MoveCart in modo asincrono.
    /// </summary>
    public async Task MoveCartAsync(DescPose pose, int tool, int user, float vel, float acc, CancellationToken cancellationToken = default)
    {
        if (!IsConnected) throw new InvalidOperationException("Robot non connesso.");

        await Task.Run(() =>
        {
            double[] rawPose = { pose.tran.x, pose.tran.y, pose.tran.z, pose.rpy.rx, pose.rpy.ry, pose.rpy.rz };
            int result = _proxy.MoveCart(rawPose, tool, user, vel, acc, 0, 0, -1); // I parametri mancanti vanno aggiunti
            if (result != 0)
            {
                throw new RobotCommandException($"Errore durante MoveCart: {result}");
            }
        }, cancellationToken);
    }

    /// <summary>
    /// Abilita o disabilita il robot.
    /// </summary>
    public async Task RobotEnableAsync(bool enable, CancellationToken cancellationToken = default)
    {
        if (!IsConnected) throw new InvalidOperationException("Robot non connesso.");

        await Task.Run(() =>
        {
            int result = _proxy.RobotEnable(enable ? 1 : 0);
            if (result != 0)
            {
                throw new RobotCommandException($"Errore durante RobotEnable: {result}");
            }
        }, cancellationToken);
    }

    // ... QUI AGGIUNGERAI GLI ALTRI METODI CHE TI SERVONO, SEGUENDO QUESTO PATTERN ...
    // Esempio: public async Task<DescPose> GetActualTCPPoseAsync(...)
    */
    #endregion

    #region Logica di Monitoraggio dello Stato (Il Cuore)

    /// <summary>
    /// Loop principale che si connette al socket di stato e parsa i dati.
    /// Sostituisce i 4 thread della libreria originale.
    /// </summary>
    private async Task StateMonitoringLoop(CancellationToken token)
    {
        // Buffer che persiste tra le letture per gestire pacchetti spezzettati
        var accumulatedBuffer = new List<byte>();

        while (!token.IsCancellationRequested)
        {
            try
            {
                using (var client = new TcpClient { NoDelay = true }) // NoDelay è utile per comunicazioni in tempo reale
                {
                    await client.ConnectAsync(_robotIp, 20004);

                    using (var stream = client.GetStream())
                    {
                        var readBuffer = new byte[4096]; // Buffer per ogni singola lettura

                        while (!token.IsCancellationRequested)
                        {
                            int bytesRead = await stream.ReadAsync(readBuffer, 0, readBuffer.Length, token);
                            if (bytesRead == 0) break; // Connessione chiusa dal server

                            // Aggiungiamo i byte appena letti al nostro buffer accumulato
                            accumulatedBuffer.AddRange(readBuffer.Take(bytesRead));

                            // Ora processiamo il buffer accumulato per cercare pacchetti completi
                            while (true)
                            {
                                var (packet, bytesConsumed) = FindAndParsePacket(accumulatedBuffer);

                                if (packet != null)
                                {
                                    // Abbiamo trovato un pacchetto valido!
                                    CurrentState = packet.Value;
                                    StateChanged?.Invoke(this, CurrentState); // Notifichiamo gli ascoltatori

                                    // Rimuoviamo i byte del pacchetto processato dal buffer
                                    accumulatedBuffer.RemoveRange(0, bytesConsumed);
                                }
                                else
                                {
                                    // Nessun pacchetto completo trovato, usciamo dal loop interno
                                    // e aspettiamo di ricevere altri dati.
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (OperationCanceledException) { break; } // Uscita pulita
            catch (Exception ex)
            {
                // log.Warn($"Errore nel monitoraggio: {ex.Message}. Riprovo tra 5s.");
                await Task.Delay(5000, token);
            }
        }
    }

    /// <summary>
    /// Metodo helper che cerca un pacchetto valido nel buffer, lo parsa e lo restituisce.
    /// </summary>
    /// <returns>Una tupla contenente il pacchetto (se trovato) e il numero di byte consumati dal buffer.</returns>
    private (ROBOT_STATE_PKG? packet, int bytesConsumed) FindAndParsePacket(List<byte> buffer)
    {
        const byte HEADER_BYTE = 0x5A; // Corrisponde a 'Z'

        // Cerchiamo l'inizio di un pacchetto (due byte 'Z')
        for (int i = 0; i < buffer.Count - 1; i++)
        {
            if (buffer[i] == HEADER_BYTE && buffer[i + 1] == HEADER_BYTE)
            {
                // Abbiamo trovato l'header. Controlliamo se abbiamo abbastanza dati per leggere la lunghezza.
                if (buffer.Count < i + 5)
                {
                    // Non abbiamo ancora abbastanza dati, ma potremmo averli alla prossima lettura.
                    // Rimuoviamo i byte spazzatura prima dell'header.
                    if (i > 0) buffer.RemoveRange(0, i);
                    return (null, 0);
                }

                // Estraiamo la lunghezza del payload. È un ushort (2 byte) in formato Little Endian.
                // i+3 è il byte basso, i+4 è il byte alto.
                int payloadLength = buffer[i + 3] | (buffer[i + 4] << 8);
                int totalPacketLength = 5 + payloadLength + 2; // 5 (header+len) + payload + 2 (checksum)

                // Controlliamo se abbiamo ricevuto l'intero pacchetto
                if (buffer.Count >= i + totalPacketLength)
                {
                    // Sì, abbiamo un pacchetto completo!
                    var packetBytes = buffer.GetRange(i, totalPacketLength).ToArray();

                    // Estraiamo il checksum dal pacchetto (ultimi 2 byte, Little Endian)
                    ushort receivedChecksum = (ushort)(packetBytes[totalPacketLength - 2] | (packetBytes[totalPacketLength - 1] << 8));

                    // Calcoliamo il checksum dei dati
                    ushort calculatedChecksum = 0;
                    for (int j = 0; j < totalPacketLength - 2; j++)
                    {
                        calculatedChecksum += packetBytes[j];
                    }

                    if (receivedChecksum == calculatedChecksum)
                    {
                        // Checksum CORRETTO! Possiamo parsare il pacchetto.
                        int structSize = Marshal.SizeOf(typeof(ROBOT_STATE_PKG));
                        IntPtr ptr = Marshal.AllocHGlobal(structSize);
                        try
                        {
                            // Copiamo i byte del pacchetto (escludendo il checksum) nel puntatore
                            Marshal.Copy(packetBytes, 0, ptr, structSize);
                            var parsedPacket = (ROBOT_STATE_PKG)Marshal.PtrToStructure(ptr, typeof(ROBOT_STATE_PKG));

                            // Rimuoviamo i byte spazzatura prima del pacchetto trovato
                            if (i > 0) buffer.RemoveRange(0, i);

                            return (parsedPacket, totalPacketLength);
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(ptr);
                        }
                    }
                    else
                    {
                        // Checksum fallito. Il pacchetto è corrotto.
                        // Saltiamo l'header e continuiamo a cercare dal byte successivo.
                        // log.Warn("Checksum del pacchetto di stato non valido. Pacchetto scartato.");
                        continue;
                    }
                }
                else
                {
                    // Non abbiamo ancora ricevuto l'intero pacchetto.
                    // Rimuoviamo i byte spazzatura prima dell'header che abbiamo trovato e aspettiamo altri dati.
                    if (i > 0) buffer.RemoveRange(0, i);
                    return (null, 0);
                }
            }
        }

        // Se abbiamo scansionato tutto il buffer e non abbiamo trovato un header,
        // possiamo scartare quasi tutto, tranne l'ultimo byte che potrebbe essere la prima 'Z' di un nuovo header.
        if (buffer.Count > 1)
        {
            buffer.RemoveRange(0, buffer.Count - 1);
        }

        return (null, 0);
    }

    #endregion
}