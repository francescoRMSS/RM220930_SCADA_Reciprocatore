using fairino;
using System;
using System.Threading;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using RMLib.Logger;
using RMLib.DataAccess;
using RMLib.PLC;
using RMLib.Alarms;
using RM.src.RM220930.Forms.Plant;
using RM.Properties;
using RMLib.MessageBox;
using RM.src.RM220930.Classes.FR20.Applications.Application;
using RM.src.RM220930.Forms.DragMode;
using RM.src.RM220930.Classes.PLC;
using RM.src.RM220930.Classes.FR20.Jog;
using RM.src.RM220930.Forms;
using RM.src.RM220930.Forms.Plant.ViveTracker;
using CookComputing.XmlRpc;
using System.Windows.Forms;
using RMLib.Keyboards;
using RMLib.Security;
using static log4net.Appender.RollingFileAppender;
using static System.Windows.Forms.AxHost;
using static System.Net.Mime.MediaTypeNames;


namespace RM.src.RM220930
{
    /// <summary>
    /// Gestisce il robot in tutte le sue funzioni, la classe contiene solo riferimenti statici poichè il robot è unico 
    /// nell'impianto. Nel caso se ne dovessero aggiungere dei nuovi bisognerà rifare la classe in modo che ci sia un array
    /// di Robot e i metodi per accedere alle funzioni di un singolo robot alla volta. 
    /// <br>Il robot restituisce come feedback per ogni metodo interno alla sua libreria un codice di errore che può essere
    /// controllato al fine di gestire la pagina degli allarmi.</br>
    /// <br>Il robot apparentemente si muove di pochi mm perciò non sta mai del tutto fermo, per fare il controllo sul movimento
    /// è necessario aggiungere degli offset.</br>
    /// <br>La libreria fairino presenta problemi a gestire la sincronizzazione tra comando ed esecuzione, per questo motivo 
    /// è difficile sapere a quale posizione il robot si sta muovendo. Inoltre sembra che a volte il robot non si fermi subito 
    /// al comando Stop, proprio per via della coda di istruzioni inviate.</br>
    /// </summary>
    /// 
    public class RobotManager
    {
        #region Campi Statici e Proprietà

        #region Componenti Principali e Connessioni

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        /// <summary>
        /// Oggetto per l'accesso ai dati del robot nel database.
        /// </summary>
        private static readonly RobotDAOSqlite RobotDAO = new RobotDAOSqlite();
        /// <summary>
        /// Configurazione della connessione al database.
        /// </summary>
        private static readonly SqliteConnectionConfiguration DatabaseConnection = new SqliteConnectionConfiguration();
        /// <summary>
        /// Stringa di connessione al database.
        /// </summary>
        private static readonly string ConnectionString = DatabaseConnection.GetConnectionString();
        /// <summary>
        /// Oggetto di lock per garantire l'accesso thread-safe all'istanza del robot.
        /// </summary>
        private static readonly object _robotInstanceLock = new object();
        /// <summary>
        /// Campo privato contenente l'istanza del Robot dalla libreria fairino.
        /// </summary>
        private static Robot _robot;

        /// <summary>
        /// Proprietà pubblica e thread-safe per accedere all'istanza del Robot.
        /// </summary>
        public static Robot robot
        {
            get { lock (_robotInstanceLock) { return _robot; } }
            private set { lock (_robotInstanceLock) { _robot = value; } }
        }

        /// <summary>
        /// Gestisce i task in background.
        /// </summary>
        public readonly static TaskManager taskManager;

        /// <summary>
        /// IP statico assegnato al robot. Per modificarlo si deve usare il pannello dedicato.
        /// </summary>
        public static string RobotIpAddress = "192.168.2.70";

        #endregion

        #region Stato e Parametri del Robot

        /// <summary>
        /// Errore che restituisce il Robot.
        /// </summary>
        public static int err = 0;
        /// <summary>
        /// Codice principale errore Robot.
        /// </summary>
        public static int maincode = 0;
        /// <summary>
        /// Codice specifico errore Robot.
        /// </summary>
        public static int subcode = 0;
        /// <summary>
        /// Applicazione da far eseguire al Robot.
        /// </summary>
        public static string applicationName;
        /// <summary>
        /// Indica se la modalità del robot è al momento in automatica (true) o manuale (false).
        /// </summary>
        public static bool isAutomaticMode;
        /// <summary>
        /// Percentuale di velocità.
        /// </summary>
        public static int speed = 0;
        /// <summary>
        /// ID dello strumento in uso dal robot.
        /// </summary>
        public static int tool = 0;
        /// <summary>
        /// Utente che sta usando il robot.
        /// </summary>
        public static int user = 0;
        /// <summary>
        /// Carico massimo del robot in kg.
        /// </summary>
        public static int weight = 0;
        /// <summary>
        /// Percentuale di velocità.
        /// </summary>
        public static float vel = 0;
        /// <summary>
        /// Percentuale di accelerazione.
        /// </summary>
        public static float acc = 0;
        /// <summary>
        /// Fattore di scalatura di velocità.
        /// </summary>
        public static float ovl = 0;
        /// <summary>
        /// Valore che rappresenta la smoothness dei movimenti del robot (blend).
        /// </summary>
        public static float blendT = 0;
        /// <summary>
        /// Configurazione dello spazio giunto.
        /// </summary>
        public static int config = -1;
        /// <summary>
        /// Flag -> 0: blocking, 1: non_blocking.
        /// </summary>
        public static byte flag = 0;
        /// <summary>
        /// Estensione area di lavoro Robot.
        /// </summary>
        public static ExaxisPos ePos = new ExaxisPos(0, 0, 0, 0);
        /// <summary>
        /// Offset di posizione.
        /// </summary>
        public static DescPose offset = new DescPose();
        /// <summary>
        /// Frequenza registrazione punti in DragMode.
        /// </summary>
        public static int velRec = 500;
        /// <summary>
        /// Proprietà speed del Robot.
        /// </summary>
        public static int speedRobot = 30;
        /// <summary>
        /// Posizione TCP attuale del robot.
        /// </summary>
        public static DescPose TCPCurrentPosition = new DescPose(0, 0, 0, 0, 0, 0);
        /// <summary>
        /// Punto corrente precedente del Robot.
        /// </summary>
        public static DescPose previousTCPposition = new DescPose(0, 0, 0, 0, 0, 0);
        /// <summary>
        /// Raccoglie le proprietà del robot in un oggetto.
        /// </summary>
        public static RobotProperties robotProperties;
        /// <summary>
        /// Modalità operativa corrente.
        /// </summary>
        public static int mode = -1;
        /// <summary>
        /// Livello di collisione corrente.
        /// </summary>
        public static int currentCollisionLevel = 0;

        #endregion

        #region Gestori di Componenti e Form

        /// <summary>
        /// Riferimento alla pagina degli allarmi.
        /// </summary>
        public static FormAlarmPage formAlarmPage;
        /// <summary>
        /// Istanza form di diagnostica.
        /// </summary>
        public static FormDiagnostics formDiagnostics;
        /// <summary>
        /// Istanza user control per il Vive Tracker.
        /// </summary>
        public static UC_ViveTrackerPage viveTrackerPage;
        /// <summary>
        /// Istanza form calibrazione tracker.
        /// </summary>
        public static FormTrackerCalibration formTrackerCalibration = new FormTrackerCalibration();
        /// <summary>
        /// Oggetto che gestisce il tracker HTC VIVE.
        /// </summary>
        public static OpenVRsupport openVRsupport = new OpenVRsupport();
        /// <summary>
        /// Gestore dei frame del robot.
        /// </summary>
        private static Frames frameManager;
        /// <summary>
        /// Gestore dei tool del robot.
        /// </summary>
        private static Tools toolManager;
        /// <summary>
        /// Gestore delle collisioni del robot.
        /// </summary>
        private static Collisions collisionManager;

        #endregion

        #region Variabili di Stato per la Logica di Controllo

        // --- Stato connessione e allarmi ---
        /// <summary>
        /// Flag che indica un errore di connessione con il robot.
        /// </summary>
        private static readonly bool Connection_Robot_Error = false;
        /// <summary>
        /// Dizionario di allarmi per evitare segnalazioni duplicate.
        /// </summary>
        private static readonly Dictionary<string, bool> allarmiSegnalati = new Dictionary<string, bool>();
        /// <summary>
        /// Rappresenta lo stato precedente della connessione al PLC.
        /// </summary>
        private static bool prevIsPlcConnected = true;

        // --- Stato movimento e posizione ---
        /// <summary>
        /// A true quando il punto corrente del Robot si trova nel punto endingPoint passato come parametro.
        /// </summary>
        public static bool inPosition = false;
        /// <summary>
        /// Parametro da usare per eseguire inPosition.
        /// </summary>
        public static DescPose endingPoint = new DescPose(0, 0, 0, 0, 0, 0);
        /// <summary>
        /// Parametro da usare per eseguire in position sulla pose del tracker calcolata.
        /// </summary>
        public static DescPose trackerEndingPoint = new DescPose(0, 0, 0, 0, 0, 0);
        /// <summary>
        /// A true quando il robot si trova in safe zone.
        /// </summary>
        public static bool isInSafeZone = false;
        /// <summary>
        /// A true quando il robot si trova in home zone.
        /// </summary>
        public static bool isInHomePosition = false;
        /// <summary>
        /// Stato precedente di isInSafeZone per rilevare i cambiamenti.
        /// </summary>
        private static bool? prevIsInSafeZone = null;
        /// <summary>
        /// Variabile per memorizzare lo stato precedente di isInHomePosition.
        /// </summary>
        private static bool? previousIsInHomePosition = null;
        /// <summary>
        /// Timestamp di quando il robot ha iniziato a muoversi, per logica di debounce.
        /// </summary>
        static DateTime? robotMovingStartTime = null;

        // --- Stato ciclo applicazione ---
        /// <summary>
        /// A true quando viene terminata la routine del ciclo.
        /// </summary>
        public static bool stopCycleRoutine = false;
        /// <summary>
        /// A true quando si richiede lo stop del ciclo del Robot.
        /// </summary>
        public static bool stopCycleRequested = false;
        /// <summary>
        /// A true quando viene richiesta una pausa del ciclo.
        /// </summary>
        public static bool pauseCycleRequested = false;
        /// <summary>
        /// A true quando il ciclo deve riprendere da un punto precedente.
        /// </summary>
        public static bool riprendiCiclo = false;
        /// <summary>
        /// Indica l'indice del punto corrente nel ciclo.
        /// </summary>
        public static int currentIndex = -1;

        // --- Stato catena ---
        /// <summary>
        /// Contatore spostamento catena.
        /// </summary>
        public static int chainPos = 0;
        /// <summary>
        /// A true quando bisogna fermare l'updater del contatore catena.
        /// </summary>
        public static bool stopChainUpdaterThread = false;

        // --- Stato Abilitazione e Modalità ---
        /// <summary>
        /// Stato attuale isEnabled del robot.
        /// </summary>
        public static bool isEnabledNow = false;
        /// <summary>
        /// Stato precedente isEnable.
        /// </summary>
        private static bool prevIsEnable = false;
        /// <summary>
        /// Stato precedente isNotEnable.
        /// </summary>
        private static bool prevIsNotEnable = false;
        /// <summary>
        /// Stato precedente di robotReadyToStart.
        /// </summary>
        private static bool prevRobotReadyToStart = false;
        /// <summary>
        /// Stato precedente di robotHasProgramInMemory.
        /// </summary>
        private static bool prevRobotHasProgramInMemory = false;
        /// <summary>
        /// Rappresenta il valore della modalità automatica nello step precedente.
        /// </summary>
        private static bool prevIsAuto = false;
        /// <summary>
        /// Rappresenta il valore della modalità manuale nello step precedente.
        /// </summary>
        private static bool prevIsManual = false;
        /// <summary>
        /// Rappresenta il valore della modalità Off nello step precedente.
        /// </summary>
        private static bool prevIsOff = false;
        /// <summary>
        /// Modalità precedente letta dal PLC.
        /// </summary>
        private static int lastMode = -1;
        /// <summary>
        /// Timestamp dell'ultima modifica di modalità.
        /// </summary>
        private static DateTime lastModeChangeTime;
        /// <summary>
        /// Modalità stabile da impostare dopo il debounce.
        /// </summary>
        private static int stableMode = -1;
        /// <summary>
        /// Indica se la pagina drag mode è al momento attiva, in tal caso abilita la lettura dei comandi plc della teach mode
        /// </summary>
        public static bool isUcDragModeDisplayed = false;

        // --- Drag & Teach e Varie ---
        /// <summary>
        /// Lista di punti da visualizzare.
        /// </summary>
        public static List<PointPosition> positionsToSend = new List<PointPosition>();
        /// <summary>
        /// Punto da aggiungere alla lista di posizioni.
        /// </summary>
        public static PointPosition positionToSend = new PointPosition();
        /// <summary>
        /// Lista di punti da salvare.
        /// </summary>
        public static List<PointPosition> positionsToSave = new List<PointPosition>();
        /// <summary>
        /// Flag per avviare/fermare il recupero della posizione del tracker.
        /// </summary>
        public static bool getTrackerPosition = false;
        /// <summary>
        /// A true quando non si vuole eseguire retrieve delle coordinate dal tracker.
        /// </summary>
        public static bool stopFetchTrackerData = false;
        /// <summary>
        /// Callback per il timer di teaching.
        /// </summary>
        private static TimerCallback timerCallback;
        /// <summary>
        /// Timer usato per il teaching lineare.
        /// </summary>
        private static System.Threading.Timer timer;

        // --- Position Checker ---
        /// <summary>
        /// Oggetto usato per eseguire inPosition dei punti.
        /// </summary>
        private static PositionChecker checker_pos;
        /// <summary>
        /// Oggetto usato per controllare che un punto sia nella safeZone.
        /// </summary>
        private static PositionChecker checker_safeZone;
        /// <summary>
        /// Oggetto usato per controllare che la pose del tracker calcolata sia conforme con quella del robot pura.
        /// </summary>
        private readonly static PositionChecker checker_tracker = new PositionChecker(50.0);
        /// <summary>
        /// Checker utilizzato per la colorazione delle righe all'interna di lw_positions.
        /// </summary>
        private static readonly PositionChecker checker_monitoringPos = new PositionChecker(100.0);

        #endregion

