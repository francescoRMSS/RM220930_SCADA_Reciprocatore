using RMLib.Alarms;
using RMLib.DataAccess;
using RMLib.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RM.src.RM220930.Classes.PLC
{
    /// <summary>
    /// Classe che gestisce task di comunicazione con PLC
    /// </summary>
    public static class ComPLC
    {
        // <summary>
        /// Evento invocato quando gli allarmi vengono resettati.
        /// </summary>
        public static event EventHandler AllarmeResettato;

        /// <summary>
        /// Evento invocato quando viene generato un allarme.
        /// </summary>
        public static event EventHandler AllarmeGenerato;

        /// <summary>
        /// Dizionario di allarmi per evitare segnalazioni duplicate.
        /// </summary>
        public static readonly Dictionary<string, bool> allarmiSegnalati = new Dictionary<string, bool>();

        /// <summary>
        /// Delay di refresh del task
        /// </summary>
        const int lowPriorityRefreshPeriod = 600;

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        /// <summary>
        /// Rappresenta lo stato precedente della connessione al PLC.
        /// </summary>
        private static bool prevIsPlcConnected = true;

        /// <summary>
        /// Oggetto per l'accesso ai dati del robot nel database.
        /// </summary>
        private static readonly RobotDAOSqlite ComPLCDAO = new RobotDAOSqlite();
        /// <summary>
        /// Configurazione della connessione al database.
        /// </summary>
        private static readonly SqliteConnectionConfiguration DatabaseConnection = new SqliteConnectionConfiguration();
        /// <summary>
        /// Stringa di connessione al database.
        /// </summary>
        private static readonly string ConnectionString = DatabaseConnection.GetConnectionString();

        /// <summary>
        /// Thread a priorità bassa che gestisce comunicazione PLC
        /// </summary>
        public async static Task CheckLowPriority(CancellationToken token)
        {
            DateTime now = DateTime.Now;
            long unixTimestamp = ((DateTimeOffset)now).ToUnixTimeMilliseconds();
            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("dd-MM-yyyy HH:mm:ss");

            Dictionary<string, object> alarmValues = new Dictionary<string, object>();
            Dictionary<string, string> alarmDescriptions = new Dictionary<string, string>
                {
                    { "Safety NOK", "Ausiliari non pronti" },
                    { "Modbus robot error", "Errore comunicazione modbus robot" },
                    { "Robot Cycle Paused", "Ciclo robot in pausa" },
                    { "Error plates full", "Teglie piene" },
                    { "Check open gr.failed", "Controllo pinza aperta fallito" },
                    { "Check pos_Dx gr. failed", "Controllo pinza chiusa fallito" },
                    { "Robot fault present", "Errore robot" },
                    { "US_Dx_Error", "Errore ultrasuono" },
                    { "US_Dx_Enabled", "Ultrasuono abilitato" },
                    { "US_Dx_Started", "Ultrasuono avviato" },
                    { "US_Dx_Error_Disconnect", "Ultrasuono disconnesso" },
                    { "Errore_Drive_Destro", "Mancata presa pinza robot" },
                };

            try
            {
                while (!token.IsCancellationRequested)
                {
                    CheckPLCConnection();
                    GetPLCErrorCode(alarmValues, alarmDescriptions, now, unixTimestamp,
                        dateTime, formattedDate);

                    await Task.Delay(lowPriorityRefreshPeriod, token);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"[TASK] {nameof(CheckLowPriority)}: {ex}");
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Creazione di un allarme
        /// </summary>
        /// <param name="id">ID allarme</param>
        /// <param name="description">Descrizione allarme</param>
        /// <param name="timestamp">Timestamp allarme</param>
        /// <param name="device">Device da cui deriva l'allarme</param>
        /// <param name="state">ON-OFF</param>
        public static void CreateAlarm(string id, string description, string timestamp, string device, string state)
        {
            // Solleva l'evento quando il robot si ferma
            OnAlarm(new RobotAlarmsEventArgs(id, description, timestamp, device, state));
        }

        /// <summary>
        /// Check su errori comunicati da PLC
        /// </summary>
        /// <param name="alarmValues"></param>
        /// <param name="alarmDescriptions"></param>
        /// <param name="now"></param>
        /// <param name="unixTimestamp"></param>
        /// <param name="dateTime"></param>
        /// <param name="formattedDate"></param>
        private static void GetPLCErrorCode(Dictionary<string, object> alarmValues, Dictionary<string, string> alarmDescriptions,
        DateTime now, long unixTimestamp, DateTime dateTime, string formattedDate)
        {
            /*
            object alarmsPresent;

            lock (PLCConfig.appVariables)
            {
                alarmsPresent = PLCConfig.appVariables.getValue("PLC1_" + "Alarm present");

                if (Convert.ToBoolean(alarmsPresent))
                {
                    foreach (var key in alarmDescriptions.Keys)
                    {
                        alarmValues[key] = PLCConfig.appVariables.getValue("PLC1_" + key);
                    }
                }
            }
            */
            /*
            try
            {

                foreach (var key in alarmDescriptions.Keys)
                {
                    alarmValues[key] = PLCConfig.appVariables.getValue("PLC1_" + key);
                }

                // if (Convert.ToBoolean(alarmsPresent))
                // {
                now = DateTime.Now;
                unixTimestamp = ((DateTimeOffset)now).ToUnixTimeMilliseconds();
                dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).DateTime.ToLocalTime();
                formattedDate = dateTime.ToString("dd-MM-yyyy HH:mm:ss");

                foreach (var alarm in alarmValues)
                {
                    if (Convert.ToBoolean(alarm.Value) && !IsAlarmAlreadySignaled(alarm.Key))
                    {
                        string id = GenerateAlarmId(alarm.Key);
                        CreateRobotAlarm(id, alarmDescriptions[alarm.Key], formattedDate, "PLC", "ON");
                        MarkAlarmAsSignaled(alarm.Key);
                    }
                }
                // }
            }
            catch(Exception ex)
            {

            }
            */
        }

        /// <summary>
        /// Check su connessione PLC
        /// </summary>
        private static void CheckPLCConnection()
        {
            if (!AlarmManager.isPlcConnected) // Se il PLC è disconnesso
            {
                AlarmManager.blockingAlarm = true;
                string id = "0";
                string description = "PLC disconnesso. Il ciclo è stato terminato.";

                DateTime now = DateTime.Now;
                long unixTimestamp = ((DateTimeOffset)now).ToUnixTimeMilliseconds();
                DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTimestamp.ToString())).DateTime.ToLocalTime();
                string formattedDate = dateTime.ToString("dd-MM-yyyy HH:mm:ss");

                string device = "PLC";
                string state = "ON";

                if (!IsAlarmAlreadySignaled(id))
                {
                    CreateAlarm(id, description, formattedDate, device, state);
                    MarkAlarmAsSignaled(id);
                }

                prevIsPlcConnected = false;
            }
            else
            {
                if (!prevIsPlcConnected)
                {
      
                    AlarmManager.blockingAlarm = false;
                    prevIsPlcConnected = true;
                }
            }
        }

        /// <summary>
        /// Trigger attivato quando vengono cancellati gli allarmi
        /// </summary>
        public static void TriggerAllarmeResettato()
        {
            OnAllarmeResettato(EventArgs.Empty);
        }

        /// <summary>
        /// Generazione evento da allarmi resettati
        /// </summary>
        /// <param name="e"></param>
        public static void OnAllarmeResettato(EventArgs e)
        {
            AllarmeResettato?.Invoke(null, e);
        }
 
        /// <summary>
        /// Metodo che aggiunge alla lista degli allarmi l'allarme
        /// </summary>
        /// <param name="e"></param>
        public static void OnAlarm(RobotAlarmsEventArgs e)
        {
            // Calcola il timestamp Unix in millisecondi
            long unixTimestamp = ((DateTimeOffset)Convert.ToDateTime(e.Timestamp)).ToUnixTimeMilliseconds();

            ComPLCDAO.SaveRobotAlarm(ConnectionString, Convert.ToInt32(e.Id), e.Description,
                unixTimestamp.ToString(), e.Device, e.State);
            FormHomePage.formAlarmPage.AddAlarmToList(e.Id, e.Description, e.Timestamp, e.Device, e.State);
            TriggerAllarmeGenerato();

        }

        /// <summary>
        /// Generazione eventi
        /// </summary>
        public static void TriggerAllarmeGenerato()
        {
            OnAllarmeGenerato(EventArgs.Empty);
        }

        /// <summary>
        /// Generazione evento da allarme ricevuto
        /// </summary>
        /// <param name="e"></param>
        public static void OnAllarmeGenerato(EventArgs e)
        {
            AllarmeGenerato?.Invoke(null, e);
        }

        /// <summary>
        /// Avvisa se un allarme è già stato segnalato
        /// </summary>
        /// <param name="alarmKey"></param>
        /// <returns></returns>
        private static bool IsAlarmAlreadySignaled(string alarmKey)
        {
            return allarmiSegnalati.ContainsKey(alarmKey) && allarmiSegnalati[alarmKey];
        }

        /// <summary>
        /// Imposta l'allarme come segnalato
        /// </summary>
        /// <param name="alarmKey"></param>
        private static void MarkAlarmAsSignaled(string alarmKey)
        {
            if (allarmiSegnalati.ContainsKey(alarmKey))
            {
                allarmiSegnalati[alarmKey] = true;
            }
            else
            {
                allarmiSegnalati.Add(alarmKey, true);
            }
        }
    }
}
