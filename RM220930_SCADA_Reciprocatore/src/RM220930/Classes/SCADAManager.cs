using CookComputing.XmlRpc;
using fairino;
using RM.src.RM220930.Classes.PLC;
using RM.src.RM220930.Classes.UiBinder;
using RM.src.RM220930.Forms.Plant;
using RMLib.Alarms;
using RMLib.DataAccess;
using RMLib.Logger;
using RMLib.PLC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static EasyModbus.ModbusServer;

namespace RM.src.RM220930.Classes
{
    /// <summary>
    /// Classe che contiene configurazione dello SCADA e i relativi task
    /// </summary>
    public class SCADAManager
    {
        #region Componenti Principali e Connessioni

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        /// <summary>
        /// Gestisce i task in background.
        /// </summary>
        public readonly static TaskManager taskManager;

        /// <summary>
        /// Oggetto per l'accesso ai dati del robot nel database.
        /// </summary>
        private static readonly DAOSqlite ComPLCDAO = new DAOSqlite();
        /// <summary>
        /// Configurazione della connessione al database.
        /// </summary>
        private static readonly SqliteConnectionConfiguration DatabaseConnection = new SqliteConnectionConfiguration();
        /// <summary>
        /// Stringa di connessione al database.
        /// </summary>
        private static readonly string ConnectionString = DatabaseConnection.GetConnectionString();

        #endregion

        #region Nomi tasks

        /// <summary>
        /// Nome del task high priority
        /// </summary>
        public static string TaskHighPriorityName = nameof(CheckHighPriority);
        /// <summary>
        /// Nome del task low priority
        /// </summary>
        public static string TaskLowPriorityName = nameof(CheckLowPriority);
        /// <summary>
        /// Nome del task auxiliary worker
        /// </summary>
        public static string TaskAuxiliaryWorkerName = nameof(AuxiliaryWorker);
        /// <summary>
        /// Nome del task plc com handler
        /// </summary>
        public static string TaskPlcComHandlerName = nameof(PlcComHandler);
        /// <summary>
        /// Nome del task check robot com
        /// </summary>
        public static string TaskCheckRobotConneciton = nameof(CheckRobotConnection);
        /// <summary>
        /// Nome del task application manager
        /// </summary>
        public static string TaskApplicationManager = nameof(ApplicationTaskManager);
        /// <summary>
        /// Nome del task safety manager
        /// </summary>
        public static string TaskSafetyManager = nameof(SafetyTaskManager);

        #endregion

        #region Tempi di Delay dei Task

        /// <summary>
        /// Periodo di refresh per il task a bassa priorità.
        /// </summary>
        private readonly static int lowPriorityRefreshPeriod = 300;

        /// <summary>
        /// Periodo di refresh per il task a bassa priorità.
        /// </summary>
        private readonly static int plcComhandlerRefreshPeriod = 20;

        #endregion

        #region Variabili di Stato per la Logica di Controllo

        /// <summary>
        /// Rappresenta lo stato precedente della connessione al PLC.
        /// </summary>
        private static bool prevIsPlcConnected = true;

        /// <summary>
        /// Dizionario di allarmi per evitare segnalazioni duplicate.
        /// </summary>
        private static readonly Dictionary<string, bool> allarmiSegnalati = new Dictionary<string, bool>();

        #endregion

        #region Eventi Pubblici

        /// <summary>
        /// Evento invocato quando viene generato un allarme.
        /// </summary>
        public static event EventHandler AllarmeGenerato;
        /// <summary>
        /// Evento invocato quando gli allarmi vengono resettati.
        /// </summary>
        public static event EventHandler AllarmeResettato;

        #endregion

        #region Gestori di Componenti e Form

        /// <summary>
        /// Riferimento alla pagina degli allarmi.
        /// </summary>
        public static FormAlarmPage formAlarmPage;

        #endregion

        #region Variabili comunicazione PLC

        public static bool Cmd_On_Axe = false;

        #endregion

        /// <summary>
        /// Costruttore
        /// </summary>
        static SCADAManager()
        {
            taskManager = new TaskManager();
            taskManager.StartTaskChecker();
        }

        #region Metodi della classe SCADAManager

        /// <summary>
        /// Metodo che avvia task SCADA
        /// </summary>
        /// <returns></returns>
        public static bool InitSCADA()
        {
            formAlarmPage = new FormAlarmPage();
            formAlarmPage.AlarmsCleared += RMLib_AlarmsCleared;

            _zState = new ZAxisState[numZ];
            _prevZState = new ZAxisState[numZ];

            for (int i = 0; i < numZ; i++)
            {
                _zState[i] = new ZAxisState();
                _prevZState[i] = new ZAxisState();
            }


            // Faccio partire i task
            taskManager.AddTask(TaskCheckRobotConneciton, CheckRobotConnection, TaskType.LongRunning, true);
            taskManager.AddTask(TaskHighPriorityName, CheckHighPriority, TaskType.LongRunning, true);
            taskManager.AddTask(TaskAuxiliaryWorkerName, AuxiliaryWorker, TaskType.LongRunning, true);
            taskManager.AddTask(TaskLowPriorityName, CheckLowPriority, TaskType.LongRunning, true);
            taskManager.AddTask(TaskApplicationManager, ApplicationTaskManager, TaskType.LongRunning, true);
            taskManager.AddTask(TaskPlcComHandlerName, PlcComHandler, TaskType.LongRunning, true);
            taskManager.AddTask(TaskSafetyManager, SafetyTaskManager, TaskType.LongRunning, true);

            taskManager.StartTask(TaskCheckRobotConneciton);
            taskManager.StartTask(TaskHighPriorityName);
            taskManager.StartTask(TaskAuxiliaryWorkerName);
            taskManager.StartTask(TaskLowPriorityName);
            taskManager.StartTask(TaskApplicationManager);
            taskManager.StartTask(TaskPlcComHandlerName);
            taskManager.StartTask(TaskSafetyManager);

            log.Info("Task di background dello SCADA avviati tramite TaskManager.");

            return true;
        }