        #region Tempi di Delay dei Task

        /// <summary>
        /// Periodo di refresh per il task ad alta priorità.
        /// </summary>
        private readonly static int highPriorityRefreshPeriod = 20;
        /// <summary>
        /// Periodo di refresh per il task degli ausiliari.
        /// </summary>
        private readonly static int auxiliaryThreadRefreshPeriod = 200;
        /// <summary>
        /// Periodo di refresh per il task di demo.
        /// </summary>
        private readonly static int demoThreadRefreshPeriod = 50;
        /// <summary>
        /// Periodo di refresh per il task a bassa priorità.
        /// </summary>
        private readonly static int lowPriorityRefreshPeriod = 200;
        /// <summary>
        /// Tempo di refresh all'interno del metodo CheckPosition del thread positionCheckerThread.
        /// </summary>
        private readonly static int positionCheckerThreadRefreshPeriod = 50;
        /// <summary>
        /// Periodo di refresh all'interno del metodo ApplicationTaskManager
        /// </summary>
        private readonly static int applicationTaskManagerRefreshPeriod = 100;

        #endregion

        #region Variabili Versioni Robot

        /// <summary>
        /// Versione SDK del controllore.
        /// </summary>
        public static string RobotSdkVer = "##########";
        /// <summary>
        /// Modello del robot.
        /// </summary>
        public static string RobotModelVer = "##########";
        /// <summary>
        /// Versione web.
        /// </summary>
        public static string RobotWebVer = "##########";
        /// <summary>
        /// Versione controller.
        /// </summary>
        public static string RobotControllerVer = "##########";
        /// <summary>
        /// Versione del firmware della control box.
        /// </summary>
        public static string RobotFwBoxBoardVer = "##########";
        /// <summary>
        /// Versione firmware driver 1.
        /// </summary>
        public static string RobotFwDriver1Ver = "##########";
        /// <summary>
        /// Versione firmware driver 2.
        /// </summary>
        public static string RobotFwDriver2Ver = "##########";
        /// <summary>
        /// Versione firmware driver 3.
        /// </summary>
        public static string RobotFwDriver3Ver = "##########";
        /// <summary>
        /// Versione firmware driver 4.
        /// </summary>
        public static string RobotFwDriver4Ver = "##########";
        /// <summary>
        /// Versione firmware driver 5.
        /// </summary>
        public static string RobotFwDriver5Ver = "##########";
        /// <summary>
        /// Versione firmware driver 6.
        /// </summary>
        public static string RobotFwDriver6Ver = "##########";
        /// <summary>
        /// Versione firmware della scheda end-effector.
        /// </summary>
        public static string RobotFwEndBoardVer = "##########";
        /// <summary>
        /// Versione hardware della control box.
        /// </summary>
        public static string RobotHwBoxBoardVer = "##########";
        /// <summary>
        /// Versione hardware driver 1.
        /// </summary>
        public static string RobotHwDriver1Ver = "##########";
        /// <summary>
        /// Versione hardware driver 2.
        /// </summary>
        public static string RobotHwDriver2Ver = "##########";
        /// <summary>
        /// Versione hardware driver 3.
        /// </summary>
        public static string RobotHwDriver3Ver = "##########";
        /// <summary>
        /// Versione hardware driver 4.
        /// </summary>
        public static string RobotHwDriver4Ver = "##########";
        /// <summary>
        /// Versione hardware driver 5.
        /// </summary>
        public static string RobotHwDriver5Ver = "##########";
        /// <summary>
        /// Versione hardware driver 6.
        /// </summary>
        public static string RobotHwDriver6Ver = "##########";
        /// <summary>
        /// Versione hardware della scheda end-effector.
        /// </summary>
        public static string RobotHwEndBoardVer = "##########";
        /// <summary>
        /// IP corrente del controllore.
        /// </summary>
        public static string RobotCurrentIP = "##########";

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
        /// <summary>
        /// Evento invocato al termine della routine per riabilitare i tasti per riavvio della routine.
        /// </summary>
        public static event EventHandler CycleRoutineStarted;
        /// <summary>
        /// Evento invocato dalla rpoutine go to home position
        /// </summary>
        public static event EventHandler HomeRoutineStarted;
        /// <summary>
        /// Evento invocato per riabilitare i tasti della modalità Drag.
        /// </summary>
        public static event EventHandler EnableDragModeButtons;
        /// <summary>
        /// Viene invocato quando si modifica la velocità del Robot.
        /// </summary>
        public static event EventHandler RobotVelocityChanged;
        /// <summary>
        /// Viene invocato quando si modifica la modalità del Robot.
        /// </summary>
        public static event EventHandler RobotModeChanged;
        /// <summary>
        /// Viene invocato quando si rileva che il robot si sta muovendo.
        /// </summary>
        public static event EventHandler RobotIsMoving;
        /// <summary>
        /// Evento scatenato quando il robot cambia posizione (per la colorazione della UI).
        /// </summary>
        public static event EventHandler RobotPositionChanged;
        /// <summary>
        /// Evento scatenato quando riparte il ciclo.
        /// </summary>
        public static event EventHandler CycleRestarted;
        /// <summary>
        /// Evento scatenato quando viene aggiunto un punto in modalità Drag.
        /// </summary>
        public static event EventHandler PointPositionAdded;
        /// <summary>
        /// Evento scatenato quando viene richiesto lo start della teach mode
        /// </summary>
        public static event EventHandler RequestedStartTeach;
        /// <summary>
        /// Evento scatenato quando viene richiesto lo stop della teach mode
        /// </summary>
        public static event EventHandler RequestedStopTeach;

        #endregion

        #region Struttura checker position

        /// <summary>
        /// A true quando il thread/task che controlla la posizione per la UI deve essere concluso.
        /// </summary>
        private static bool stopPositionCheckerThread = false;
        /// <summary>
        /// A true quando il punto attuale del robot corrisponde con la posizione interrogata della lw_positions.
        /// </summary>
        private static bool inPositionGun = false;
        /// <summary>
        /// Lista di posizioni su cui eseguire l'inPosition per gestire colorazione di lw_positions.
        /// </summary>
        private static List<KeyValuePair<int, DescPose>> positionsToCheck = new List<KeyValuePair<int, DescPose>>();
        /// <summary>
        /// Numero di posizioni su cui eseguire l'inPosition e la relativa colorazione su lw_positions.
        /// </summary>
        private static int numPositionsToCheck = 5;
        /// <summary>
        /// A true quando l'aggiornamento della lista di posizioni su cui eseguire inPosition 
        /// per gestire colorazione su lw_positions viene terminato.
        /// </summary>
        private static bool aggiornamentoFinito = false;
        /// <summary>
        /// Variabile per tracciare l'ultima posizione aggiornata nella ListView.
        /// </summary>
        private static int? lastUpdatedKey = null;

        #endregion

        #region Comandi da interfaccia (da rimuovere quando ci saranno su plc)

        /// <summary>
        /// A true quando routine di home avviata
        /// </summary>
        public static bool homeRoutineStarted = false;

        public static bool CmdStartCycle = false;
        public static bool CmdStopCycle = false;
        public static bool CmdPauseCycle = false;
        public static bool CmdHomeRoutine = false;
        public static bool CmdAddSpeed = false;
        public static bool CmdRemoveSpeed = false;
        public static int  CmdSetSpeed = -1;

        private static bool PrevCmdStartCycle = CmdStartCycle;
        private static bool PrevCmdStopCycle = CmdStopCycle;
        private static bool PrevCmdPauseCycle = CmdPauseCycle;
        private static bool PrevCmdHomeRoutine = CmdHomeRoutine;
        private static bool PrevCmdAddSpeed = CmdAddSpeed;
        private static bool PrevCmdRemoveSpeed = CmdRemoveSpeed;
        private static int  PrevCmdSetSpeed = CmdSetSpeed;

        /// <summary>
        /// Specifica se il robot è attualmente in drag mode o se la drag mode sta per partire
        /// </summary>
        private static bool isDragStart = false;
        private static bool isTeachStart = false;
        public static short dragMode = 0;

        public static bool francesco = false;

        #endregion

        #endregion

        #region Metodi della classe RobotManager

        /// <summary>
        /// Costruttore statico, chiamato dal programma in automatico all'inizio
        /// </summary>
        static RobotManager()
        {
            taskManager = new TaskManager();
            taskManager.StartTaskChecker();
        }

        /// <summary>
        /// Metodo che inizializza Robot e lo accende
        /// </summary>
        /// <param name="robotIpAddress">Indirizzo IP Robot</param>
        /// <returns></returns>
        public static bool InitRobot(string robotIpAddress)
        {
            formAlarmPage = new FormAlarmPage();
            formAlarmPage.AlarmsCleared += RMLib_AlarmsCleared;

            formDiagnostics = new FormDiagnostics();
            viveTrackerPage = new UC_ViveTrackerPage();

            // Collegamento evento ValueChanged del dizionario al metodo HandleDictionaryChange
            PLCConfig.appVariables.ValueChanged += RefreshVariables;

            RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16");

            // Istanzio il robot
            RobotIpAddress = robotIpAddress;
            robot = new Robot();
            robot.RPC(RobotIpAddress);
            //robot.ResumeMotion();
            AlarmManager.isRobotConnected = true;

            frameManager = new Frames(robot);
            toolManager = new Tools(robot);
            collisionManager = new Collisions(robot);

            taskManager.AddTask(nameof(CheckRobotConnection), CheckRobotConnection, TaskType.LongRunning, true);
            taskManager.AddTask(nameof(CheckHighPriority), CheckHighPriority, TaskType.LongRunning, true);
            taskManager.AddTask(nameof(AuxiliaryWorker), AuxiliaryWorker, TaskType.LongRunning, true);
            taskManager.AddTask(nameof(CheckLowPriority), CheckLowPriority, TaskType.LongRunning, true);
            taskManager.AddTask(nameof(ApplicationTaskManager), ApplicationTaskManager, TaskType.LongRunning, true);

            taskManager.StartTask(nameof(CheckRobotConnection));
            taskManager.StartTask(nameof(CheckHighPriority));
            taskManager.StartTask(nameof(AuxiliaryWorker));
            taskManager.StartTask(nameof(CheckLowPriority));
            taskManager.StartTask(nameof(ApplicationTaskManager));

            log.Info("Task di background del robot avviati tramite TaskManager.");

            GetRobotInfo();

            if (err == -4)
            {
                log.Error("RPC exception durante Init del Robot");
                return false;
            }

            robot.SetSpeed(speedRobot);
            robot.DragTeachSwitch(0);

            openVRsupport.Initialize();

            // Se fallisce setting della proprietà del Robot
            if (!SetRobotProperties())
                return false;

            return true;
        }

        #region Task di servizio

        /// <summary>
        /// Gestisce gli ausiliari del Robot
        /// </summary>
        private static async Task AuxiliaryWorker(CancellationToken token)
        {
            // Lista di aggiornamenti da inviare al PLC
            List<(string key, bool value, string type)> updates = new List<(string, bool, string)>();

            DataRow robotAlarm = null;

            bool allarmeSegnalato = false;

            string id = "";
            string description = "";
            string timestamp = "";
            string device = "";
            string state = "";

            DateTime now = DateTime.Now;
            long unixTimestamp = ((DateTimeOffset)now).ToUnixTimeMilliseconds();
            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("dd-MM-yyyy HH:mm:ss");

            JointPos jPos = new JointPos(0, 0, 0, 0, 0, 0);
            JointPos j1_actual_pos = new JointPos(0, 0, 0, 0, 0, 0);
            JointPos j2_actual_pos = new JointPos(0, 0, 0, 0, 0, 0);
            JointPos j3_actual_pos = new JointPos(0, 0, 0, 0, 0, 0);
            JointPos j4_actual_pos = new JointPos(0, 0, 0, 0, 0, 0);
            JointPos j5_actual_pos = new JointPos(0, 0, 0, 0, 0, 0);
            JointPos j6_actual_pos = new JointPos(0, 0, 0, 0, 0, 0);

            // Get del punto di home dal dizionario delle posizioni
            var pHome = ApplicationConfig.applicationsManager.GetPosition("pHome", "RM");
            // Creazione della DescPose del punto di Home
            DescPose homePose = new DescPose(pHome.x, pHome.y, pHome.z, pHome.rx, pHome.ry, pHome.rz);

            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (AlarmManager.isRobotConnected)
                    {
                        CheckIsRobotEnable();
                        CheckRobotMode();
                        CheckIsRobotReadyToStart();
                        CheckRobotHasProgramInMemory();

                        CheckIsRobotInHomePosition(homePose);

                        GetRobotErrorCode(updates, allarmeSegnalato, robotAlarm, now, id, description, timestamp,
                        device, state, unixTimestamp, dateTime, formattedDate);

                        SendRobotPositionToPLC(jPos, j1_actual_pos, j2_actual_pos, j3_actual_pos, j4_actual_pos, j5_actual_pos, j6_actual_pos);

                        //Gestione drag mode
                        GetDragCommands();
                        GetTeachCommands();
                    }

                    await Task.Delay(auxiliaryThreadRefreshPeriod, token);
                }

