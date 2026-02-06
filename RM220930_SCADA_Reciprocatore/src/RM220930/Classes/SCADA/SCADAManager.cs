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
using static System.Windows.Forms.AxHost;

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
        /// A true quando la UI si sta aggiornando
        /// </summary>
        public static bool isUIUpdating = false;

        /// <summary>
        /// Rappresenta lo stato precedente della connessione al PLC.
        /// </summary>
        private static bool prevIsPlcConnected = true;

        /// <summary>
        /// Dizionario di allarmi per evitare segnalazioni duplicate.
        /// </summary>
        private static readonly Dictionary<string, bool> allarmiSegnalati = new Dictionary<string, bool>();

        /// <summary>
        /// Numero di asse
        /// </summary>
        public static int axeOffset = 1;

        /// <summary>
        /// Numero precednete di asse
        /// </summary>
        public static int _prevAxeOffset = 99;

        /// <summary>
        /// Numero di assi
        /// </summary>
        public static int numZ = 7;

        #region Home page

        /// <summary>
        /// Contiene la lista di indicatori ON/OFF nella pagina monitor ciclo
        /// </summary>
        public static readonly List<BiStateButton> Z_ONOFF = new List<BiStateButton>();

        /// <summary>
        /// Contiene la lista di label che leggono l'attuale posizione dell'asse nella pagina monitor ciclo
        /// </summary>
        public static readonly List<UiLabel> z_actualPos = new List<UiLabel>();

        /// <summary>
        /// Tasto auto in home page
        /// </summary>
        public static BiStateButton autoMode = new BiStateButton();

        /// <summary>
        /// Tasto man in home page
        /// </summary>
        public static BiStateButton manMode = new BiStateButton();

        /// <summary>
        /// Tasto pos 0 in home page
        /// </summary>
        public static BiStateButton pos0Mode = new BiStateButton();

        /// <summary>
        /// Oggetto che rappresenta lo stato dell'asse
        /// </summary>
        public static ZAxisState[] _zState;

        /// <summary>
        /// Oggetto che rappresenta lo stato precedente dell'asse
        /// </summary>
        private static ZAxisState[] _prevZState;

        /// <summary>
        /// Contiene la lista di button degli assi
        /// </summary>
        public static readonly List<BiStateButton> selectedAxe_axis = new List<BiStateButton>();

        #endregion

        #region Work params

        /// <summary>
        /// Stato precedente per l'asse selezionato (WorkParams)
        /// </summary>
        public static ZAxisState _prevWorkParamsState = new ZAxisState();

        /// <summary>
        /// Stato precedente per l'asse selezionato (WorkParams)
        /// </summary>
        private static ZAxisState _prevAxeConfigurationState = new ZAxisState();

        /// <summary>
        /// Tasto ON-OFF dell'asse selzionato in workParams
        /// </summary>
        public static BiStateButton Z_ONOFF_workParams = new BiStateButton();

        /// <summary>
        /// Tasto Home dell'asse selezionato in workParams
        /// </summary>
        public static BiStateButton Z_Home_workParams = new BiStateButton();

        /// <summary>
        /// Tasto auto ON-OFF dell'asse selezionato in workParams
        /// </summary>
        public static BiStateButton Z_Auto_workParams = new BiStateButton();

        /// <summary>
        /// Etichetta con numero di asse selezionato
        /// </summary>
        public static UiLabel numAxe_workParams = new UiLabel();

        /// <summary>
        /// Velocità asse selezionato
        /// </summary>
        public static UiLabel speed_workParams = new UiLabel();

        /// <summary>
        /// Posizione no pezzo asse selezionato
        /// </summary>
        public static UiLabel posRange_workParams = new UiLabel();

        /// <summary>
        /// Distanza dal pezzo
        /// </summary>
        public static UiLabel offsetFromPiece_workParams = new UiLabel();

        /// <summary>
        /// Posizione Alta
        /// </summary>
        public static UiLabel posAlta_workParams = new UiLabel();

        /// <summary>
        /// Posizione Bassa
        /// </summary>
        public static UiLabel posBassa_workParams = new UiLabel();

        #endregion

        #region Axe configuration

        /// <summary>
        /// Tasto ON-OFF dell'asse selzionato in axeConfiguration
        /// </summary>
        public static BiStateButton Z_ONOFF_axeConfiguration = new BiStateButton();

        /// <summary>
        /// Home timeout
        /// </summary>
        public static UiLabel homeTimeout_axeConfiguration = new UiLabel();

        /// <summary>
        /// Vel min
        /// </summary>
        public static UiLabel velMin_axeConfiguration = new UiLabel();

        /// <summary>
        /// Vel max
        /// </summary>
        public static UiLabel velMax_axeConfiguration = new UiLabel();

        /// <summary>
        /// Accelerazione
        /// </summary>
        public static UiLabel acceleration_axeConfiguration = new UiLabel();

        /// <summary>
        /// Decelerazione
        /// </summary>
        public static UiLabel deceleration_axeConfiguration = new UiLabel();

        /// <summary>
        /// Pos min
        /// </summary>
        public static UiLabel posMin_axeConfiguration = new UiLabel();

        /// <summary>
        /// Pos max
        /// </summary>
        public static UiLabel posMax_axeConfiguration = new UiLabel();

        /// <summary>
        /// Pos stop
        /// </summary>
        public static UiLabel posStop_axeConfiguration = new UiLabel();

        /// <summary>
        /// Vel stop
        /// </summary>
        public static UiLabel velStop_axeConfiguration = new UiLabel();

        /// <summary>
        /// Offset base
        /// </summary>
        public static UiLabel offsetBase_axeConfiguration = new UiLabel();

        /// <summary>
        /// Distanza pistole
        /// </summary>
        public static UiLabel disPistole_axeConfiguration = new UiLabel();

        /// <summary>
        /// Pos lavaggio
        /// </summary>
        public static UiLabel posLavaggio_axeConfiguration = new UiLabel();

        /// <summary>
        /// Vel lavaggio
        /// </summary>
        public static UiLabel velLavaggio_axeConfiguration = new UiLabel();

        #endregion

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

            InitZAxisState();

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
        /// Inizializza gli oggetti che rappresentato lo stato degli assi
        /// </summary>
        private static void InitZAxisState()
        {
            _zState = new ZAxisState[numZ];
            _prevZState = new ZAxisState[numZ];

            for (int i = 0; i < numZ; i++)
            {
                _zState[i] = new ZAxisState();
                _prevZState[i] = new ZAxisState();
            }
        }

        public static bool? hmiVisManualMode;

        /// <summary>
        /// Aggiorna le variabili dal dizionario PLC
        /// </summary>
        private static void UpdateVariablesFromPlcValues()
        {
            if (Z_ONOFF.Count == 0) return;

            bool changed = false; // Segnala il cambiamento di una variabile

            for (int i = 0; i < numZ; i++)
            {
                // ===== READ PLC =====
                var cmdOn = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_On_Axe}"));
                var cmdEn = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_En_Axe}"));
                var readHomeOK = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Read_Home_Ok}"));
                var cmdAutoFromPc = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_AutoFrom_Pc}"));
                var actPos = Convert.ToSingle(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Read_Act_Pos}"));
                var cmdSpeedPos = Convert.ToSingle(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_Speed_Pos}"));
                var cmdPosRange = Convert.ToSingle(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_Pos_Range}"));
                var HMIVisAutomaticMode = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_{PLCTagName.Hmi_Vis_Automatic_Mode}"));
                var HMIVisManualMode = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_{PLCTagName.Hmi_Vis_Manual_Mode}"));
                var HMIVisPos0Mode = Convert.ToBoolean(PLCConfig.appVariables.getValue($"PLC1_{PLCTagName.Hmi_Vis_Pos_0}"));
                var cmdOffsetFromPiece = Convert.ToSingle(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_Offset_From_Piece}"));
                var cmdMaxPos = Convert.ToSingle(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_Max_Pos}"));
                var cmdMinPos = Convert.ToSingle(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_Min_Pos}"));
                var cmdHomeTimeout = Convert.ToInt32(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_Time_Home}"));
                var cmdVelMin = Convert.ToSingle(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_Min_Speed}"));
                var cmdVelMax = Convert.ToSingle(PLCConfig.appVariables.getValue($"PLC1_z{i}_{PLCTagName.Cmd_Max_Speed}"));


                // ===== UPDATE STATE =====
                _zState[i].CmdOnAxe = cmdOn;
                _zState[i].CmdEnAxe = cmdEn;
                _zState[i].ReadHomeOK = readHomeOK;
                _zState[i].CmdAutoFromPc = cmdAutoFromPc;
                _zState[i].ActPosition = actPos;
                _zState[i].cmdSpeedPos = cmdSpeedPos;
                _zState[i].CmdPosRange = cmdPosRange;
                _zState[i].CmdOffsetFromPiece = cmdOffsetFromPiece;
                _zState[i].CmdMaxPos = cmdMaxPos;
                _zState[i].CmdMinPos = cmdMinPos;
                _zState[i].CmdHomeTimeout = cmdHomeTimeout;
                _zState[i].CmdVelMin = cmdVelMin;
                _zState[i].CmdVelMax = cmdVelMax;

                #region UI HOME PAGE 

                changed = _prevZState[i].CmdOnAxe != cmdOn;
                if (changed)
                {
                    _prevZState[i].CmdOnAxe = cmdOn;
                    Z_ONOFF[i].ChangeStatusCustom(cmdOn);
                }

                changed = _prevZState[i].ActPosition != actPos;
                if (changed)
                {
                    _prevZState[i].ActPosition = actPos;
                    z_actualPos[i].Write(Math.Round(actPos, 1).ToString());
                }

                changed = autoMode.GetStatus() != HMIVisAutomaticMode;
                if (changed)
                {
                    autoMode.ChangeStatus(HMIVisAutomaticMode);
                }
                
                changed = manMode.GetStatus() != HMIVisManualMode;
                if (changed)
                {
                    manMode.ChangeStatus(HMIVisManualMode);
                }

                changed = pos0Mode.GetStatus() != HMIVisPos0Mode;
                if (changed)
                {
                    pos0Mode.ChangeStatus(HMIVisPos0Mode);
                }

                #endregion
            }

            #region UI WORKPARAMS

            int idx = SCADAManager.axeOffset;
            changed = _prevAxeOffset != idx;
            if (changed)
            {
                ResetPrevStates();
            }

           var state = _zState[idx];

            changed = _prevWorkParamsState.CmdOnAxe != state.CmdOnAxe;
            if (changed)
            {
                _prevWorkParamsState.CmdOnAxe = state.CmdOnAxe;
                Z_ONOFF_workParams.ChangeStatusCustom(state.CmdOnAxe);
            }

            changed = _prevWorkParamsState.ReadHomeOK != state.ReadHomeOK;
            if (changed)
            {
                _prevWorkParamsState.ReadHomeOK = state.ReadHomeOK;
                Z_Home_workParams.ChangeStatus(state.ReadHomeOK);
            }

            changed = _prevWorkParamsState.CmdAutoFromPc != state.CmdAutoFromPc;
            if (changed)
            {
                _prevWorkParamsState.CmdAutoFromPc = state.CmdAutoFromPc;
                Z_Auto_workParams.ChangeStatus(state.CmdAutoFromPc);
            }

            changed = _prevWorkParamsState.cmdSpeedPos != state.cmdSpeedPos;
            if (changed && !isUIUpdating)
            {
                _prevWorkParamsState.cmdSpeedPos = state.cmdSpeedPos;
                speed_workParams.Write(state.cmdSpeedPos.ToString());
            }

            changed = _prevWorkParamsState.CmdPosRange != state.CmdPosRange;
            if (changed && !isUIUpdating)
            {
                _prevWorkParamsState.CmdPosRange = state.CmdPosRange;
                posRange_workParams.Write(state.CmdPosRange.ToString());
            }

            changed = _prevWorkParamsState.CmdOffsetFromPiece != state.CmdOffsetFromPiece;
            if (changed && !isUIUpdating)
            {
                _prevWorkParamsState.CmdOffsetFromPiece = state.CmdOffsetFromPiece;
                offsetFromPiece_workParams.Write(state.CmdOffsetFromPiece.ToString());
            }

            changed = _prevWorkParamsState.CmdMaxPos != state.CmdMaxPos;
            if (changed && !isUIUpdating)
            {
                _prevWorkParamsState.CmdMaxPos = state.CmdMaxPos;
                posAlta_workParams.Write(state.CmdMaxPos.ToString());
            }

            changed = _prevWorkParamsState.CmdMinPos != state.CmdMinPos;
            if (changed && !isUIUpdating)
            {
                _prevWorkParamsState.CmdMinPos = state.CmdMinPos;
                posBassa_workParams.Write(state.CmdMinPos.ToString());
            }

            changed = _prevAxeOffset != idx;
            if (changed)
            {
                
                _prevAxeOffset = idx;
                numAxe_workParams.Write(idx.ToString());
                foreach (var axe in selectedAxe_axis)
                    axe.ChangeStatus(false);

                selectedAxe_axis[idx].ChangeStatus(true);
            }

            #endregion

            #region UI AXE_CONFIGURATION 

            changed = _prevAxeConfigurationState.CmdEnAxe != state.CmdEnAxe;
            if (changed)
            {
                _prevAxeConfigurationState.CmdEnAxe = state.CmdEnAxe;
                Z_ONOFF_axeConfiguration.ChangeStatusCustom(state.CmdEnAxe);
            }

            changed = _prevAxeConfigurationState.CmdHomeTimeout != state.CmdHomeTimeout;
            if (changed && !isUIUpdating)
            {
                _prevAxeConfigurationState.CmdHomeTimeout = state.CmdHomeTimeout;
                homeTimeout_axeConfiguration.Write(state.CmdHomeTimeout.ToString());
            }

            changed = _prevAxeConfigurationState.CmdVelMin != state.CmdVelMin;
            if (changed && !isUIUpdating)
            {
                _prevAxeConfigurationState.CmdVelMin = state.CmdVelMin;
                velMin_axeConfiguration.Write(state.CmdVelMin.ToString());
            }

            changed = _prevAxeConfigurationState.CmdVelMax != state.CmdVelMax;
            if (changed && !isUIUpdating)
            {
                _prevAxeConfigurationState.CmdVelMax = state.CmdVelMax;
                velMax_axeConfiguration.Write(state.CmdVelMax.ToString());
            }

            #endregion

            // ===== VISIBILITÀ Z_Auto =====
            bool shouldBeVisible = idx != 0;
            if (Z_Auto_workParams._button.Visible != shouldBeVisible)
                Z_Auto_workParams.ChangeVisibility(shouldBeVisible);
        }

        private static void ResetPrevStates()
        {
            _prevAxeConfigurationState = new ZAxisState();
            _prevWorkParamsState = new ZAxisState();

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