        #endregion

        #region Task di servizio

        /// <summary>
        /// Thread a priorità bassa che gestisce allarmi robot e PLC
        /// </summary>
        private async static Task CheckLowPriority(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    CheckPLCConnection();

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
                log.Error($"[TASK] {TaskLowPriorityName}: {ex}");
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static async Task AuxiliaryWorker(CancellationToken token)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private async static Task CheckHighPriority(CancellationToken token)
        {
        }

        /// <summary>
        /// Gestisce aggiornamento variabili PLC
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async static Task PlcComHandler(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    UpdateVariablesFromPlcValues();
                    //SendVariablesValuesToPlc();

                    await Task.Delay(plcComhandlerRefreshPeriod, token);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"[TASK] {TaskLowPriorityName}: {ex}");
                throw;
            }
            finally
            {

            }
        }

        private static int numZ = 9;

        /// <summary>
        /// Contiene la lista di indicatori nella pagina monitor ciclo
        /// </summary>
        public static readonly List<BiStateButton> Z_ONOFF = new List<BiStateButton>();




        /// <summary>
        /// Contiene la lista di label che leggono l'attuale posizione dell'asse nella pagina monitor ciclo
        /// </summary>
        public static readonly List<UiLabel> z_actualPos = new List<UiLabel>();

        public static ZAxisState[] _zState;
        private static ZAxisState[] _prevZState;



        private static void UpdateVariablesFromPlcValues()
        {
            if (Z_ONOFF.Count == 0) return;

            for (int i = 0; i < numZ; i++)
            {
                // ===== READ PLC =====
                var cmdOn = Convert.ToBoolean(
                    PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_On_Axe}")
                );

                var actPos = Convert.ToSingle(
                    PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Read_Act_Pos}")
                );

                /*
                var errorCode = Convert.ToInt16(
                    PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Error_Code}")
                );
                */
                // ===== UPDATE STATE =====
                _zState[i].CmdOnAxe = cmdOn;
                _zState[i].ActPosition = actPos;
                //_zState[i].ErrorCode = errorCode;

                // ===== UI UPDATE (solo se cambia) =====
                if (_prevZState[i].CmdOnAxe != cmdOn)
                {
                    _prevZState[i].CmdOnAxe = cmdOn;
                    Z_ONOFF[i].ChangeStatusCustom();
                }

                if (_prevZState[i].ActPosition != actPos)
                {
                    _prevZState[i].ActPosition = actPos;
                    z_actualPos[i].Write(actPos.ToString());
                }
                /*
                if (_prevZState[i].ErrorCode != errorCode)
                {
                    _prevZState[i].ErrorCode = errorCode;
                    // aggiorna allarmi / colore / icona
                }
                */
            }
        }




        /// <summary>
        ///
        /// <returns></returns>
        private async static Task CheckRobotConnection(CancellationToken token)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private async static Task ApplicationTaskManager(CancellationToken token)
        {   
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private async static Task SafetyTaskManager(CancellationToken token)
        {

        }

        #endregion

        #region Metodi helper

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
        /// Metodo che aggiunge alla lista degli allarmi l'allarme
        /// </summary>
        /// <param name="e"></param>
        public static void OnAlarm(RobotAlarmsEventArgs e)
        {
            // Calcola il timestamp Unix in millisecondi
            long unixTimestamp = ((DateTimeOffset)Convert.ToDateTime(e.Timestamp)).ToUnixTimeMilliseconds();

            ComPLCDAO.SaveAlarm(ConnectionString, Convert.ToInt32(e.Id), e.Description,
                unixTimestamp.ToString(), e.Device, e.State);
            formAlarmPage.AddAlarmToList(e.Id, e.Description, e.Timestamp, e.Device, e.State);
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
        protected static void OnAllarmeGenerato(EventArgs e)
        {
            AllarmeGenerato?.Invoke(null, e);
        }

        /// <summary>
        /// Gestore dell'evento allarmi cancellati presente nella libreria RMLib.Alarms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void RMLib_AlarmsCleared(object sender, EventArgs e)
        {
            var criteria = new List<(string device, string description)>
            {
                ("", "PLC disconnesso. Il ciclo è stato terminato.")
            };

            bool isBlocking = formAlarmPage.IsBlockingAlarmPresent(criteria);

            if (isBlocking)
            {
                // Segnalo che non ci sono più allarmi bloccanti
                AlarmManager.blockingAlarm = false;
            }

            TriggerAllarmeResettato();

            // Reset degli allarmi segnalati
            foreach (var key in allarmiSegnalati.Keys.ToList())
            {
                allarmiSegnalati[key] = false;
            }

        }

        /// <summary>
        /// Trigget per avvisare che gli allarmi sono stati resettati
        /// </summary>
        public static void TriggerAllarmeResettato()
        {
            OnAllarmeResettato(EventArgs.Empty);
        }

        /// <summary>
        /// Generazione evento da allarmi resettati
        /// </summary>
        /// <param name="e"></param>
        protected static void OnAllarmeResettato(EventArgs e)
        {
            AllarmeResettato?.Invoke(null, e);
        }

        #endregion
    }
}