                token.ThrowIfCancellationRequested(); //Solleva eccezione per far andare in stop e non in completed
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"[TASK] {nameof(AuxiliaryWorker)}: {ex}");
                throw;
            }
            finally
            {

            }

        }

        /// <summary>
        /// Thread ad alta priorità che tiene monitorato movimento robot e zone di ingombro
        /// </summary>
        private async static Task CheckHighPriority(CancellationToken token)
        {
            try
            {
                // Lista di aggiornamenti da inviare al PLC
                List<(string key, bool value, string type)> updates = new List<(string, bool, string)>();

                #region Safe zone

                // Dichiarazione del punto di safe
                var pSafeZone = ApplicationConfig.applicationsManager.GetPosition("pSafeZone", "RM");

                DescPose pointSafeZone = new DescPose(pSafeZone.x, pSafeZone.y, pSafeZone.z, pSafeZone.rx, pSafeZone.ry, pSafeZone.rz);

                // Oggetto che rileva safe zone
                double delta_safeZone = 200.0;
                checker_safeZone = new PositionChecker(delta_safeZone);

                #endregion

                checker_pos = new PositionChecker(30.0);

                while (!token.IsCancellationRequested)
                {
                    if (robot != null && AlarmManager.isRobotConnected)
                    {
                        try
                        {
                            robot.GetActualTCPPose(flag, ref TCPCurrentPosition); // Leggo posizione robot TCP corrente
                            CheckIsRobotMoving(updates);
                            //CheckIsRobotInObstructionArea(startPoints, updates);
                            CheckIsRobotInSafeZone(pointSafeZone, updates);
                            CheckIsRobotInPos();
                        }
                        catch (Exception e)
                        {
                            log.Error("RobotManager: errore durante la valutazione delle variabili HIGH: " + e.Message);
                        }
                    }

                    updates.Clear(); //Svuoto la lista di aggiornamento

                    await Task.Delay(highPriorityRefreshPeriod, token);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"[TASK] {nameof(CheckHighPriority)}: {ex}");
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Thread a priorità bassa che gestisce allarmi robot e PLC
        /// </summary>
        private async static Task CheckLowPriority(CancellationToken token)
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
                    CheckChainPos();

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
        /// Task asincrono che controlla se il robot è connesso invocando un metodo direttamente tramite il proxy (controllore).
        /// Non vengono usati metodi della libreria poichè gestiti male e bloccanti, una chiamata al diretto interessato funziona meglio
        /// e viene fatta senza bloccare il task.
        /// Nel caso fosse rilevata la disconnessione allora viene chiamato closeRPC e il task continua a cercare la riconnessione.
        /// Quando la riconnessione arriva allora prova a re istanziare il robot.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async static Task CheckRobotConnection(CancellationToken token)
        {
            try
            {
                //Istanzio il proxy che interfaccia il controllore in maniera diretta
                ICallSupervisor connectionProxy = XmlRpcProxyGen.Create<ICallSupervisor>();
                connectionProxy.Url = $"http://{RobotIpAddress}:20003/RPC2";
                connectionProxy.Timeout = 500; // Timeout breve per non bloccare troppo a lungo.

                log.Warn("[Guardian] Task di controllo connessione avviato.");

                while (!token.IsCancellationRequested)
                {
                    bool isProxyConnected = false;
                    try
                    {
                        // --- IL CONTROLLO DIRETTO SUL PROXY ---
                        // Eseguiamo la chiamata RPC su un thread del pool per non bloccare
                        // il nostro loop while nel caso in cui il timeout non funzioni bene.
                        await Task.Run(() => connectionProxy.GetRobotErrorCode(), token);
                        isProxyConnected = true;
                    }
                    catch (Exception ex)
                    {
                        // Qualsiasi eccezione (XmlRpcException, WebException) significa che non siamo connessi.
                        isProxyConnected = false;
                        //log.Error($"[Guardian] Rilevata disconnessione: {ex.GetType().Name} - {ex.Message}");
                    }

                    if (isProxyConnected) //Connessione verificata
                    {
                        if (!AlarmManager.isRobotConnected)
                        {
                            // Eravamo in stato "disconnesso", ma ora la rete è tornata.
                            // Forziamo comunque una riconnessione per essere sicuri che la libreria sia sana.
                            log.Warn("[Guardian] Connessione al robot RISTABILITA.");

                            await AttemptReconnect(); // Tentiamo di ripristinare la libreria
                        }
                    }
                    else //Connessione assente
                    {
                        if (AlarmManager.isRobotConnected)
                        {
                            // È la prima volta che rileviamo la disconnessione
                            log.Error("[Guardian] Connessione al robot PERSA. Avvio tentativi di riconnessione...");
                            AlarmManager.isRobotConnected = false;
                            RefresherTask.AddUpdate(PLCTagName.Emergency, 1, "INT16");

                            try { robot.CloseRPC(); } catch { } //Chiusura dei thread di libreria
                        }
                    }

                    await Task.Delay(500, token);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"[TASK] {nameof(CheckRobotConnection)}: {ex}");
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Metodo helper che tenta di ricreare e riconnettere l'oggetto robot.
        /// Non è in un loop, viene chiamato dal Guardian quando necessario.
        /// </summary>
        private static async Task AttemptReconnect()
        {
            try
            {
                log.Warn("[Reconnect] Tentativo di ricreazione dell'oggetto Robot...");

                // 1. Ricrea l'oggetto da zero!
                robot = new Robot();

                // 2. Esegui di nuovo la procedura di connessione RPC e avvio dei thread di libreria
                int rpcResult = robot.RPC(RobotIpAddress);

                if (rpcResult == 0)
                {
                    // Diamo un secondo per stabilizzare
                    await Task.Delay(1000);

                    //await Task.Run(() => robot.ResumeMotion());
                    await Task.Run(() => robot.SetSpeed(speedRobot));

                    await Task.Run(() => robot.DragTeachSwitch(0));

                    log.Warn("[Reconnect] Oggetto Robot ricreato.");

                    // La prossima iterazione del Guardian lo confermerà.
                    AlarmManager.isRobotConnected = true;
                    RefresherTask.AddUpdate(PLCTagName.Emergency, 0, "INT16");

                    await Task.Run(() => GetRobotInfo());
                }
                else
                {
                    log.Warn("[Reconnect] Chiamata RPC fallita durante la riconnessione.");
                }
            }
            catch (Exception ex)
            {
                log.Error($"[Reconnect] Errore durante il tentativo di riconnessione: {ex.Message}");
            }
        }

        /// <summary>
        /// Esegue controlli su modalità robot, task in uso e sceglie quali task fermare/partire
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async static Task ApplicationTaskManager(CancellationToken token)
        {   
            try
            {
                while (!token.IsCancellationRequested)
                {
                    GetInterfaceCommands();
                    //SetRobotMode();
                    //ManageTasks();
                    await Task.Delay(applicationTaskManagerRefreshPeriod);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"[TASK] {nameof(ApplicationTaskManager)}: {ex}");
                throw;
            }
            finally
            {
                CmdStartCycle = false;
                CmdStopCycle = false;
                CmdPauseCycle = false;
                CmdHomeRoutine = false;
                CmdAddSpeed = false;
                CmdRemoveSpeed = false;
                CmdSetSpeed = -1;
                PrevCmdStartCycle = CmdStartCycle;
                PrevCmdStopCycle = CmdStopCycle;
                PrevCmdPauseCycle = CmdPauseCycle;
                PrevCmdHomeRoutine = CmdHomeRoutine;
                PrevCmdAddSpeed = CmdAddSpeed;
                PrevCmdRemoveSpeed = CmdRemoveSpeed;
                PrevCmdSetSpeed = CmdSetSpeed;
            }
        }

        #endregion

        #region Comandi Teach mode

        /// <summary>
        /// Legge i comandi da plc per mettere il robot in drag mode
        /// </summary>
        private static void GetDragCommands()
        {
            bool cmdStartDrag = Convert.ToInt16(PLCConfig.appVariables.getValue(PLCTagName.Cmd_dragMode_ON)) > 0;
            if(cmdStartDrag && !isDragStart) // Avvio drag mode
            {
                log.Info("Richiesta di start drag mode");

                // Controlli prima di avviare
                if (isAutomaticMode)
                {
                    log.Error("Tentativo di accesso a Drag Mode con robot in modalità automatic");
                    return;
                }

                byte state = 1;
                //byte enable = 1;
                int err = 0;
                //Abilitazione
                robot.DragTeachSwitch(state);
                //robot.SetLoadWeight(1);
                //await Task.Delay(10);
                //Controllo
                //err = robot.RobotEnable(enable);
                robot.IsInDragTeach(ref state);

                if (state != 1 && err != 0)
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Errore durante drag mode switch a ON");
                    log.Error("Errore durante drag mode switch a ON");
                }
                else
                {
                    isDragStart = true;
                }
            }
            else if(!cmdStartDrag && isDragStart) // Stop drag mode
            {
                log.Info("Richiesta di stop drag mode");

                byte state = 0;
                //byte enable = 0;
                int err = 0;
                //Disabilitazione
                robot.DragTeachSwitch(state);
                //robot.SetLoadWeight(0);
                //await Task.Delay(100);
                //Controllo

                //err = robot.RobotEnable(enable);
                robot.IsInDragTeach(ref state);

                if (state != 0 && err != 0)
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Errore durante drag mode switch a OFF. Necessario riavvia del sistema.");
                    log.Error("Errore durante drag mode switch a OFF. Necessario riavvia del sistema.");
                }
                else
                {
                    isDragStart = false;
                }
            }
            else // Drag mode disattivata o drag mode in corso
            {

            }
        }

        private static void GetTeachCommands()
        {
            if (!isUcDragModeDisplayed)
                return;

            bool cmdStartTeach = Convert.ToInt16(PLCConfig.appVariables.getValue(PLCTagName.Cmd_teachingRec_ON)) > 0;
            if (cmdStartTeach && !isTeachStart) // Start teach mode
            {
                log.Warn("Richiesta di start teach mode");
                if (isAutomaticMode)
                {
                    log.Error("Tentativo di accesso a Drag Mode con robot in modalità automatic");
                    return;
                }
                //// Avvio teach mode
                //if (AlarmManager.isRobotMoving)
                //{
                //    log.Error("Aspetta che il robot finisca il movimento");
                //    return;
                //}
                if (!isDragStart && dragMode != 2) // Le modalità ptp e linear hanno bisogno di essere in drag
                {
                    //CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Attivare la drag mode per usare questa funzionalità");
                    log.Error("Attivare la drag mode per usare questa funzionalità");
                    return;
                }
                // Controlli prima di avviare
                else if (dragMode == 2) // Se modalità VR non c'è bisogno di avere la drag mode attiva
                {
                    DescPose trackerPose = formTrackerCalibration.trackerTransformedCurrentPose;
                    DescPose robotPose = TCPCurrentPosition;
                    int deltaTraslation = 20; //2cm
                    int deltaRotation = 3;

                    bool match = true;
                    if (Math.Abs(Math.Abs(robotPose.tran.x) - Math.Abs(trackerPose.tran.x)) > deltaTraslation) match = false;
                    if (Math.Abs(Math.Abs(robotPose.tran.y) - Math.Abs(trackerPose.tran.y)) > deltaTraslation) match = false;
                    if (Math.Abs(Math.Abs(robotPose.tran.z) - Math.Abs(trackerPose.tran.z)) > deltaTraslation) match = false;
                    if (Math.Abs(Math.Abs(robotPose.rpy.rx) - Math.Abs(trackerPose.rpy.rx)) > deltaRotation) match = false;
                    if (Math.Abs(Math.Abs(robotPose.rpy.ry) - Math.Abs(trackerPose.rpy.ry)) > deltaRotation) match = false;
                    if (Math.Abs(Math.Abs(robotPose.rpy.rz) - Math.Abs(trackerPose.rpy.rz)) > deltaRotation) match = false;

                    if (!match)
                    {
                        CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "La posizione del tracker calibrato non combacia con quella del robot");
                        log.Error("La posizione del tracker calibrato non combacia con quella del robot");
                        return;
                    }
                }

                positionsToSend.Clear();

                // Impostazione del carico del robot
                robot.SetLoadWeight(robotProperties.Weight);

                if (dragMode == 0) // PTP
                {
                    StartTeachingPTP();
                }
                else if (dragMode == 1) // Linear
                {
                    StartTeachingLineare();
                }
                else if (dragMode == 2) // Tracker
                {
                    StartTeachingTracker();
                }

                isTeachStart = true;

                try
                {
                    RequestedStartTeach?.Invoke(null, EventArgs.Empty);
                }
                catch(Exception ex)
                {
                    log.Error("Erorre durante l'aggiornamento della UI da start drag: " + ex);
                }
            }
            else if(!cmdStartTeach && isTeachStart) // Stop teaching
            {
                log.Warn("Richiesta di stop teach mode");

                if (dragMode == 0) // PTP
                {
                    StopTeachingPTP();
                }
                else // Linear o VR
                {
                    StopTeachingLineare();
                }

                isTeachStart = false;

                try
                {
                    RequestedStopTeach?.Invoke(null, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    log.Error("Erorre durante l'aggiornamento della UI da start drag: "  + ex);
                }
            }
            else
            {

            }
        }

        #endregion

        #region Comandi interfaccia

        /// <summary>
        /// Esegue inPosition su una lista di posizioni continuamente aggiornata da CheckMonitoring
        /// </summary>
        private static async Task CheckPosition(CancellationToken token)
        {
            try
            {
                // Contiene indice della riga precedente a quella attuale
                int previousPoint = -1;

                // Lista contenente tutti i gun settings
                List<GunSettings> gunSettings = new List<GunSettings>();

                // Lista di appoggio
                List<ApplicationPositions> positions = ApplicationConfig.applicationsManager.getDictionary()[applicationName].positions;
                List<(string key, int? value, string type)> updates = new List<(string, int?, string)>();

                int? prevFeed_air = 0;
                int? prevDosage_air = 0;
                int? prevGun_air = 0;
                int? prevMicroampere = 0;
                int? prevKV = 0;
                int? prevStatus = 0;

                // Scorri la lista per inserire tutti i gun settings 
                foreach (ApplicationPositions pos in positions)
                {
                    gunSettings.Add(pos.gunSettings);
                }

                while (!token.IsCancellationRequested && !AlarmManager.blockingAlarm)
                {

                    // Se siamo all'ultimo punto, resetta il contatore ma NON invocare ancora l'evento
                    if (previousPoint >= positions.Count - 1)
                    {
                        previousPoint = -1; // Resetta solo il contatore
                        RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16");
                        chainPos = 0;
                    }

                    inPositionGun = false; // Reset di inPosition

                    if (aggiornamentoFinito) // Se la lista ha concluso l'aggiornamento, procedo
                    {
                        var copiaListaDizionario = positionsToCheck.ToList(); // Creare una copia per iterazioni sicure

                        foreach (var elemento in copiaListaDizionario)
                        {
                            inPositionGun = checker_monitoringPos.IsInPosition(elemento.Value, TCPCurrentPosition);

                            if (inPositionGun && elemento.Key > previousPoint && elemento.Key < previousPoint + 5) // Verifica posizione valida
                            {
                                #region Scrittura valori pistola a PLC

                                if (gunSettings[elemento.Key].feed_air != prevFeed_air)
                                    updates.Add((PLCTagName.Bar_Per_100_POLVERE, gunSettings[elemento.Key].feed_air, "INT16"));

                                if (gunSettings[elemento.Key].dosage_air != prevDosage_air)
                                    updates.Add((PLCTagName.Bar_Per_100_DOSAGGIO, gunSettings[elemento.Key].dosage_air, "INT16"));

                                if (gunSettings[elemento.Key].gun_air != prevGun_air)
                                    updates.Add((PLCTagName.Bar_Per_100_ARIA, gunSettings[elemento.Key].gun_air, "INT16"));

                                if (gunSettings[elemento.Key].microampere != prevMicroampere)
                                    updates.Add((PLCTagName.UA, gunSettings[elemento.Key].microampere, "INT16"));

                                if (gunSettings[elemento.Key].kV != prevKV)
                                    updates.Add((PLCTagName.Kv, gunSettings[elemento.Key].kV, "INT16"));

                                if (gunSettings[elemento.Key].status != prevStatus)
                                    updates.Add((PLCTagName.Start_Gun, gunSettings[elemento.Key].status, "INT16"));

                                // Invio al PLC solo se ci sono aggiornamenti
                                if (updates.Any())
                                {
                                    SendUpdatesToPLC(updates);
                                    updates.Clear();
                                }

                                // Aggiornamento dei valori di confronto
                                prevFeed_air = gunSettings[elemento.Key].feed_air;
                                prevDosage_air = gunSettings[elemento.Key].dosage_air;
                                prevGun_air = gunSettings[elemento.Key].gun_air;
                                prevMicroampere = gunSettings[elemento.Key].microampere;
                                prevKV = gunSettings[elemento.Key].kV;
                                prevStatus = gunSettings[elemento.Key].status;

                                #endregion

                                #region Colorazione lw

                                // Invoca l'evento solo se la posizione è cambiata
                                if (lastUpdatedKey != elemento.Key)
                                {
                                    RobotPositionChanged?.Invoke(elemento.Key, EventArgs.Empty);
                                    lastUpdatedKey = elemento.Key; // Aggiorna la chiave
                                }

                                #endregion

                                // Se riparte dal primo punto, invoca l'evento CycleRestarted
                                if (previousPoint == -1 && elemento.Key == 0)
                                {
                                    CycleRestarted?.Invoke(null, EventArgs.Empty);
                                    RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 1, "INT16");
                                    stopChainUpdaterThread = false;
                                }

                                // Aggiorna i riferimenti dei punti
                                previousPoint = elemento.Key;
                                currentIndex = elemento.Key;
                            }
                        }
                    }

                    // Ritardo per ridurre la frequenza di esecuzione
                    await Task.Delay(positionCheckerThreadRefreshPeriod);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Avvia la riproduzione dei punti dell'applicazione selezionata
        /// </summary>
        /// <param name="application">Applicazione selezionata</param>
        private static void StartApplication(string application)
        {
            log.Info("Richiesta di avvio ciclo");
            bool start = false;

            // Se il Robot non si trova in home position e non stava riproducendo nessun punto
            // stampo un messaggio di errore per richiedere di portare il Robot in posizione di Home
            if (!isInHomePosition && currentIndex < 0)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Spostare il Robot nella posizione di Home", Resources.safeZone_yellow32);
                log.Error("Tentativo di avvio applicazione con Robot fuori posizione di Home");
                CmdStartCycle = false;
                return;
            }

            if (!string.IsNullOrEmpty(application)) // Se l'applicazione è stata selezionata
            {
                // Setto della velocità del Robot dalle sue proprietà memorizzate sul database
                if (robotProperties.Speed > 1)
                {
                    int speed = robotProperties.Speed;
                    robot.SetSpeed(speed);
                }

                if (!AlarmManager.isRobotMoving) // Se il Robot non è in movimento 
                {
                    // Get da database delle posizioni dell'applicazione selezionata
                    DataTable positions = RobotDAO.GetPointsPosition(ConnectionString, application);

                    // Se non ci sono posizioni presenti nell'applicazione da riprodurre
                    if (positions.Rows.Count < 1)
                    {
                        CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Nessun punto presente nell'applicazione selezionata");
                        log.Error("Tentativo di riprodurre un'applicazione senza posizioni");
                        CmdStartCycle = false;
                        return;
                    }

                    // Se l'avvio dell'applicazione parte dal primo punto
                    if (currentIndex < 0)
                    {
                        if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Procedere con l'avvio dell'applicazione?") != DialogResult.OK)
                        {
                            CmdStartCycle = false;
                            return;
                        }
                    }
                    else // Se l'applicazione riprende da un punto precedente
                    {
                        if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Riprendere la riproduzione dell'applicazione?") != DialogResult.OK)
                        {
                            CmdStartCycle = false;
                            return;
                        }
                    }

                    // Quindi, se il ciclo era stato messo precedentemente in pausa, metto a true il booleano riprendiCiclo
                    // che fa uscire dallo step di attesa il metodo che riproduce i punti dentro StartApplication
                    if (pauseCycleRequested)
                    {
                        riprendiCiclo = true;
                    }
                    else // Se invece il ciclo deve iniziare dall'inizio, avvio normalmente
                    {
                        //StartApplication(application);
                        start = true;
                        CycleRoutineStarted.Invoke(0, EventArgs.Empty);
                        //EnableCycleButton(1); // Disattiva stop, disattiva pause, attiva start
                    }
                }
                else // Se il Robot è in movimento
                {
                    log.Error("Impossibile inviare nuovi punti al Robot. Robot in movimento");
                    CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impossibile inviare nuovi punti al Robot");
                    CmdStartCycle = false;
                    return;
                }
            }
            else // Se l'applicazione non è stata selezionata
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Nessuna applicazione caricata da riprodurre");
                log.Error("Nessuna applicazione caricata da riprodurre");
                CmdStartCycle = false;
                return;
            }

            if (start)
            {
                // Reset della condizione per terminare il thread che gestisce colorazione su lw_positions
                stopPositionCheckerThread = false;

                // positionsToCheck.Clear(); // Reset della lista che viene letta da positionCheckerThread
                UC_FullDragModePage.stopMonitoring = true; // Stop del thread monitoring nel caso sia attivo

                // Thread che esegue selezione su lw_positions e scrittura valori pistole (position checker)
                taskManager.AddAndStartTask(nameof(CheckPosition), CheckPosition, TaskType.Default, false);

                stopCycleRequested = false; // Reset della richiesta di stop della riproduzione dei punti

                pauseCycleRequested = false; // Reset della richiesta di pausa della riproduzione dei punti

                stopCycleRoutine = false; // A true quando la routine termina

                stopChainUpdaterThread = false; // A true quando bisogna disattivare l'updater del contatore della catena

                chainPos = 0; // Inizializzazione posizione della catena

                // Faccio girare processo su un thread esterno a quello principale
                taskManager.AddAndStartTask(nameof(ApplicationCycleRoutine),
                    (token) => ApplicationCycleRoutine(application, token), TaskType.Default, false);
            }
        }

        /// <summary>
        /// Routine del ciclo di applicazione
        /// </summary>
        /// <param name="application"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async static Task ApplicationCycleRoutine(string application, CancellationToken token)
        {
            int thresholdPos = 5; // Soglia di posizioni da eseguire

            // Indice della posizione presente nella lista delle posizione da riprodurre su cui eseguire inPosition 
            int calculateIndex = 2;

            int step = 0; // Step routine

            // Get della posizione di home dal dizionario delle posizioni
            var homePose = ApplicationConfig.applicationsManager.GetPosition("pHome", "RM");

            int index = 0; // Indice della posizione

            int indexEndingPoint = 0; // Indice della posizione su cui eseguire inPosition

            DescPose targetPos = new DescPose(0, 0, 0, 0, 0, 0); // Posizione da riprodurre

            // Creazione del punto di Home
            DescPose pHome = new DescPose(
                homePose.x,
                homePose.y,
                homePose.z,
                homePose.rx,
                homePose.ry,
                homePose.rz
                );

            // Dichiarazione lista che conterrà le posizioni dell'applicazione selezionata
            List<DescPose> pointList = new List<DescPose>();

            int offsetChain = 0; // Variabile da aggiornare ogni ciclo che corrisponde allo spostamento catena

            try
            {
                // Get da database delle posizioni dell'applicazione selezionata
                DataTable positions = RobotDAO.GetPointsPosition(ConnectionString, application);

                // Riempimento lista delle posizioni dell'applicazione selezionata
                foreach (DataRow rw in positions.Rows)
                {
                    DescPose pos = new DescPose(
                        Convert.ToDouble(rw["x"]),
                        Convert.ToDouble(rw["y"]),
                        Convert.ToDouble(rw["z"]),
                        Convert.ToDouble(rw["rx"]),
                        Convert.ToDouble(rw["ry"]),
                        Convert.ToDouble(rw["rz"])
                    );

                    pointList.Add(pos);
                }

                // Inizializzazione della variabile currentIndex in modo che parta dall'indice corrente
                if (currentIndex < 0)
                    index = 0;
                else if (currentIndex >= positions.Rows.Count - 1)
                {
                    index = 0;
                }
                else
                    index = currentIndex + 1;

                // Reset variabile che fa partire il contatore della catena
                RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16");
                await Task.Delay(200); // Leggero ritardo per stabilizzare il sistema

                // Imposto a 1 (true) Automatic_Start, così che parta anche il conteggio dello spostamento della catena
                RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 1, "INT16");

                // Imposto a 0 (false) Auto_Cycle_End che segnala che il ciclo automatico è iniziato
                RefresherTask.AddUpdate(PLCTagName.Auto_Cycle_End, 0, "INT16");

                // Lista delle posizioni da riprodurre 
                List<DescPose> targetPositions = new List<DescPose>();

                for (int i = 0; i < pointList.Count; i++)
                {
                    targetPos = new DescPose(
                                pointList[i].tran.x,
                                pointList[i].tran.y,
                                pointList[i].tran.z,
                                pointList[i].rpy.rx,
                                pointList[i].rpy.ry,
                                pointList[i].rpy.rz);

                    targetPositions.Add(targetPos);
                }

                while (!token.IsCancellationRequested && !AlarmManager.blockingAlarm && !stopCycleRoutine)
                {
                    switch (step)
                    {
                        case 0:
                            #region Invio delle posizioni da riprodurre

                            if (index <= pointList.Count - 1 && !stopCycleRequested)
                            {
                                offsetChain = chainPos;
                                // Calcolo dell'indice della posizione su cui eseguire inPosition
                                indexEndingPoint = index + calculateIndex;

                                // Reset inPosition
                                inPosition = false;

                                aggiornamentoFinito = false;

                                // Riproduco le posizioni che partono dall'indice in cui si trova la routine
                                // all'indice composto dalla somma tra l'indice e la soglia che indica il numero di posizioni da riprodurre
                                for (int i = index; i < index + thresholdPos; i++)
                                {
                                    if (i <= pointList.Count - 1)
                                    {
                                        targetPos = new DescPose(
                                            targetPositions[i].tran.x + offsetChain + (i - index),
                                            targetPositions[i].tran.y,
                                            targetPositions[i].tran.z + (i - index),
                                            targetPositions[i].rpy.rx,
                                            targetPositions[i].rpy.ry,
                                            targetPositions[i].rpy.rz
                                            );

                                        positionsToCheck.Add(new KeyValuePair<int, DescPose>(i, targetPos));
                                    }
                                }
                                aggiornamentoFinito = true;

                                // Riproduco le posizioni che partono dall'indice in cui si trova la routine
                                // all'indice composto dalla somma tra l'indice e la soglia che indica il numero di posizioni da riprodurre
                                for (int i = index; i < index + thresholdPos; i++)
                                {
                                    if (i <= pointList.Count - 1)
                                    {
                                        targetPos = new DescPose(
                                           targetPositions[i].tran.x + offsetChain + (i - index),
                                           targetPositions[i].tran.y,
                                           targetPositions[i].tran.z + (i - index),
                                           targetPositions[i].rpy.rx,
                                           targetPositions[i].rpy.ry,
                                           targetPositions[i].rpy.rz
                                           );

                                        // Se l'indice della posizione creata corrisponde all'indice
                                        // della posizione su cui eseguire l'inPosition, assegno questo punto come endingPoint
                                        if (i == indexEndingPoint)
                                            endingPoint = targetPos;

                                        // Invio delle posizione al robot
                                        err = robot.MoveCart(targetPos, tool, user, vel, acc, ovl, blendT, config);

                                        if (err != 0) // Se il movimento ha generato un errore
                                            log.Error("Errore durante il movimento del robot: " + err.ToString());
                                    }
                                }

                                step = 10;
                            }
                            else // Se non ci sono più posizioni da riprodurre
                            {
                                // Se è stata richiesto lo stop immediato del ciclo
                                if (stopCycleRequested)
                                {
                                    step = 5;
                                }
                                else
                                    if (index > pointList.Count - 1) // Se i punti sono terminati, riavvio il ciclo
                                {
                                    index = 0;
                                    step = 0;
                                    chainPos = 0; // Azzero spostamento catena
                                    stopChainUpdaterThread = true; // Fermo aggiornamento catena
                                }
                            }

                            break;

                        #endregion

                        case 5:
                            #region Termine routine

                            CmdStartCycle = false;

                            positionsToCheck.Clear();

                            stopPositionCheckerThread = true;

                            // Imposto a 0 (false) Automatic_Start che resetta anche il contatore dello spostamento della catena
                            RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16");

                            // Imposto a 1 (true) Auto_Cycle_End che segnala che il ciclo automatico è terminato
                            RefresherTask.AddUpdate(PLCTagName.Auto_Cycle_End, 1, "INT16");

                            // Abilito il tasto Start per avviare nuovamente la routine
                            CycleRoutineStarted?.Invoke(1, EventArgs.Empty);

                            // Imposto a false il booleano che fa terminare il thread della routine
                            stopCycleRoutine = true;

                            // Fermo thread per aggiornamento posizione catena
                            stopChainUpdaterThread = true;

                            break;

                        #endregion

                        case 10:
                            #region Attesa inPosition, aggiornamento index e check richiesta di stop/pausa

                            if (inPosition) // Se il robot è arrivato all'ending point
                            {
                                // Aggiornamento index
                                index = index + thresholdPos;

                                if (positionsToCheck.Count > numPositionsToCheck)
                                {
                                    positionsToCheck.RemoveRange(0, 2);
                                }

                                step = 0;
                            }


                            // Se è stata richiesta lo stop del ciclo, rimando subito allo step 0,
                            // così che venga termino il thread
                            if (stopCycleRequested)
                            {
                                step = 0;
                            }

                            // Se è stata richiesta la pausa del ciclo, aspetto di arrivare alla posizione
                            // subito successiva alla quale si trova il Robot
                            if (pauseCycleRequested)
                            {
                                inPosition = false;
                                if (currentIndex < targetPositions.Count - 1)
                                {
                                    // Se la pausa viene richiesta non all'ultimo punto,
                                    // metto come endingPoint il punto successivo
                                    endingPoint = targetPositions[currentIndex + 1];
                                }
                                else
                                {
                                    // Se la pausa viene richiesta all'ultimo punto,
                                    // metto come endingPoint il primo punto della lista
                                    endingPoint = targetPositions[0];
                                }

                                step = 15;
                            }
                            break;

                        #endregion

                        case 15:
                            #region Pausa del Robot

                            CmdStartCycle = false;
                            if (inPosition)
                            {
                                // Stop del Robot
                                robot.PauseMotion();
                                await Task.Delay(200);
                                robot.StopMotion();

                                inPosition = false; // Reset inPosition

                                // Abilito il tasto Start per avviare nuovamente la routine
                                CycleRoutineStarted?.Invoke(1, EventArgs.Empty);

                                step = 20;
                            }
                            break;

                        #endregion

                        case 20:
                            #region Ripresa del ciclo

                            if (riprendiCiclo)
                            {
                                riprendiCiclo = false;
                                pauseCycleRequested = false; // Reset richiesta di pausa
                                index = currentIndex + 1;
                                CycleRoutineStarted?.Invoke(0, EventArgs.Empty);
                                step = 0;
                            }
                            break;

                            #endregion
                    }

                    await Task.Delay(10, token);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CmdStartCycle = false;
                CycleRoutineStarted.Invoke(1, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Mette in pausa il ciclo alzando un bit
        /// </summary>
        private static void PauseApplication()
        {
            pauseCycleRequested = true; // Alzo richiesta di pausa ciclo
            RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16"); // Reset della variabile che fa partire contatore catena
            RefresherTask.AddUpdate(PLCTagName.Chain_Enable, 0, "INT16"); // Reset della variabile che attiva catena
            stopChainUpdaterThread = true; // Fermo aggiornamento catena

            CmdPauseCycle = false;
        }

        /// <summary>
        /// Stoppa forzatamente il ciclo alzando un bit
        /// </summary>
        private static async void StopApplication()
        {
            stopCycleRoutine = true; // Alzo richiesta per fermare thread di riproduzione ciclo
                                     // Imposto a 0 (false) Automatic_Start che resetta anche il contatore dello spostamento della catena
            RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16");

            // Imposto a 1 (true) Auto_Cycle_End che segnala che il ciclo automatico è terminato
            RefresherTask.AddUpdate(PLCTagName.Auto_Cycle_End, 1, "INT16");

            //EnableCycleButton(0);

            currentIndex = -1; // Reset dell'indice corrente della posizione che riproduce il Robot

            robot.PauseMotion(); // Invio comando di pausa al robot
            await Task.Delay(200); // Leggero ritardo per stabilizzare il robot
            robot.StopMotion(); // Stop Robot con conseguente cancellazione di coda di punti

            CmdStopCycle = false;
        }

        /// <summary>
        /// Avvia la riproduzione della home routine
        /// </summary>
        private static async void HomeRoutineStarter()
        {
            // Se la home routine è già in esecuzione non eseguo il metodo
            if (homeRoutineStarted)
            {
                CmdHomeRoutine = false;
                return;
            }

            // Se il robot non si trova in SafeZone per tornare in Home stampo messaggio di errore
            else if (!isInSafeZone)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Robot fuori dalla zona sicura per tornare in Home");
                CmdHomeRoutine = false;
                return;
            }

            // Chiedo conferma per avvio della HomeRoutine
            if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Posizionare il Robot in Home position?") != DialogResult.OK)
            {
                CmdHomeRoutine = false;
                return;
            }

            robot.RobotEnable(1); // Abilito il Robot
            await Task.Delay(2000); // Attesa di 2s per stabilizzare il Robot
            homeRoutineStarted = true; // Segnalo che la home routine è partita

            // Rendo il tasto per andare in HomePosition non cliccabile
            //btn_homePosition.Enabled = false;
            //btn_homePosition.BackColor = SystemColors.ControlDark;
            //btn_homePosition.BackgroundImage = null;

            taskManager.AddAndStartTask(nameof(HomeRoutine), HomeRoutine, TaskType.Default, false);
        }

        /// <summary>
        /// Task di home routine
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task HomeRoutine(CancellationToken token)
        {
            // Get del punto di home
            var restPose = ApplicationConfig.applicationsManager.GetPosition("pHome", "RM");
            DescPose pHome = new DescPose(restPose.x, restPose.y, restPose.z, restPose.rx, restPose.ry, restPose.rz);

            bool stopHomeRoutine = false; // Reset della richiesta di stop HomeRoutine
            int stepHomeRoutine = 0; // Reset degli step della HomeRoutine

            try
            {
                while (!token.IsCancellationRequested && !stopHomeRoutine && !AlarmManager.blockingAlarm)
                {
                    // Se viene tolta la modalità manuale durante la home routine, questa viene bloccata immediatamente
                    if (mode != 2)
                    {
                        robot.PauseMotion(); // Pausa Robot
                        await Task.Delay(200); // Leggero delay per stabilizzare il Robot
                        robot.StopMotion(); // Stop del Robot
                        homeRoutineStarted = false; // Reset variabile che indica l'avvio della home routine
                        stopHomeRoutine = true; // Alzo richiesta per terminare metodo che riproduce home routine
                    }
                    else // Altrimenti eseguo la home routine normalmente
                    {
                        switch (stepHomeRoutine)
                        {
                            case 0:
                                #region Cancellazione coda Robot e disattivazione tasti applicazione
                                HomeRoutineStarted?.Invoke(1, EventArgs.Empty);
                                //ClearRobotQueue();
                                await Task.Delay(1000);

                                stepHomeRoutine = 10;
                                break;

                            #endregion

                            case 10:
                                #region Movimento a punto di home

                                GoToHomePosition();
                                endingPoint = pHome;

                                stepHomeRoutine = 20;
                                break;

                            #endregion

                            case 20:
                                #region Attesa inPosition home

                                if (inPosition)
                                    stepHomeRoutine = 30;
                                break;

                            #endregion

                            case 30:
                                #region Termine ciclo e riattivazione tasti applicazione e tasto home 

                                homeRoutineStarted = false;
                                HomeRoutineStarted?.Invoke(0, EventArgs.Empty);
                                stopHomeRoutine = true;

                                break;

                                #endregion
                        }
                    }
                    await Task.Delay(100, token);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await Task.Delay(2000); // Attesa di 2s per stabilizzare il Robot

                robot.RobotEnable(0); // Disattivo il Robot

                homeRoutineStarted = false;

                CmdHomeRoutine = false;
            }
        }

        private static void ManageSpeed(bool increase, int setVal = -1)
        {
            if (setVal == -1)
            {
                vel = Math.Min(100, increase ? vel + 1 : vel - 1);

                RobotDAO.SetRobotVelocity(ConnectionString, Convert.ToInt16(vel));
                RobotDAO.SetRobotAcceleration(ConnectionString, Convert.ToInt16(vel));
                TriggerRobotVelocityChangedEvent(Convert.ToInt16(vel));

                robot.SetSpeed(Convert.ToInt16(vel));
            }
            else if (setVal >= 0 && setVal <= 100)
            {
                RobotDAO.SetRobotVelocity(ConnectionString, Convert.ToInt16(setVal));
                RobotDAO.SetRobotAcceleration(ConnectionString, Convert.ToInt16(setVal));
                TriggerRobotVelocityChangedEvent(Convert.ToInt16(setVal));

                vel = setVal;
                robot.SetSpeed(setVal);
            }

            // Reset
            CmdAddSpeed = false;
            CmdRemoveSpeed = false;
            CmdSetSpeed = -1; 
        }

        #endregion

        /// <summary>
        /// Legge i flag alzati dalla UI per capire i comandi da lanciare
        /// </summary>
        private static void GetInterfaceCommands()
        {
            // --- GESTIONE START CICLO ---
            // Controlliamo il "fronte di salita"
            if (CmdStartCycle && !PrevCmdStartCycle)
            {
                log.Info("[Interpreter] Rilevato comando START CICLO.");
                StartApplication(applicationName); // Esegui l'azione
            }
            PrevCmdStartCycle = CmdStartCycle; // Aggiorna lo stato precedente

            // --- GESTIONE STOP CICLO ---
            if (CmdStopCycle && !PrevCmdStopCycle)
            {
                log.Info("[Interpreter] Rilevato comando STOP CICLO.");
                StopApplication();
            }
            PrevCmdStopCycle = CmdStopCycle;

            // --- GESTIONE PAUSA CICLO ---
            if (CmdPauseCycle && !PrevCmdPauseCycle)
            {
                log.Info("[Interpreter] Rilevato comando PAUSA CICLO.");
                PauseApplication();
            }
            PrevCmdPauseCycle = CmdPauseCycle;

            // --- GESTIONE HOME ROUTINE ---
            if (CmdHomeRoutine && !PrevCmdHomeRoutine)
            {
                log.Info("[Interpreter] Rilevato comando GO TO HOME POSITION");
                HomeRoutineStarter();
            }
            PrevCmdHomeRoutine = CmdHomeRoutine;
            
            // --- GESTIONE VELOCITÀ ---
            if (CmdAddSpeed && !PrevCmdAddSpeed)
            {
                log.Info("[Interpreter] Rilevato comando AUMENTA VELOCITÀ.");
                ManageSpeed(true);
            }
            PrevCmdAddSpeed = CmdAddSpeed;

            if (CmdRemoveSpeed && !PrevCmdRemoveSpeed)
            {
                log.Info("[Interpreter] Rilevato comando DIMINUISCI VELOCITÀ.");
                ManageSpeed(false);
            }
            PrevCmdRemoveSpeed = CmdRemoveSpeed;

            // --- GESTIONE IMPOSTA VELOCITÀ ---
            if (CmdSetSpeed != -1)
            {
                log.Info($"[Interpreter] Rilevato comando IMPOSTA VELOCITÀ a {CmdSetSpeed}%.");
                ManageSpeed(true, CmdSetSpeed);
            }
        }

        /// <summary>
        /// Ottiene le informazioni del robot attraverso i metodi bloccanti della libreria
        /// </summary>
        private static void GetRobotInfo()
        {
            if(AlarmManager.isRobotConnected)
            {
                log.Info("Recupero informazioni del robot");
                robot.GetSDKVersion(ref RobotSdkVer);
                robot.GetControllerIP(ref RobotCurrentIP);
                robot.GetSoftwareVersion(ref RobotModelVer, ref RobotWebVer, ref RobotControllerVer);
                robot.GetFirmwareVersion(ref RobotFwBoxBoardVer, ref RobotFwDriver1Ver, ref RobotFwDriver2Ver, ref RobotFwDriver3Ver,
                    ref RobotFwDriver4Ver, ref RobotFwDriver5Ver, ref RobotFwDriver6Ver, ref RobotFwEndBoardVer);
                robot.GetHardwareVersion(ref RobotHwBoxBoardVer, ref RobotHwDriver1Ver, ref RobotHwDriver2Ver, ref RobotHwDriver3Ver,
                    ref RobotHwDriver4Ver, ref RobotHwDriver5Ver, ref RobotHwDriver6Ver, ref RobotHwEndBoardVer);
            }
        }

        /// <summary>
        /// Invoca metodo relativo al cambio di velocità del robot
        /// </summary>
        /// <param name="vel">Velocità impostata al Robot</param>
        public static void TriggerRobotVelocityChangedEvent(int vel)
        {
            RobotVelocityChanged?.Invoke(vel, EventArgs.Empty);
        }

        /// <summary>
        /// Invoca metodo relativo al cambio di modalità del robot
        /// </summary>
        /// <param name="mode"></param>
        public static void TriggerRobotModeChangedEvent(int mode)
        {
            RobotModeChanged?.Invoke(mode, EventArgs.Empty);
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
        /// Legge allarmi derivanti dal Robot
        /// </summary>
        /// <param name="updates"></param>
        /// <param name="allarmeSegnalato"></param>
        /// <param name="robotAlarm"></param>
        /// <param name="now"></param>
        /// <param name="id"></param>
        /// <param name="description"></param>
        /// <param name="timestamp"></param>
        /// <param name="device"></param>
        /// <param name="state"></param>
        /// <param name="unixTimestamp"></param>
        /// <param name="dateTime"></param>
        /// <param name="formattedDate"></param>
        private static void GetRobotErrorCode(List<(string key, bool value, string type)> updates, bool allarmeSegnalato, 
            DataRow robotAlarm, DateTime now, string id, string description, string timestamp, string device, string state, long unixTimestamp,
            DateTime dateTime, string formattedDate)
        {
            if (AlarmManager.isRobotConnected)
            {
                err = robot.GetRobotErrorCode(ref maincode, ref subcode);
                if (maincode != 0 && !IsAlarmAlreadySignaled(maincode.ToString() + subcode.ToString()))
                {
                    robotAlarm = RobotDAO.GetRobotAlarm(ConnectionString, maincode, subcode);
                    if (robotAlarm != null)
                    {
                        Console.WriteLine($"mainErrCode {maincode} subErrCode {subcode} ");

                        // Ottieni la data e l'ora attuali
                        now = DateTime.Now;

                        // Calcola il timestamp Unix in millisecondi
                        unixTimestamp = ((DateTimeOffset)now).ToUnixTimeMilliseconds();

                        dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTimestamp.ToString())).DateTime.ToLocalTime();
                        formattedDate = dateTime.ToString("dd-MM-yyyy HH:mm:ss");

                        if (robotAlarm["id"].ToString() == "")
                        {
                            id = "9999";
                            description = "Generic/Not found";
                            timestamp = formattedDate;
                            device = "Robot";
                            state = "ON";
                        }
                        else
                        {
                            id = robotAlarm["id"].ToString();
                            description = robotAlarm["descr_MainCode"].ToString() + ": " + robotAlarm["descr_SubCode"].ToString();
                            timestamp = formattedDate;
                            device = "Robot";
                            state = "ON";
                        }
                        CreateRobotAlarm(id, description, timestamp, device, state);
                        MarkAlarmAsSignaled(maincode.ToString() + subcode.ToString());
                        log.Warn(robotAlarm["descr_MainCode"].ToString() + ": " + robotAlarm["descr_SubCode"].ToString());

                        // Imposta la variabile di stato dell'allarme
                        allarmeSegnalato = true;                      
                    }
                    else
                    {
                        // Ottieni la data e l'ora attuali
                        now = DateTime.Now;

                        // Calcola il timestamp Unix in millisecondi
                        unixTimestamp = ((DateTimeOffset)now).ToUnixTimeMilliseconds();

                        dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unixTimestamp.ToString())).DateTime.ToLocalTime();
                        formattedDate = dateTime.ToString("dd-MM-yyyy HH:mm:ss");

                        id = "9999";
                        description = "Generic/Not found";
                        timestamp = formattedDate;
                        device = "Robot";
                        state = "ON";

                        CreateRobotAlarm(id, description, timestamp, device, state);

                        // Imposta la variabile di stato dell'allarme
                        allarmeSegnalato = true;
                    }

                    // Alzo bit per segnalare errore
                    RefresherTask.AddUpdate(PLCTagName.System_error, 1, "INT16");

                    // Segnalo che è presente un allarme bloccante (allarme robot)
                    AlarmManager.blockingAlarm = true;

                    pauseCycleRequested = false; // Reset della richiesta di pausa ciclo
                }
                else if (maincode == 0)
                {
                    // Reimposta la variabile di stato se l'allarme è risolto
                    allarmeSegnalato = false;
                }
            }

            //Thread.Sleep(1000); // Attendere prima di controllare di nuovo
            
        }

        /// <summary>
        /// Controlla che la lista di applicazione del robot abbia almeno un elemento
        /// </summary>
        private static void CheckIsRobotReadyToStart()
        {
            int numRobotApplications = ApplicationConfig.applicationsManager.getDictionary().Count;

            if (numRobotApplications > 1) // 1 perché escludo l'applicazione "RM" 
            {
                if (!prevRobotReadyToStart)
                {
                    // Imposta "Ready to Start" su 1 (true)
                    RefresherTask.AddUpdate(PLCTagName.Hardware_Ready_To_Start, 1, "INT16");
                    prevRobotReadyToStart = true; // Aggiorna lo stato
                }
            }
            else
            {
                if (numRobotApplications < 2) // 2 perché escludo l'applicazione "RM"
                {
                    // Imposta "Ready to Start" su 0 (false)
                    RefresherTask.AddUpdate(PLCTagName.Hardware_Ready_To_Start, 0, "INT16");
                    prevRobotReadyToStart = false; // Aggiorna lo stato
                }
            }
        }

        /// <summary>
        /// Controlla che il Robot abbia un'applicazione caricata in memoria
        /// </summary>
        private static void CheckRobotHasProgramInMemory()
        {
            if (string.IsNullOrEmpty(applicationName))
            {
                if (!prevRobotHasProgramInMemory)
                {
                    // Imposta "Program_In_Memory" su 1 (true)
                    RefresherTask.AddUpdate(PLCTagName.Program_In_Memory, 0, "INT16");
                    prevRobotHasProgramInMemory = true; // Aggiorna lo stato
                }
            }
            else 
            if (!string.IsNullOrEmpty(applicationName))
            {
                if (prevRobotHasProgramInMemory)
                {
                    // Imposta "Program_In_Memory" su 0 (false)
                    RefresherTask.AddUpdate(PLCTagName.Program_In_Memory, 1, "INT16");
                    prevRobotHasProgramInMemory = false; // Aggiorna lo stato
                }
            }
        }
        
        /// <summary>
        /// Esegue check su modalità Robot
        /// </summary>
        private static void CheckRobotMode()
        {
            // Ottieni la modalità operativa dal PLC
            mode = Convert.ToInt16(PLCConfig.appVariables.getValue(PLCTagName.Operating_Mode));

            // Controlla se la modalità è cambiata rispetto all'ultima lettura
            if (mode != lastMode)
            {
                // Aggiorna l'ultima modalità letta e il timestamp
                lastMode = mode;
                lastModeChangeTime = DateTime.Now;
                return; // Aspettiamo che il valore si stabilizzi
            }

            // Verifica se la modalità è rimasta invariata per almeno 1 secondo
            if (DateTime.Now - lastModeChangeTime >= TimeSpan.FromSeconds(2) && mode != stableMode)
            {
                // Modalità confermata stabile: aggiorniamo lo stato
                stableMode = mode;

                // Cambia la modalità del robot in base alla modalità stabile
                if (stableMode == 1 && !prevIsAuto) // Passaggio alla modalità automatica
                {
                    isAutomaticMode = true;
                    SetRobotMode(0);                  // Imposta il robot in modalità automatica
                    JogMovement.StopJogRobotThread(); // Ferma il thread di movimento manuale
                    prevIsAuto = true;
                    prevIsManual = false;
                    prevIsOff = false;
                    TriggerRobotModeChangedEvent(1);  // Evento: modalità automatica
                }
                else if (stableMode == 2 && !prevIsManual) // Passaggio alla modalità manuale
                {
                    isAutomaticMode = false;
                    SetRobotMode(1);                  // Imposta il robot in modalità manuale
                    prevIsManual = true;
                    prevIsAuto = false;
                    prevIsOff = false;
                    TriggerRobotModeChangedEvent(0);  // Evento: modalità manuale
                }
                else if (stableMode == 0 && !prevIsOff) // Passaggio alla modalità Off
                {
                    prevIsOff = true;
                    prevIsAuto = false;
                    prevIsManual = false;
                    TriggerRobotModeChangedEvent(3);  // Evento: modalità Off
                }
            }

            // Esegui logiche aggiuntive come il movimento manuale (Jog)
            if (isEnabledNow && stableMode == 2)
            {
                JogMovement.StartJogRobotThread(); // Avvia il thread di movimento manuale (Jog)
            }
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
                    CreateRobotAlarm(id, description, formattedDate, device, state);
                    MarkAlarmAsSignaled(id);
                }

                prevIsPlcConnected = false;
            }
            else
            {
                if (!prevIsPlcConnected)
                {
                    ClearRobotAlarm();
                    ClearRobotQueue();
                    AlarmManager.blockingAlarm = false;
                    prevIsPlcConnected = true;
                }
            }
        }

        /// <summary>
        /// Invia aggiornamenti alla coda che esegue aggiornamento su PLC
        /// </summary>
        /// <param name="updates">Lista di aggiornamenti</param>
        private static void SendUpdatesToPLC(List<(string key, int? value, string type)> updates)
        {
            foreach (var update in updates)
            {
                RefresherTask.AddUpdate(update.key, update.value, update.type);
            }

            updates.Clear(); // Pulizia della coda
        }

        /// <summary>
        /// Verifica se il punto corrente è all'interno dell'area di safe zone
        /// </summary>
        /// <param name="updates">Lista di aggiornamenti</param>
        private static void CheckIsRobotInSafeZone(DescPose pSafeZone, List<(string key, bool value, string type)> updates)
        {
            isInSafeZone = checker_safeZone.IsInCubicArea(pSafeZone, TCPCurrentPosition);

            if (!AlarmManager.isFormReady)
                return;

            if (!isInSafeZone && prevIsInSafeZone != false) // Se il robot non è nella safe zone
            {
                prevIsInSafeZone = false;
                FormHomePage.Instance.RobotSafeZone.BackgroundImage = Resources.safeZone_yellow32;
                RefresherTask.AddUpdate(PLCTagName.SafePos, 0, "INT16");
            }
            else if (isInSafeZone && prevIsInSafeZone != true) // Se il robot è nella safe zone
            {
                prevIsInSafeZone = true;
                FormHomePage.Instance.RobotSafeZone.BackgroundImage = Resources.safeZone_green32;
                RefresherTask.AddUpdate(PLCTagName.SafePos, 1, "INT16");
            }

        }

        /// <summary>
        /// Verifica se il punto corrente corrisponde ai punti di pick e/o place
        /// </summary>
        private static void CheckIsRobotInPos()
        {
            bool isInPosition = checker_pos.IsInPosition(endingPoint,TCPCurrentPosition);

            if (isInPosition)
            {
                inPosition = true;
            }
            else
            {
                inPosition = false;
            }

        }

        /// <summary>
        /// Verifica se il Robot si trova in posizione di Home
        /// </summary>
        private static void CheckIsRobotInHomePosition(DescPose homePose)
        {
            // Calcola lo stato corrente
            isInHomePosition = checker_pos.IsInPosition(homePose, TCPCurrentPosition);

            // Controlla se lo stato è cambiato rispetto al precedente
            if (previousIsInHomePosition == null || isInHomePosition != previousIsInHomePosition)
            {
                // Aggiorna solo se c'è stato un cambiamento
                RefresherTask.AddUpdate(PLCTagName.Home_Pos, isInHomePosition ? 1 : 0, "INT16");

                // Aggiorna lo stato precedente
                previousIsInHomePosition = isInHomePosition;
            }
        }

        /// <summary>
        /// Verifica se la pose del tracker calcolata si trova nella stessa posizione del robot
        /// </summary>
        /// <param name="trackerPose"></param>
        /// <param name="robotPose"></param>
        /// <returns></returns>
        public static bool CheckIsTrackerInRobotPosition()
        {
            // Calcola lo stato corrente
            bool isInPosition = checker_tracker.IsInPosition(formTrackerCalibration.trackerTransformedCurrentPose, TCPCurrentPosition);

            return isInPosition;
        }

        /// <summary>
        /// Metodo richiamato dall'evento ValueChanged del dizionario delle variabili PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void RefreshVariables(object sender, DictionaryChangedEventArgs e)
        {
            if (AlarmManager.isPlcConnected)
            {
                switch (e.Key)
                {
                    
                }
            }
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
                ("Robot", ""),
                ("", "PLC disconnesso. Il ciclo è stato terminato.")
            };

            bool isBlocking = formAlarmPage.IsBlockingAlarmPresent(criteria);

            if (isBlocking)
            {

                ClearRobotAlarm();
                ClearRobotQueue();

                // Segnalo che non ci sono più allarmi bloccanti
                AlarmManager.blockingAlarm = false;

                // Abilito il tasto Start per avviare nuovamente la routine
                CycleRoutineStarted?.Invoke(1, EventArgs.Empty);

                // Abilito i tasti relativi al monitoring
                EnableDragModeButtons?.Invoke(null, EventArgs.Empty);

                // Resetto contatore posizionamento catena
                RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16");

            }
            
            TriggerAllarmeResettato();

            RefresherTask.AddUpdate(PLCTagName.System_error, 0 ,"INT16");

            // Reset degli allarmi segnalati
            foreach (var key in allarmiSegnalati.Keys.ToList())
            {
                allarmiSegnalati[key] = false;
            }

        }

        /// <summary>
        /// Registra e restituisce punto posizione attuale del Robot
        /// </summary>
        /// <returns></returns>
        public static DescPose RecPoint()
        {
            DescPose pos = new DescPose();

            // Salvo le posizioni registrate
            robot.GetActualTCPPose(flag, ref pos);

            RoundPositionDecimals(ref pos, 3);

            return pos;
        }

        /// <summary>
        /// Esegue get del codice di movimento del robot
        /// </summary>
        /// <param name="result">Codice risultato del movimento del robot</param>
        public static void GetRobotMovementCode(int result)
        {
            if (result != 0) // Se il codice passato come parametro è diverso da 0, significa che il movimento ha generato un errore
            {
                // Get del codice di errore dal database
                DataRow code = RobotDAO.GetRobotMovementCode(ConnectionString, result);

                if (code != null) // Se il codice è presente nel dizionario nel database eseguo la get dei dettagli
                {
                    // Stampo messaggio di errore
                    CustomMessageBox.Show(
                        MessageBoxTypeEnum.ERROR,
                        "Errcode: " + code["Errcode"].ToString() + "\nDescribe: " + code["Describe"].ToString() + "\nProcessing method: " + code["Processing method"].ToString()
                        );

                    // Scrivo messaggio nel log
                    log.Error("Errcode: " + code["Errcode"].ToString() + "\nDescribe: " + code["Describe"].ToString() + "\nProcessing method: " + code["Processing method"].ToString());
                }
                else // Se il codice non è presente nel dizionario nel database stampo un errore generico
                {
                    CustomMessageBox.Show(
                       MessageBoxTypeEnum.ERROR,
                       "Errore generico durante il movimento del robot"
                       );

                    log.Error("Errore generico durante il movimento del robot");

                }
            }
        }

        /// <summary>
        /// Imposta le proprietà del robot prelevandole dal database.
        /// </summary>
        /// <returns>True se l'operazione ha successo, altrimenti False.</returns>
        public static bool SetRobotProperties()
        {
            try
            {
                log.Info("Inizio impostazione delle proprietà del robot dal database.");

                // Ottieni le proprietà del robot dal database
                DataTable dt_robotProperties = RobotDAO.GetRobotProperties(ConnectionString);
                if (dt_robotProperties == null)
                {
                    log.Error("La tabella delle proprietà del robot è nulla.");
                    return false;
                }

                // Estrai e assegna le proprietà del robot
                int speed = Convert.ToInt16(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_SPEED_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());
                float velocity = float.Parse(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_VELOCITY_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());
                float blend = float.Parse(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_BLEND_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());
                float acceleration = float.Parse(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_ACCELERATION_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());
                float ovl = float.Parse(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_OVL_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());
                int tool = Convert.ToInt16(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_TOOL_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());
                int user = Convert.ToInt16(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_USER_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());
                int weight = Convert.ToInt16(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_WEIGHT_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());
                int velRec = Convert.ToInt16(dt_robotProperties.Rows[RobotDAOSqlite.ROBOT_PROPERTIES_VELREC_ROW_INDEX]
                    [RobotDAOSqlite.ROBOT_PROPERTIES_VALUE_COLUMN_INDEX].ToString());

                // Creazione dell'oggetto robotProperties
                robotProperties = new RobotProperties(speed, velocity, blend, acceleration, ovl, tool, user, weight, velRec);

                log.Info($"SetRobotProperties completata: " +
                         $"\n Speed: {speed}" +
                         $"\n Velocity: {velocity}" +
                         $"\n Blend: {blend}" +
                         $"\n Acceleration: {acceleration}" +
                         $"\n Ovl: {ovl}" +
                         $"\n Tool: {tool}" +
                         $"\n User: {user}" +
                         $"\n Weight: {weight}" +
                         $"\n VelRec: {velRec}" );

                // Modifica delle variabili statiche e globali di RobotManager
                RobotManager.speed = robotProperties.Speed;
                RobotManager.vel = robotProperties.Velocity;
                RobotManager.acc = robotProperties.Acceleration;
                RobotManager.ovl = robotProperties.Ovl;
                RobotManager.blendT = robotProperties.Blend;
                RobotManager.tool = robotProperties.Tool;
                RobotManager.user = robotProperties.User;
                RobotManager.weight = robotProperties.Weight;
                RobotManager.velRec = robotProperties.VelRec;   

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Errore durante SetRobotProperties: " + ex.ToString());
                return false;
            }
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
        /// Generazione evento da allarmi resettati
        /// </summary>
        /// <param name="e"></param>
        protected static void OnAllarmeResettato(EventArgs e)
        {
            AllarmeResettato?.Invoke(null, e);
        }

        /// <summary>
        /// Generazione eventi
        /// </summary>
        public static void TriggerAllarmeGenerato()
        {
            OnAllarmeGenerato(EventArgs.Empty);
        }

        /// <summary>
        /// Trigger attivato quando vengono cancellati gli allarmi
        /// </summary>
        public static void TriggerAllarmeResettato()
        {
            OnAllarmeResettato(EventArgs.Empty);
        }

        /// <summary>
        /// Aggiorna il contatore che indica lo spostamento della catena
        /// </summary>
        public static void CheckChainPos() 
        {
            if (!stopChainUpdaterThread)
            {
                // Get del valore dello spostamento catena dal dizionario di variabili PLC
                chainPos = Convert.ToInt16(PLCConfig.appVariables.getValue(PLCTagName.Chain_Pos));
            }
            else
            {
                chainPos = 0;
            }
        }

        /// <summary>
        /// Usato per muovere il Robot al punto precedente/successivo della lista di punti
        /// </summary>
        /// <param name="pos">Punto da raggiungere</param>
        public static void MoveToPoint(DescPose pos) 
        {
            int result = robot.MoveCart(pos, tool, user, vel, acc, ovl, blendT, config);

            GetRobotMovementCode(result);
        }

        /// <summary>
        /// Esegue check su Robot enable
        /// </summary>
        public static void CheckIsRobotEnable()
        {
            // Controllo se il robot è abilitato tramite PLC
            isEnabledNow = Convert.ToBoolean(PLCConfig.appVariables.getValue(PLCTagName.Enable));

            if (isEnabledNow && !prevIsEnable)
            {
                // Abilitazione del robot
                robot.RobotEnable(1);
                prevIsEnable = true;
                prevIsNotEnable = false; // Resetta lo stato "non abilitato"
                AlarmManager.blockingAlarm = false;
                
                currentIndex = -1;
            }
            else if (!isEnabledNow && !prevIsNotEnable)
            {
                // Disabilitazione del robot
                JogMovement.StopJogRobotThread(); // Ferma il thread di Jog
                robot.RobotEnable(0);
                prevIsNotEnable = true;
                prevIsEnable = false; // Resetta lo stato "abilitato"
                prevIsManual = false;
                AlarmManager.blockingAlarm = true;
                pauseCycleRequested = false;
                currentIndex = -1;
                UC_FullDragModePage.debugCurrentIndex = -1;
                robot.StopMotion(); // Cancellazione della coda di punti
            }
        }

        /// <summary>
        /// Invia posizioni al PLC in formato cartesiano e joint
        /// </summary>
        /// <param name="jPos">Posizione in joint ottenuta dal calcolo di cinematica inversa partendo dalla posizione TCP</param>
        /// <param name="j1_actual_pos">Posizione del giunto 1</param>
        /// <param name="j2_actual_pos">Posizione del giunto 2</param>
        /// <param name="j3_actual_pos">Posizione del giunto 3</param>
        /// <param name="j4_actual_pos">Posizione del giunto 4</param>
        /// <param name="j5_actual_pos">Posizione del giunto 5</param>
        /// <param name="j6_actual_pos">Posizione del giunto 6</param>
        public static void SendRobotPositionToPLC(JointPos jPos, JointPos j1_actual_pos, JointPos j2_actual_pos, JointPos j3_actual_pos, 
            JointPos j4_actual_pos, JointPos j5_actual_pos, JointPos j6_actual_pos)
        {
            // Calcolo della posizione in joint eseguendo il calcolo di cinematica inversa
            robot.GetInverseKin(0, TCPCurrentPosition, -1, ref jPos);

            #region TCP

            // Scrittura posizione su asse x
            RefresherTask.AddUpdate(PLCTagName.x_actual_pos, TCPCurrentPosition.tran.x, "FLOAT");

            // Scrittura posizione su asse y
            RefresherTask.AddUpdate(PLCTagName.y_actual_pos, TCPCurrentPosition.tran.y, "FLOAT");

            // Scrittura posizione su asse z
            RefresherTask.AddUpdate(PLCTagName.z_actual_pos, TCPCurrentPosition.tran.z, "FLOAT");

            // Scrittura posizione su asse rx
            RefresherTask.AddUpdate(PLCTagName.rx_actual_pos, TCPCurrentPosition.rpy.rx, "FLOAT");

            // Scrittura posizione su asse ry
            RefresherTask.AddUpdate(PLCTagName.ry_actual_pos, TCPCurrentPosition.rpy.ry, "FLOAT");

            // Scrittura posizione su asse rz
            RefresherTask.AddUpdate(PLCTagName.rz_actual_pos, TCPCurrentPosition.rpy.rz, "FLOAT");

            #endregion

            #region Joint

            // Scrittura posizione giunto 1
            RefresherTask.AddUpdate(PLCTagName.j1_actual_pos, jPos.jPos[0], "FLOAT");

            // Scrittura posizione giunto 2
            RefresherTask.AddUpdate(PLCTagName.j2_actual_pos, jPos.jPos[1], "FLOAT");

            // Scrittura posizione giunto 3
            RefresherTask.AddUpdate(PLCTagName.j3_actual_pos, jPos.jPos[2], "FLOAT");

            // Scrittura posizione giunto 4
            RefresherTask.AddUpdate(PLCTagName.j4_actual_pos, jPos.jPos[3], "FLOAT");

            // Scrittura posizione giunto 5
            RefresherTask.AddUpdate(PLCTagName.j5_actual_pos, jPos.jPos[4], "FLOAT");

            // Scrittura posizione giunto 6
            RefresherTask.AddUpdate(PLCTagName.j6_actual_pos, jPos.jPos[5], "FLOAT");

            DescPose trackerPose = formTrackerCalibration.trackerTransformedCurrentPose;
            if (francesco)
            {
                string pos = $" \t{Math.Round(TCPCurrentPosition.tran.x,3)} \t{Math.Round(TCPCurrentPosition.tran.y,3)} \t{Math.Round(TCPCurrentPosition.tran.z,3)}" +
                $" \t{Math.Round(TCPCurrentPosition.rpy.rx,3)} \t{Math.Round(TCPCurrentPosition.rpy.ry, 3)} \t{Math.Round(TCPCurrentPosition.rpy.rz, 3)}";
                string track = $" \t{Math.Round(trackerPose.tran.x,3)} \t{Math.Round(trackerPose.tran.y, 3)} \t{Math.Round(trackerPose.tran.z, 3)}" +
                $" \t{Math.Round(trackerPose.rpy.rx, 3)} \t{Math.Round(trackerPose.rpy.ry, 3)} \t{Math.Round(trackerPose.rpy.rz, 3)}";

                string delta = $" \t{Math.Round(Math.Abs(TCPCurrentPosition.tran.x - trackerPose.tran.x),3)}" +
                    $" \t{Math.Round(Math.Abs(TCPCurrentPosition.tran.y - trackerPose.tran.y),3)}" +
                    $" \t{Math.Round(Math.Abs(TCPCurrentPosition.tran.z - trackerPose.tran.z),3)}" +
                    $" \t {Math.Round(Math.Abs(TCPCurrentPosition.rpy.rx - trackerPose.rpy.rx),3)}" +
                    $" \t{Math.Round(Math.Abs(TCPCurrentPosition.rpy.ry - trackerPose.rpy.ry), 3)}" +
                    $"  \t{Math.Round(Math.Abs(TCPCurrentPosition.rpy.rz - trackerPose.rpy.rz), 3)}";

                log.Info("Robot  : " + pos);
                log.Info("Tracker: " + track);
                log.Info("Delta  : " + delta);
            }

            #endregion
        }

        public static async Task TrackerCalibrationProcedure(CancellationToken token)
        {
            var restPose = ApplicationConfig.applicationsManager.GetPosition("pHome", "RM");
            DescPose pHome = new DescPose(restPose.x, restPose.y, restPose.z, restPose.rx, restPose.ry, restPose.rz);

            if(!isInHomePosition)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Posizionare il robot in home position prima di procedere");
                return;
            }

            int step = 0;
            int movementResult = -1;

            ExaxisPos epos = new ExaxisPos(0, 0, 0, 0); // Nessun asse esterno
            DescPose offset = new DescPose(0, 0, 0, 0, 0, 0); // Nessun offset
            byte offsetFlag = 0; // Flag per offset (0 = disabilitato)

            try
            {
                #region Punto origin

                JointPos jointStart = new JointPos(0, 0, 0, 0, 0, 0);
                DescPose descPosStart = new DescPose(0.775, -0.799, 0.728, 90.678, 0.858, -179.343);
                robot.GetInverseKin(0, descPosStart, -1, ref jointStart);

                #endregion

                #region Punto su x

                JointPos jointX = new JointPos(0, 0, 0, 0, 0, 0);
                DescPose descPosX = new DescPose(200, -0.799, 0.728, 90.678, 0.858, -179.343);
                robot.GetInverseKin(0, descPosX, -1, ref jointX);

                #endregion

                #region Punto su z

                JointPos jointZ = new JointPos(0, 0, 0, 0, 0, 0);
                DescPose descPosZ = new DescPose(0.775, -0.799, 200, 90.678, 0.858, -179.343);
                robot.GetInverseKin(0, descPosZ, -1, ref jointZ);

                #endregion

                await Task.Run(() => robot.SetSpeed(10));

                while (!token.IsCancellationRequested && !AlarmManager.blockingAlarm && step < 100)
                {
                    switch (step)
                    {
                        case 0:
                            #region Invio punto di origine

                            movementResult = robot.MoveL(jointStart, descPosStart, tool, user, vel, acc, ovl, blendT, epos, 0, offsetFlag, offset);
                            GetRobotMovementCode(movementResult);

                            inPosition = false;
                            endingPoint = descPosStart;

                            step = 5;

                            break;

                        #endregion

                        case 5:
                            #region Attesa in position punto di origine

                            if (inPosition) // Se il Robot è arrivato in posizione di Pick
                            {
                                step = 10;
                            }

                            break;

                        #endregion

                        case 10:
                            #region Invio punto su x

                            movementResult = robot.MoveL(jointX, descPosX, tool, user, vel, acc, ovl, blendT, epos, 0, offsetFlag, offset);
                            GetRobotMovementCode(movementResult);

                            inPosition = false;
                            endingPoint = descPosX;

                            step = 15;

                            break;

                        #endregion

                        case 15:
                            #region Attesa in position punto su x

                            if (inPosition) // Se il Robot è arrivato in posizione di Pick
                            {
                                step = 16;
                            }

                            break;

                        #endregion

                        case 16:
                            #region Ritorno punto su origine

                            movementResult = robot.MoveL(jointStart, descPosStart, tool, user, vel, acc, ovl, blendT, epos, 0, offsetFlag, offset);
                            GetRobotMovementCode(movementResult);

                            inPosition = false;
                            endingPoint = descPosStart;

                            step = 17;

                            break;

                        #endregion

                        case 17:
                            #region Attesa in position punto su x

                            if (inPosition) // Se il Robot è arrivato in posizione di Pick
                            {
                                step = 20;
                            }

                            break;

                        #endregion

                        case 20:
                            #region Invio punto su z

                            movementResult = robot.MoveL(jointZ, descPosZ, tool, user, vel, acc, ovl, blendT, epos, 0, offsetFlag, offset);
                            GetRobotMovementCode(movementResult);

                            inPosition = false;
                            endingPoint = descPosZ;

                            step = 25;

                            break;

                        #endregion

                        case 25:
                            #region Attesa in position punto su z

                            if (inPosition) // Se il Robot è arrivato in posizione di Pick
                            {
                                step = 30;
                            }

                            break;

                        #endregion

                        case 30:
                            #region Torna indietro di offset fisso

                            JointPos jointRetract = new JointPos(0, 0, 0, 0, 0, 0);
                            DescPose descPosRetract = TCPCurrentPosition;
                            descPosRetract.tran.y -= 250;
                            robot.GetInverseKin(0, descPosRetract, -1, ref jointRetract);

                            inPosition = false;
                            endingPoint = descPosRetract;

                            step = 35;

                            break;

                        #endregion

                        case 35:
                            #region Attesa in position punto su punto intermedio

                            if (inPosition) // Se il Robot è arrivato in posizione di Pick
                            {
                                step = 40;
                            }

                            break;

                        #endregion

                        case 40:
                            #region Start home routine

                            taskManager.AddAndStartTask(nameof(HomeRoutine), HomeRoutine, TaskType.Default, false);
                            inPosition = false;
                            endingPoint = pHome;

                            step = 41;

                            break;

                        #endregion

                        case 41:
                            #region Check home position

                            if (inPosition) 
                            {
                                step = 100;
                                inPosition = false;
                            }

                            break;

                            #endregion
                    }

                    await Task.Delay(100, token);
                }
                token.ThrowIfCancellationRequested();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Check su movimento del Robot
        /// </summary>
        /// <param name="updates"></param>
        public static void CheckIsRobotMoving(List<(string key, bool value, string type)> updates)
        {

            if (AlarmManager.isRobotConnected)
            {
                double[] coordNewTCPposition = {
                    Math.Round(TCPCurrentPosition.tran.x, 0),
                    Math.Round(TCPCurrentPosition.tran.y, 0),
                    Math.Round(TCPCurrentPosition.tran.z, 0),
                    Math.Round(TCPCurrentPosition.rpy.rx, 0),
                    Math.Round(TCPCurrentPosition.rpy.ry, 0),
                    Math.Round(TCPCurrentPosition.rpy.rz, 0)
                };

                double[] coordpreviousTCPposition = {
                    Math.Round(previousTCPposition.tran.x, 0),
                    Math.Round(previousTCPposition.tran.y, 0),
                    Math.Round(previousTCPposition.tran.z, 0),
                    Math.Round(previousTCPposition.rpy.rx, 0),
                    Math.Round(previousTCPposition.rpy.ry, 0),
                    Math.Round(previousTCPposition.rpy.rz, 0)
                };

                //TODO: è possibile aggiungere una tolleranza per ridurre ancora la quantità di allarmi generati

                // Confronta gli array arrotondati
                bool sonoUguali = coordNewTCPposition.SequenceEqual(coordpreviousTCPposition);

                if (sonoUguali)
                {
                    if (AlarmManager.isRobotMoving)
                    {
                        AlarmManager.isRobotMoving = false;
                        RobotIsMoving?.Invoke(false, EventArgs.Empty);
                        robotMovingStartTime = null; // Resetta il timer
                    }
                }
                else
                {
                    if (!AlarmManager.isRobotMoving)
                    {
                        // Quando il robot inizia a muoversi, avvia il timer
                        if (robotMovingStartTime == null)
                        {
                            robotMovingStartTime = DateTime.Now;
                        }
                        else if ((DateTime.Now - robotMovingStartTime.Value).TotalSeconds > 2)
                        {
                            // Invoca l'evento solo dopo 1 secondo
                            AlarmManager.isRobotMoving = true;
                            RobotIsMoving?.Invoke(true, EventArgs.Empty);
                            robotMovingStartTime = null; // Resetta il timer dopo l'invocazione
                        }
                    }
                    else
                    {
                        robotMovingStartTime = null; // Resetta il timer se torna falso
                    }
                }
            }

            // Aggiorna la posizione TCP precedente con la posizione TCP attuale
            previousTCPposition.tran.x = TCPCurrentPosition.tran.x;
            previousTCPposition.tran.y = TCPCurrentPosition.tran.y;
            previousTCPposition.tran.z = TCPCurrentPosition.tran.z;
            previousTCPposition.rpy.rx = TCPCurrentPosition.rpy.rx;
            previousTCPposition.rpy.ry = TCPCurrentPosition.rpy.ry;
            previousTCPposition.rpy.rz = TCPCurrentPosition.rpy.rz;
        
    }

        /// <summary>
        /// Approssima i valori delle posizioni a n cifre decimali
        /// </summary>
        /// <param name="dp">Contiene il riferimento allo struct che contiene i valori da approssimare</param>
        /// <param name="digits">Numero di cifre decimali desiderate</param>
        public static void RoundPositionDecimals(ref DescPose dp, int digits)
        {
            dp.tran.x = Math.Round(dp.tran.x, digits);
            dp.tran.y = Math.Round(dp.tran.y, digits);
            dp.tran.z = Math.Round(dp.tran.z, digits);
            dp.rpy.rx = Math.Round(dp.rpy.rx, digits);
            dp.rpy.ry = Math.Round(dp.rpy.ry, digits);
            dp.rpy.rz = Math.Round(dp.rpy.rz, digits);
        }

        /// <summary>
        /// Metodo che ferma il robot e cancella la coda di punti
        /// </summary>
        public static void ClearRobotQueue()
        {
            AlarmManager.isRobotMoving = false;
            robot.PauseMotion();
            robot.StopMotion();
        }

        /// <summary>
        /// Creazione di un allarme quando il robot si ferma
        /// </summary>
        /// <param name="id">ID allarme</param>
        /// <param name="description">Descrizione allarme</param>
        /// <param name="timestamp">Timestamp allarme</param>
        /// <param name="device">Device da cui deriva l'allarme</param>
        /// <param name="state">ON-OFF</param>
        public static void CreateRobotAlarm(string id, string description, string timestamp, string device,string state)
        {
            // Solleva l'evento quando il robot si ferma
            OnRobotAlarm(new RobotAlarmsEventArgs(id, description, timestamp, device, state));
        }

        /// <summary>
        /// Metodo che aggiunge alla lista degli allarmi l'allarme
        /// </summary>
        /// <param name="e"></param>
        public  static void OnRobotAlarm(RobotAlarmsEventArgs e)
        {
            // Calcola il timestamp Unix in millisecondi
            long unixTimestamp = ((DateTimeOffset)Convert.ToDateTime(e.Timestamp)).ToUnixTimeMilliseconds();

            RobotDAO.SaveRobotAlarm(ConnectionString, Convert.ToInt32(e.Id), e.Description,
                unixTimestamp.ToString(), e.Device, e.State);
            formAlarmPage.AddAlarmToList(e.Id, e.Description, e.Timestamp, e.Device, e.State);
            TriggerAllarmeGenerato();

        }

        /// <summary>
        /// Metodo che mette in pausa il Robot
        /// </summary>
        public static void PauseMotion()
        {
            robot.PauseMotion();
            log.Warn("Il robot è stato messo in pausa");
        }

        /// <summary>
        /// Metodo che riprende movimento Robot
        /// </summary>
        public static void ResumeMotion()
        {
            robot.ResumeMotion();
        }

        /// <summary>
        /// Metodo che porta il Robot in HomePosition
        /// </summary>
        public static void GoToHomePosition()
        {
           // robot.SetSpeed(10);
            var restPose = ApplicationConfig.applicationsManager.GetPosition("pHome", "RM");
            DescPose pHome = new DescPose(restPose.x, restPose.y, restPose.z, restPose.rx, restPose.ry, restPose.rz);
            int result = robot.MoveCart(pHome, tool, user, 5, 5, ovl, blendT, config);

            GetRobotMovementCode(result);

            robot.SetSpeed(speedRobot);
        }
        
        /// <summary>
        /// Metodo per reset errori Robot
        /// </summary>
        public static void ClearRobotAlarm()
        {
           int err = robot.ResetAllError();
        }

        /// <summary>
        /// Imposta la modalità operativa del robot: 
        /// <para>0 = automatico</para>
        /// <para>1 = manuale</para>
        /// </summary>
        /// <param name="mode"></param>
        public static void SetRobotMode(int mode)
        {
            if(mode != 0 && mode != 1)
                return;

            robot.Mode(mode);
            isAutomaticMode = mode == 0;
        }

        #endregion

        #region Metodi per il debug

        /// <summary>
        /// Metodo che stoppa modalità teaching Point to Point
        /// </summary>
        public static void StopTeachingPTP()
        {
            byte state = new byte();
            DescPose pos = new DescPose();
            string guid_pos;
            string guid_gun_settings;
            GunSettings gunSettings;
            int id_gun_settings;

            // Se il robot è in TeachingMode, stoppo la modalità di teaching
            if (robot.IsInDragTeach(ref state) == 0 && state == 1)
            {
                /*
                robot.SetWebTPDStop();
                robot.DragTeachSwitch(0);*/

                // Salvo le posizioni registrate
                robot.GetActualTCPPose(flag, ref pos);

                RoundPositionDecimals(ref pos, 3);

                guid_pos = Guid.NewGuid().ToString();
                guid_gun_settings = Guid.NewGuid().ToString();
                id_gun_settings = UC_FullDragModePage.pointIndex + 1;

                gunSettings = new GunSettings(guid_gun_settings, guid_pos, id_gun_settings, 100, 100, 100, 100, 100, 1, RobotManager.applicationName);

                positionToSend = new PointPosition(guid_pos, pos, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"), "PTP", "", gunSettings); //$
                //positionsToSend.Add(point);
                positionsToSave.Add(positionToSend);
            }
        }

        /// <summary>
        /// Metodo che avvia modalità teaching Point to Point
        /// </summary>
        public static void StartTeachingPTP()
        {
            //int type = 1;
            //string name = "testTeach1";
            //int period_ms = 2;
            //UInt16 di_choose = 0;
            //UInt16 do_choose = 0;
            /*
            robot.SetTPDParam(type, name, period_ms, di_choose, do_choose);

            robot.Mode(1);
            //Thread.Sleep(1000);
            robot.DragTeachSwitch(1);
            robot.SetTPDStart(type, name, period_ms, di_choose, do_choose);*/
        }

        /// <summary>
        /// Metodo che lancia Thread per salvataggio posizioni in LinearDragMode
        /// </summary>
        /// <param name="application"></param>
        public static void StartTeachingLineare()
        {
            //int type = 1;
            //string name = "testTeach1";
            //int period_ms = 2;
            //UInt16 di_choose = 0;
            //UInt16 do_choose = 0;

            //robot.SetTPDParam(type, name, period_ms, di_choose, do_choose);
            //robot.Mode(1);
            //Thread.Sleep(1000);
            //robot.DragTeachSwitch(1);
            //robot.SetTPDStart(type, name, period_ms, di_choose, do_choose);
            //robot.SetLoadWeight(0);
            // Faccio partire il thread
            timerCallback = new TimerCallback(ExecLinearDragMode);
            timer = new System.Threading.Timer(timerCallback, null, 100, velRec);
        }

        /// <summary>
        /// Metodo che lancia Thread per salvataggio posizioni in LinearDragMode
        /// </summary>
        /// <param name="application"></param>
        public static void StartTeachingTracker()
        {
            if (formTrackerCalibration.isCalibrated)
            {
                // Faccio partire il thread
                timerCallback = new TimerCallback(ExecTrackerTeachingModeByCoord);
                timer = new System.Threading.Timer(timerCallback, null, 100, velRec);
            }
            else
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Effettuare prima la calibrazione");
            }
        }

        /// <summary>
        /// Metodo che stoppa modalità LinearDragMode
        /// </summary>
        /// <param name="application"></param>
        public static void StopTeachingLineare()
        {
            // Fermo il thread
            timer.Change(0, 0);

            //byte state = new byte();

            //Se il robot è in TeachingMode, stoppo la modalità di teaching
            //if (robot.IsInDragTeach(ref state) == 0 && state == 1)
            //{
            //    robot.SetWebTPDStop();
            //    robot.DragTeachSwitch(0);
            //}
        }

        /// <summary>
        /// Metodo che salva le posizioni nella lista di posizioni
        /// </summary>
        /// <param name="obj"></param>
        private static void ExecLinearDragMode(object obj)
        {
            // soglia per fare i controlli di in position 
            int threshold = 20; // 20 mm
            // Checker usato per fare in modo da non averne punti abbastanza simili tra loro risolvendo il
            // problema della linear drag mode
            PositionChecker checkerPos = new PositionChecker(threshold);
            // Quando i 2 punti sono simili sarà True
            bool isPositionMatch = false;

            // Dichiarazione ending point
            DescPose pos = new DescPose();
            // Oggetto gun settings
            GunSettings gunSettings;
            // ID dell'oggetto gun settings
            int id_gun_settings;
            // Guid posizione
            string guid_pos;
            // Guid gun settings della posizione
            string guid_gun_settings;

            // Salvo le posizioni registrate
            robot.GetActualTCPPose(flag, ref pos);
            // Arrotondamento a 3 cifre decimali
            RoundPositionDecimals(ref pos, 3);

            // Se ci sono dei punti salvati allora posso usare l'ultimo punto per fare il controllo inPosition
            if(positionsToSave.Count > 0)
                isPositionMatch = checkerPos.IsInPosition(pos, positionsToSave.Last().position);
            
            // Se le posizioni sono diverse le salvo e genero l'evento per la list view
            if(!isPositionMatch)
            {
                // Generazione del guid posizione
                guid_pos = Guid.NewGuid().ToString();
                // Generazione del guid gun settings
                guid_gun_settings = Guid.NewGuid().ToString();
                // Assegnazione del nuovo ID, l'ID del gun settings è uguale all'ID della posizione
                id_gun_settings = UC_FullDragModePage.pointIndex + 1;
                // Creazione oggetto gun settings
                gunSettings = new GunSettings(guid_gun_settings, guid_pos, id_gun_settings, 100, 100, 100, 100, 100, 1, applicationName);

                // Creazione del punto da inviare
                positionToSend = new PointPosition(guid_pos, pos, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"), "Linear", "", gunSettings);
                // Invoco l'evento per aggiungere la nuova riga nella list view positions
                PointPositionAdded?.Invoke(null, EventArgs.Empty);
                // Aggiungo la nuova posizione nella lista delle posizioni da salvare
                //positionsToSave.Add(new PointPosition(guid_pos, pos, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"), "Linear", "", gunSettings));
                positionsToSave.Add(positionToSend);
            }
        }

        /// <summary>
        /// Funzione per normalizzare l'angolo tra -180° e 180°
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private static float NormalizeAngle(float angle)
        {
            while (angle > 180) angle -= 360;
            while (angle <= -180) angle += 360;
            return angle;
        }

        /// <summary>
        /// Metodo che salva le posizioni trasformate nella lista di posizioni
        /// </summary>
        /// <param name="obj"></param>
        private static void ExecTrackerTeachingModeByCoord(object obj)
        {
            int threshold = 5; // 10 mm
            PositionChecker checkerPos = new PositionChecker(threshold);
            bool isPositionMatch = false;

            GunSettings gunSettings;
            int id_gun_settings;
            string guid_pos;
            string guid_gun_settings;

            DescPose poseToSend = formTrackerCalibration.trackerTransformedCurrentPose;


            RoundPositionDecimals(ref poseToSend, 3);

            if (positionsToSave.Count > 0)
                isPositionMatch = checkerPos.IsInPosition(poseToSend, positionsToSave.Last().position);

            if (!isPositionMatch)
            {
                guid_pos = Guid.NewGuid().ToString();
                guid_gun_settings = Guid.NewGuid().ToString();
                id_gun_settings = UC_FullDragModePage.pointIndex + 1;

                gunSettings = new GunSettings(guid_gun_settings, guid_pos, id_gun_settings,
                    100, 100, 100, 100, 100, 1, applicationName);

                positionToSend = new PointPosition(guid_pos, poseToSend,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"),
                    "Linear", "", gunSettings);

                PointPositionAdded?.Invoke(null, EventArgs.Empty);

                positionsToSave.Add(positionToSend);
            }
            
        }

        #endregion
    }
}