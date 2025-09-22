using fairino;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RMLib.DataAccess;
using RMLib.Logger;
using RMLib.Utils;
using RMLib.MessageBox;
using RMLib.Translations;
using RM.src.RM240513.Forms.Plant.DragMode;
using RMLib.Alarms;
using RM.Properties;
using RM.src.RM240513.Classes.FR20.Applications.Application;
using System.Linq;
using RM.src.RM240513.Forms.ScreenSaver;
using RMLib.PLC;
using RM.src.RM240513.Classes.PLC;

namespace RM.src.RM240513.Forms.DragMode
{
    /// <summary>
    /// Pagina dedicata alla modalità di drag, utile per acquisire nuovi punti, visualizzarli e salvarli sul database per una specifica 
    /// applicazione. Oltre a questo permette il debug dei movimenti: tramite appositi comandi è possibile infatti muoversi tra i punti
    /// presi e se necessario modificarli.
    /// <br>Istruzioni:</br>
    /// <br></br>
    /// -Selezionare l'applicazione desiderata prima di procedere dalla apposita tabella.
    /// <br></br>
    /// -Selezionare la tabella dei punti (che ora conterrà i punti dell'applicazione scelta) per visualizzare i punti dell'applicazione.
    /// <br></br>
    /// -Aprire il pannello dei tool (tasto a destra) e usare gli strumenti di debug come avanti, indietro, modifica e sovrascrivi.
    /// </summary>
    public partial class UC_FullDragModePage : UserControl
    {
        #region Proprietà 

        #region Variabili per la connessione al DB
        // Variabili per la connessione al database
        private static readonly RobotDAOSqlite robotDAO = new RobotDAOSqlite();
        private static readonly SqliteConnectionConfiguration DatabaseConnection = new SqliteConnectionConfiguration();
        private static readonly string ConnectionString = DatabaseConnection.GetConnectionString();
        #endregion

        /// <summary>
        /// Variabile per usare i messaggi di log
        /// </summary>
        private static readonly log4net.ILog log = LogHelper.GetLogger(); // Logger

        /// <summary>
        /// Posizioni Robot salavate nel database
        /// </summary>
        private readonly List<PointPosition> savedPositions = new List<PointPosition>();

        /// <summary>
        /// Nome dell'applicazione su cui verranno salvati i punti
        /// </summary>
        private string applicationName = "";

        /// <summary>
        /// Specifica se il robot è attualmente in drag mode o se la drag mode sta per partire
        /// </summary>
        private bool isDragStart = false;

        /// <summary>
        /// Indica l'indice del punto nella traiettoria
        /// </summary>
        public static int pointIndex = 0;

        /// <summary>
        /// Indica l'indice del punto corrente nel debug
        /// </summary>
        public static int debugCurrentIndex = -1;

        /// <summary>
        /// Booleano settato a true se è stata richiesta la modifica di una posizione all'interna della lista di posizioni
        /// </summary>
        public static bool positionUpdateRequested = false;

        /// <summary>
        /// Indica l'indice dell'ultima riga aggiornata
        /// </summary>
        private short updateRowIndex = 0;

        /// <summary>
        /// Id della posizione Robot modificata
        /// </summary>
        public static int idPositionUpdated;

        /// <summary>
        /// Numero di righe della listView con le quali fare il confronto delle posizioni
        /// </summary>
        public static int maxRowsToCompare = 5;

        /// <summary>
        /// Form contenente strumenti per debug
        /// </summary>
        private FormDebugTools formDebugTools;

        /// <summary>
        /// Form contenente strumenti per modificare le impostazioni del robot
        /// </summary>
        private FormDebugSettings formDebugSettings;

        /// <summary>
        /// Form contenente strumenti per visualizzare e modificare le proprietà della pistola di uno o più punti
        /// </summary>
        private FormGunSettigs formGunSettings;

        /// <summary>
        /// Oggetto per gestire pausa e ripresa thread di monitoring
        /// </summary>
        private readonly ManualResetEvent pauseEvent = new ManualResetEvent(true);

        /// <summary>
        /// Thread di monitoring
        /// </summary>
        private static Thread monitoringThread;

        /// <summary>
        /// Contatore di posizioni lette in thread di monitoring
        /// </summary>
        public static int contMonitoring = 0;

        /// <summary>
        /// A true quando fermo thread di monitoring
        /// </summary>
        public static bool stopMonitoring = false;

        /// <summary>
        /// Richiesta per fermare il monitoring e di conseguenza anche l'applicazione in corso
        /// </summary>
        private bool stopMonitoringRequest = false;

        private bool pauseMonitoringRequest = false;

        /// <summary>
        /// Step del thread monitoring
        /// </summary>
        public static int monitoringStep = 0;

        /// <summary>
        /// A true quando il thread monitoring è avviato
        /// </summary>
        public static bool monitoringThreadStarted = false;

        /// <summary>
        /// A true quando viene richiesto di fermare il thread
        /// </summary>
        private static bool stopCheckMovement = true;

        /// <summary>
        /// Indica se le colonne delle proprietà della pistola sono mostrate o se lo sono quelle delle posizioni
        /// </summary>
        private bool gunSettingsColumnsShowed = true;

        /// <summary>
        /// Evento lanciato quando viene fatto save operation
        /// </summary>
        public static event EventHandler PositionListUpdated;

        #region Colori

        /// <summary>
        /// Colore associato alle righe lw per i nuovi punti presi in drag mode
        /// </summary>
        private readonly Color NewPointColor = Color.SeaGreen;
        /// <summary>
        /// Colore associato alle righe lw per i punti da modificare
        /// </summary>
        private readonly Color ModifyPointColor = Color.DarkTurquoise;
        /// <summary>
        /// Colore associato alle righe lw per i punti da eliminare
        /// </summary>
        private readonly Color DeletePointColor = Color.Crimson;
        /// <summary>
        /// Colore associato alle righe lw per gli spostamenti verso un punto
        /// </summary>
        private readonly Color MoveToPointColor = Color.Orange;
        /// <summary>
        /// Colore associato alle righe lw per i punti presi e salvati
        /// </summary>
        private readonly Color GenericPointColor = Color.White;
        /// <summary>
        /// Colore associato alle righe lw per il punto corrente in cui ci si trova
        /// </summary>
        private readonly Color CurrentPointColor = Color.Silver;
        /// <summary>
        /// Colore associato alle righe lw per gli elementi che sono stati selezionati per la modifica delle variabili pistola
        /// </summary>
        private readonly Color ModifyGunPointColor = Color.Gold;

        #endregion

        #region Debug threads delay

        /// <summary>
        /// Delay tra lo stop e il pause del robot
        /// </summary>
        private const short DelayMonitoringStopPause = 500;
        /// <summary>
        /// Delay nel thread monitoring
        /// </summary>
        private const short monitoringRefreshPeriod = 20;
        /// <summary>
        /// Delay nel thread Single/Multiple movement
        /// </summary>
        private const short DelayMovement = 50;

        #endregion

        #region Varabili selezione righe modifica gun settings multipla
        /// <summary>
        /// Primo elemento della modifica multipla
        /// </summary>
        private int initialIndex = 0;
        /// <summary>
        /// Ultimo elemento della modifica multipla
        /// </summary>
        private int finalIndex = 0;
        #endregion

        #endregion

        /// <summary>
        /// Costruttore UC_FullDragModePage
        /// </summary>
        public UC_FullDragModePage()
        {
            InitializeComponent();
            pointIndex = 0;
            InitView();
            Fill_lw_positions();

            // Mostro le colonne della pistola
            ShowHiddenColumns(gunSettingsColumnsShowed);

            lbl_choosenApplication.Text = RobotManager.applicationName;
        }

        #region Metodi della form

        private void UC_FullDragModePage_Load(object sender, EventArgs e)
        {
            // Collegamento evento ApplicationAdded del dizionario al metodo HandleDictionaryPositionDeleted
            ApplicationConfig.applicationsManager.ApplicationPositionDeleted +=
                HandleDictionaryPositionDeleted;

            // Collegamento evento ApplicationAdded del dizionario al metodo HandleDictionaryPositionDeletetStartingFromId
            ApplicationConfig.applicationsManager.ApplicationPositionDeletedStartingFromId +=
                HandleDictionaryPositionDeletedStartingFromId;

            // Collegamento evento ApplicationAdded del dizionario al metodo HandleDictionaryPositionDeletetFromId
            ApplicationConfig.applicationsManager.ApplicationPositionDeletedFromId +=
                HandleDictionaryPositionDeletedFromId;

            RobotManager.PointPositionAdded += AddPositionToListView;
            RobotManager.EnableDragModeButtons += RobotManager_EnableDragModeButtonEvent;
            RobotManager.RequestedStartTeach += StartDrag;
            RobotManager.RequestedStopTeach += StopDrag;
            RobotManager.isUcDragModeDisplayed = true;

            ScreenSaverManager.AutoAddClickEvents(this);
        }

        private void RobotManager_EnableDragModeButtonEvent(object sender, EventArgs e)
        {
            contMonitoring = 0;
            monitoringStep = 0;
            monitoringThreadStarted = false;
            stopMonitoringRequest = true; // Richiesta di stop del thread di monitoring
            stopPositionCheckerThread = true; // Richiesta di stop del thread che colora la lw_positions durante monitoring
            RobotManager.stopChainUpdaterThread = true; // Richiesta di stop del thread che gestisce contatore spostamento catena

            if (debugCurrentIndex > -1)
                lw_positions.Items[debugCurrentIndex].BackColor = GenericPointColor;
            if (debugCurrentIndex < lw_positions.Items.Count - 1)
                lw_positions.Items[debugCurrentIndex + 1].BackColor = GenericPointColor;

            debugCurrentIndex = -1;
            UpdateDebugLabels();

            formDebugTools.EnableMonitoringButtons(false);
            HideSettingsPanel(false);
        }


        /// <summary>
        /// Inizializza le form per i comandi di debug, si iscrive ai loro eventi e inizializza
        /// la prima riga selezionata
        /// </summary>
        private void InitDebug()
        {
            formDebugTools = new FormDebugTools();
            formDebugSettings = new FormDebugSettings();  
            formGunSettings = new FormGunSettigs(lw_positions);

            formDebugTools.ButtonPreviousClicked += PreviousPoint;
            formDebugTools.ButtonNextClicked += NextPoint;
            formDebugTools.ButtonGoToClicked += GoToPoint;
            formDebugTools.ButtonStartMonitoringClicked += StartMonitoring;
            formDebugTools.ButtonPauseMonitoringClicked += PauseMonitoring;
            formDebugTools.ButtonStopMonitoringClicked += StopMonitoring;
            //formDebugTools.ButtonResumeMonitoringClicked += ResumeMonitoring;
            formDebugTools.ButtonModifyClicked += ModifyPoint;
            formDebugTools.ButtonOverwriteClicked += OverwritePoints;
            formDebugTools.ButtonDeleteClicked += DeletePoint;

            formDebugSettings.PTPModeCheckedChanged += ChangeDragModePTP;
            formDebugSettings.LinearModeCheckedChanged += ChangeDragModeLinear;
            formDebugSettings.TrackerModeCheckedChanged += ChangeDragModeTracker;

            formGunSettings.InitialPointIndexChanged += InitialIndexGunSettingsChanged;
            formGunSettings.FinalPointIndexChanged += FinalIndexGunSettingsChanged;
            formGunSettings.GunSettingsUpdated += GunSettingsUpdated;

            if (debugCurrentIndex >= 0 && lw_positions.Items.Count > 0)
            {
                lw_positions.Items[debugCurrentIndex].BackColor = CurrentPointColor;
            }
        }

        /// <summary>
        /// Inizializza alcuni elementi grafici della schermata e iscrive form agli eventi
        /// </summary>
        private void InitView()
        {
            Size = new Size(1024, 658);
            //cts = new CancellationTokenSource();
            FormHomePage.Instance.LabelHeader = "TEACH MODE";

            switch (lblDragMode.Text)
            {
                case "Point to point":
                    RobotManager.dragMode = 0;
                    break;
                case "Linear":
                    RobotManager.dragMode = 1;
                    break;
                case "Tracker":
                    RobotManager.dragMode = 2;
                    break;
            }

            SetHeight(lw_positions, 25);

           // InitFont();

            DisableOperationButtons();
        }

        /// <summary>
        /// Metodo che setta Font
        /// </summary>
        private void InitFont()
        {
            lw_positions.Font = ProjectVariables.FontListView;
        }

        /// <summary>
        /// Serve per aumentare la grandezza delle righe di una list view.
        /// NB: può solo aumentare la grandezza e bisogna stare attenti al SO perchè è in base a quello che
        /// cambia l'altezza delle righe.
        /// </summary>
        /// <param name="listView">riferimento alla lw</param>
        /// <param name="height">altezza supplementare specificata</param>
        private void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList
            {
                ImageSize = new Size(1, height)
            };
            listView.SmallImageList = imgList;
        }

        private void DisableOperationButtons()
        {
            btn_saveOperation.Visible = false;
            btn_cancelOperation.Visible = false;
            lbl_saveOperation.Visible = false;
            lbl_cancelOperation.Visible = false;
        }

        private void EnableOperationButtons()
        {
            btn_saveOperation.Visible = true;
            btn_cancelOperation.Visible = true;
            lbl_saveOperation.Visible = true;
            lbl_cancelOperation.Visible = true;
        }

        /// <summary>
        /// Determina se abilitare o disabilitare i controlli per il debug. Essi saranno attivi solo se ci sono elementi nella lw e se non è in drag al
        /// momento.
        /// </summary>
        private void EnableScreenElements()
        {
            // start/stop - abilitati uno alla volta
            //btn_STOP.Enabled = isDragStart;
            //btn_PLAY.Enabled = !isDragStart;
            lbl_Monitor.Text = isDragStart ? "Teach mode: ON" : "Teach mode: OFF";
            pb_LoadingGif.Visible = isDragStart;
            // debug
            btn_cancelOperation.Enabled = !isDragStart;
            btn_saveOperation.Enabled = !isDragStart;
            formDebugTools.EnableButtons(isDragStart);
        }

        /// <summary>
        /// Mostra una schermata di caricamento per tot tempo
        /// </summary>
        public void ShowLoadingScreen()
        {

        }

        /// <summary>
        /// Effettua dei controlli prima di chiudere la pagina, tornare alla home o tornare indietro.
        /// Non è possibile chiudere la pagina se si è in drag mode.
        /// Si richiede all'utente se vuole comunque uscire senza aver salvato i punti presi.
        /// </summary>
        /// <returns>True quando è possibile uscire, False quando l'uscita dà problemi</returns>
        private bool ClosingControls()
        {
            // Non si può uscire durante la drag mode attiva
            if (isDragStart) 
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Disattiva la drag mode prima di uscire");
                return false;
            }
            // Non si può uscire se il robot è in movimento, previene i controlli sui comandi come monitorig, next, goto ecc...
            else if (AlarmManager.isRobotMoving)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Aspetta che il robot finisca il movimento");
                return false;
            }
            // Non si può uscire se ci sono dei punti non salvati
            else if (RobotManager.positionsToSave.Count > 0) // Se ci sono elementi nella lw significa che tornando indietro li perdi
            {
                if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Tornando indietro perderai i punti presi non salvati. " +
                    "\n\n Vuoi uscire?") != DialogResult.OK)
                {
                    return false; // Se l'utente sceglie cancel allora rimarrà nella form
                }
            }

            // RobotManager.positionsToSend.Clear();
            return true;
        }

        /// <summary>
        /// Chiude la pagina tornando alla homepage, si disiscrive dagli eventi e termina i thread del debug e drag mode
        /// </summary>
        private void CloseForm()
        {
            log.Info("Richiesta chiusura pagina drag/debug mode");

            // Controllo che i requisiti per chiudere la pagina siano verificati
            if (!ClosingControls())
            {
                log.Info("Chiusura pagina annullata");
                return;
            }

            // Resetto le liste di punti presi
            positionUpdateRequested = false;
            RobotManager.positionsToSend.Clear();
            RobotManager.positionsToSave.Clear();

            // Ferma il task di movimento del robot
            stopCheckMovement = true;

            // Ferma il thread monitoring
            if (monitoringThreadStarted)
            {
                stopMonitoring = true;
                monitoringThreadStarted = false;
                monitoringThread.Join();

                RobotManager.ClearRobotAlarm();
                RobotManager.ClearRobotQueue();
            }

            monitoringStep = 0;
            contMonitoring = 0;

            // Disiscrivi gli eventi prima di chiamare Dispose 
            ApplicationConfig.applicationsManager.ApplicationPositionDeleted -= HandleDictionaryPositionDeleted;
            ApplicationConfig.applicationsManager.ApplicationPositionDeletedStartingFromId -= HandleDictionaryPositionDeletedStartingFromId;

            formDebugTools.ButtonPreviousClicked -= PreviousPoint;
            formDebugTools.ButtonNextClicked -= NextPoint;
            formDebugTools.ButtonGoToClicked -= GoToPoint;
            formDebugTools.ButtonStartMonitoringClicked -= StartMonitoring;
            formDebugTools.ButtonPauseMonitoringClicked -= PauseMonitoring;
            formDebugTools.ButtonStopMonitoringClicked -= StopMonitoring;
            //formDebugTools.ButtonResumeMonitoringClicked -= ResumeMonitoring;
            formDebugTools.ButtonModifyClicked -= ModifyPoint;
            formDebugTools.ButtonOverwriteClicked -= OverwritePoints;
            formDebugTools.ButtonDeleteClicked -= DeletePoint;

            formDebugSettings.PTPModeCheckedChanged -= ChangeDragModePTP;
            formDebugSettings.LinearModeCheckedChanged -= ChangeDragModeLinear;
            formDebugSettings.TrackerModeCheckedChanged -= ChangeDragModeTracker;
            RobotManager.PointPositionAdded -= AddPositionToListView;

            formGunSettings.InitialPointIndexChanged -= InitialIndexGunSettingsChanged;
            formGunSettings.FinalPointIndexChanged -= FinalIndexGunSettingsChanged;
            formGunSettings.GunSettingsUpdated -= GunSettingsUpdated;

            RobotManager.EnableDragModeButtons -= RobotManager_EnableDragModeButtonEvent;

            RobotManager.RequestedStartTeach -= StartDrag;
            RobotManager.RequestedStopTeach -= StopDrag;
            RobotManager.isUcDragModeDisplayed = false;

            formDebugTools.Close();
            formDebugSettings.Close();
            formGunSettings.Close();

            Dispose();
            log.Info("Operazioni di chiusura della pagina drag/debug completate, mostro la home page");
            FormHomePage.Instance.LabelHeader = TranslationManager.GetTranslation("LBL_HOMEPAGE_HEADER");
            FormHomePage.Instance.PnlContainer.Controls["UC_HomePage"].BringToFront();
            FormHomePage.Instance.PnlContainer.Controls.RemoveByKey("UC_FullDragModePage");
        }

        /// <summary>
        /// Permette di visualizzare le colonne relative alle posizioni o le colonne relative alle proprietà della pistola
        /// </summary>
        /// <param name="showGunSettings"></param>
        private void ShowHiddenColumns(bool showGunSettings)
        {
            // positions
            columnHeader4.Text = showGunSettings ? "" : "X";
            columnHeader5.Text = showGunSettings ? "" : "Y";
            columnHeader6.Text = showGunSettings ? "" : "Z";
            columnHeader7.Text = showGunSettings ? "" : "RX";
            columnHeader8.Text = showGunSettings ? "" : "RY";
            columnHeader9.Text = showGunSettings ? "" : "RZ";

            columnHeader4.Width = showGunSettings ? 0 : 100;
            columnHeader5.Width = showGunSettings ? 0 : 100;
            columnHeader6.Width = showGunSettings ? 0 : 100;
            columnHeader7.Width = showGunSettings ? 0 : 100;
            columnHeader8.Width = showGunSettings ? 0 : 100;
            columnHeader9.Width = showGunSettings ? 0 : 100;

            // gun settings
            columnHeader10.Text = showGunSettings ? "Feed air" : "";
            columnHeader11.Text = showGunSettings ? "Dosage air" : "";
            columnHeader12.Text = showGunSettings ? "Gun air" : "";
            columnHeader13.Text = showGunSettings ? "μ Ampere" : "";
            columnHeader14.Text = showGunSettings ? "K Volt" : "";
            columnHeader15.Text = showGunSettings ? "Status" : "";

            columnHeader10.Width = showGunSettings ? 100 : 0;
            columnHeader11.Width = showGunSettings ? 100 : 0;
            columnHeader12.Width = showGunSettings ? 100 : 0;
            columnHeader13.Width = showGunSettings ? 100 : 0;
            columnHeader14.Width = showGunSettings ? 100 : 0;
            columnHeader15.Width = showGunSettings ? 100 : 0;

            btn_showColumns.BackgroundImage = showGunSettings ? Resources.positions : Resources.airbrush;
        }

        /// <summary>
        /// Rende visibili i controlli di debug
        /// </summary>
        private void OpenDebugTools()
        {
            // Se la form era già visibile, la rendo invisibile
            if (formDebugTools.Visible)
            {
                //btn_debugTools.BorderSize = 2;
                formDebugTools.Visible = false;
            }
            else // Altrimenti rendo visibile la form
            {
                // Controllo che la variabile interna sia a false, in quel caso significa che devo fare show
                if (!formDebugTools.isShown)
                {
                    formDebugTools.Location = new Point(565, 130);
                    formDebugTools.Show();
                    formDebugTools.isShown = true;
                }
                formDebugTools.Visible = true;


                DecolorMultipleSelectedRows();

                // Rendo invisibile l'altra form
                if (formDebugSettings.Visible)
                {
                    formDebugSettings.Visible = false;
                }
                // Rendo invisibile l'altra form
                if (formGunSettings.Visible)
                {
                    formGunSettings.Visible = false;
                }

                // Modifico i bordi dei pulsanti
               // btn_debugTools.BorderSize = 4;
               // btn_debugSettings.BorderSize = 2;
               // btn_gunSettings.BorderSize = 2;
            }
        }

        /// <summary>
        /// Rende visibili i controlli per la gestine delle proprietà del robot durante il debug
        /// </summary>
        private void OpenDebugSettings()
        {
            // Se la form è visibile la rendo invisibile
            if (formDebugSettings.Visible)
            {
               // btn_debugSettings.BorderSize = 2;
                formDebugSettings.Visible = false;
            }
            else // Altrimenti la rendo visibile
            {
                // Controllo che la variabile interna sia a false, in quel caso significa che devo fare show
                if (!formDebugSettings.isShown)
                {
                    formDebugSettings.Location = new Point(565, 130);
                    formDebugSettings.Show();
                    formDebugSettings.isShown = true;
                }

                formDebugSettings.Visible = true;
                
                DecolorMultipleSelectedRows();

                // Rendo invisibile l'altra form
                if (formDebugTools.Visible)
                {
                    formDebugTools.Visible = false;
                }
                // Rendo invisibile l'altra form
                if (formGunSettings.Visible)
                {
                    formGunSettings.Visible = false;
                }

                // Modifico i bordi dei pulsanti
              //  btn_debugTools.BorderSize = 2;
              //  btn_debugSettings.BorderSize = 4;
               // btn_gunSettings.BorderSize = 2;
            }
        }

        /// <summary>
        /// Rende visibili i controlli per la gestione delle proprietà della pistola durante
        /// </summary>
        private void OpenGunSettings()
        {
            // Controllo che non ci siano punti non ancora salvati
            if (RobotManager.positionsToSave.Count > 0)
            {
                log.Warn("Tentativo di apertura pannello gun settings con punti ancora da salvare");
                //btn_gunSettings.BorderSize = 2;
                //formGunSettings.Visible = false;
                DecolorMultipleSelectedRows();
            }

            // Se la form è visibile la rendo invisibile
            if (formGunSettings.Visible)
            {
               // btn_gunSettings.BorderSize = 2;
                formGunSettings.Visible = false;
                DecolorMultipleSelectedRows();
            }
            else // Altrimenti la rendo visibile
            {
                // Controllo che la variabile interna sia a false, in quel caso significa che devo fare show
                if (!formGunSettings.isShown)
                {
                    formGunSettings.Location = new Point(565, 130);
                    formGunSettings.Show();
                    formGunSettings.isShown = true;
                }

                // Rendo visibile la form 
                formGunSettings.Visible = true;
                // Coloro le righe selezionate dalla form
                ColorMultipleSelectedRows();

                // Rendo invisibile l'altra form
                if (formDebugTools.Visible)
                {
                    formDebugTools.Visible = false;
                }

                // Rendo invisibile l'altra form
                if (formDebugSettings.Visible)
                {
                    formDebugSettings.Visible = false;
                }

                // Modifico i bordi dei pulsanti
              //  btn_debugTools.BorderSize = 2;
              //  btn_debugSettings.BorderSize = 2;
               // btn_gunSettings.BorderSize = 4;  
            }
        }

        /// <summary>
        /// Dovrebbe controllare i limiti degli indici in modo che l'integrità venga preservata.
        /// in particolare,  se il target è prima di entrambi, enbtrambi venegono scalati
        /// se il target è dopo entrambi nulla,
        /// se è in mezzo allora riduco solo quello finale
        /// </summary>
        /// <param name="targetIndex"></param>
        private void ManageGunSettingsIndexes(int targetIndex)
        {
            // se initial è prima della riga target allora niente, se è uguale allora annullo selezione, se è dopo riduco di 1
            // se final è prima della riga target allora niente, se è uguale annullo selezione, se è dopo riduco di 1
            /*
            // Calcolo direzione evidenziatura
            bool direction = initialIndex <= finalIndex;

            if (direction ? (targetIndex > initialIndex && targetIndex < finalIndex)
                : (targetIndex > finalIndex && targetIndex < initialIndex))
            {
                // indice target nella selezione, riduco di uno solo l'indice finale

            }
            else if (direction ? (targetIndex < initialIndex) : (targetIndex < finalIndex))
            {
                // indice target prima della selezione, riduco di 1 entrambi
                initialIndex--;
                finalIndex--;
                // Scrittura al panel per modificare gli indici
            }
            else if (direction ? (targetIndex > finalIndex) : (targetIndex > initialIndex))
            {
                // indice target dopo la selezione, non faccio nulla
            }
            else if (targetIndex == initialIndex || targetIndex == finalIndex)
            {
                // uno degli indici è l'elemento target perciò annullo la selezione
                initialIndex = 0;
                finalIndex = 0;
            }*/
        }

        #endregion

        #region Metodi di drag mode

        /// <summary>
        /// Fa partire la modalità drag mode lineare o PTP in base alla modalità scelta
        /// </summary>
        private void StartDrag(object sender, EventArgs args)
        {
            Invoke((MethodInvoker)delegate
            {
                HideSettingsAndToolsPanels(true);
                EnableScreenElements();
            });
        }

        /// <summary>
        /// Fa fermare la modalità drag mode avviata in precedenza
        /// </summary>
        private void StopDrag(object sender, EventArgs args)
        {
            Invoke((MethodInvoker)delegate {
                List<PointPosition> list = new List<PointPosition>(RobotManager.positionsToSave);
                PointPosition point = RobotManager.positionToSend;

                // Se è stata richiesta la modifica di una riga, eseguo get dell'indice di quella riga
                int selectedIndex = 0;
                if (positionUpdateRequested)
                {
                    selectedIndex = updateRowIndex; // Get indice dell'elemento selezionato
                }

                if (RobotManager.dragMode == 0) // PTP
                {
                    if (!positionUpdateRequested) // Se non è stata richiesta la modifica di un punto
                    {
                        AddPositionToListView(point);
                        lw_positions.Items[lw_positions.Items.Count - 1].BackColor = NewPointColor;
                    }
                    else // Se è stata richiesta la modifica di un punto
                    {
                        UpdatePositionInListView(point, selectedIndex);
                        lw_positions.Items[updateRowIndex].BackColor = NewPointColor;
                    }
                }
                else // Linear
                {
                    //foreach (PointPosition pointL in list)
                    //{
                    //    AddPositionToListView(pointL);
                    //}
                }
                if (RobotManager.positionsToSave.Count > 0)
                {
                    EnableOperationButtons();
                }

                EnableScreenElements(); // ri-abilito i tasti
                HideSettingsAndToolsPanels(false);
            });
            
        }

        /// <summary>
        /// Cancella gli elementi della list view e rimette gli elementi già salvati, resettando la list view
        /// </summary>
        private void RestoreListView()
        {
            log.Info("Richiesta di cancellazione degli elementi non salvati dalla list view");

            lw_positions.Items.Clear();

            pointIndex = 0;

            foreach (PointPosition point in savedPositions)
            {
                AddPositionToListView(point);
            }
        }

        /// <summary>
        /// Aggiunge un punto alla list view
        /// </summary>
        /// <param name="point"></param>
        private void AddPositionToListView(PointPosition point)
        {
            // Incremento indice del punto
            pointIndex++;
            
            ListViewItem item = new ListViewItem(pointIndex.ToString());
            string formattedTime = Convert.ToDateTime(point.timeStamp).ToString("HH:mm:ss:ff");
            item.SubItems.Add(formattedTime);
            item.SubItems.Add(point.mode);
            item.SubItems.Add(point.position.tran.x.ToString());
            item.SubItems.Add(point.position.tran.y.ToString());
            item.SubItems.Add(point.position.tran.z.ToString());
            item.SubItems.Add(point.position.rpy.rx.ToString());
            item.SubItems.Add(point.position.rpy.ry.ToString());
            item.SubItems.Add(point.position.rpy.rz.ToString());

            item.SubItems.Add((float.Parse(point.gunSettings.feed_air.ToString())/100).ToString());
            item.SubItems.Add((float.Parse(point.gunSettings.dosage_air.ToString()) / 100).ToString());
            item.SubItems.Add((float.Parse(point.gunSettings.gun_air.ToString()) / 100).ToString());
            item.SubItems.Add((float.Parse(point.gunSettings.microampere.ToString()) / 100).ToString());
            item.SubItems.Add((float.Parse(point.gunSettings.kV.ToString()) / 100).ToString());
            item.SubItems.Add(point.gunSettings.status > 0 ? "ON" : "OFF");

            lw_positions.Items.Add(item);
            EnsureVisibleAndCentered(lw_positions.Items.Count - 1);
        }

        /// <summary>
        /// Aggiorna una posizione nella ListView sostituendo la riga selezionata con una nuova posizione.
        /// </summary>
        /// <param name="point">Le nuove coordinate del punto.</param>
        /// <param name="selectedIndex">L'indice della riga selezionata nella ListView da sostituire.</param>
        private void UpdatePositionInListView(PointPosition point, int selectedIndex)
        {
            log.Info($"Avvio del metodo UpdatePositionInListView. Indice da modificare: {selectedIndex}");
            //pointIndex++;
            //log.Info($"Nuovo pointIndex: {pointIndex}");

            // Modifica della posizione con i valori nuovi del punto modificato
            lw_positions.Items[selectedIndex].SubItems[0].Text = (selectedIndex + 1).ToString();
            lw_positions.Items[selectedIndex].SubItems[1].Text = Convert.ToDateTime(point.timeStamp).ToString("HH:mm:ss:ff");
            lw_positions.Items[selectedIndex].SubItems[2].Text = point.mode;
            lw_positions.Items[selectedIndex].SubItems[3].Text = point.position.tran.x.ToString();
            lw_positions.Items[selectedIndex].SubItems[4].Text = point.position.tran.y.ToString();
            lw_positions.Items[selectedIndex].SubItems[5].Text = point.position.tran.z.ToString();
            lw_positions.Items[selectedIndex].SubItems[6].Text = point.position.rpy.rx.ToString();
            lw_positions.Items[selectedIndex].SubItems[7].Text = point.position.rpy.ry.ToString();
            lw_positions.Items[selectedIndex].SubItems[8].Text = point.position.rpy.rz.ToString();

            lw_positions.Items[selectedIndex].SubItems[9].Text = (float.Parse(point.gunSettings.feed_air.ToString()) / 100).ToString();
            lw_positions.Items[selectedIndex].SubItems[10].Text = (float.Parse(point.gunSettings.dosage_air.ToString()) / 100).ToString();
            lw_positions.Items[selectedIndex].SubItems[11].Text = (float.Parse(point.gunSettings.gun_air.ToString()) / 100).ToString();
            lw_positions.Items[selectedIndex].SubItems[12].Text = (float.Parse(point.gunSettings.microampere.ToString()) / 100).ToString();
            lw_positions.Items[selectedIndex].SubItems[13].Text = (float.Parse(point.gunSettings.kV.ToString()) / 100).ToString();
            lw_positions.Items[selectedIndex].SubItems[14].Text = point.gunSettings.status > 0 ? "ON" : "OFF";

            // Colorazione della riga modificata
            if (debugCurrentIndex == selectedIndex)
                lw_positions.Items[selectedIndex].BackColor = CurrentPointColor;
            else
                lw_positions.Items[selectedIndex].BackColor = GenericPointColor;

            idPositionUpdated = selectedIndex;
            log.Info("Fine del metodo UpdatePositionInListView.");
        }

        /// <summary>
        /// Cancella i punti presi non salvati
        /// </summary>
        private void CancelOperation()
        {
            positionUpdateRequested = false;

            // Controllo che ci siano dei punti non salvati
            if (RobotManager.positionsToSave.Count > 0) 
            {
                for (int i = savedPositions.Count; i < lw_positions.Items.Count; i++)
                {
                    lw_positions.Items[i].BackColor = DeletePointColor;
                }

                if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Vuoi cancellare tutti i punti presi e non salvati?") == DialogResult.OK)
                {
                    log.Info("Richiesta cancellazionde dei punti non salvati");
                    RobotManager.positionsToSave.Clear();
                    RestoreListView();
                }
                else
                {
                    for (int i = savedPositions.Count; i < lw_positions.Items.Count; i++)
                    {
                        lw_positions.Items[i].BackColor = NewPointColor;
                    }
                }
            }
            else
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Non ci sono punti da cancellare");
            }
        }

        /// <summary>
        /// Salva i punti presi sul db
        /// </summary>
        private void SaveOperation()
        {
            // Controllo che ci siano punti da salvare
            if (RobotManager.positionsToSave.Count < 1)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Non ci sono punti da salvare");
                return;
            }

            log.Info($"Richiesto salvataggio dei {RobotManager.positionsToSave.Count} nuovi punti presi");

            int count = savedPositions.Count;
            if (RobotManager.dragMode == 0) // PTP
            {
                if (!positionUpdateRequested)
                {
                    // salvataggio dei punti nel db
                    foreach (PointPosition pos in RobotManager.positionsToSave)
                    {
                        robotDAO.SavePositionToDatabase(ConnectionString, pos.guid, pos.position, applicationName, pos.timeStamp,
                            Convert.ToInt32(lw_positions.Items[count].SubItems[0].Text), pos.mode, pos.gunSettings);
                        count++;
                    }
                }
                else
                {
                    // salvataggio dei punti nel db
                    foreach (PointPosition pos in RobotManager.positionsToSave)
                    {
                        robotDAO.SaveUpdatedPositionToDatabase(ConnectionString, pos.guid, pos.position, applicationName, pos.timeStamp, pos.mode, pos.gunSettings);
                        count++;
                    }
                    positionUpdateRequested = false;
                }
            }
            else // Linear
            {
                // salvataggio dei punti nel db
                foreach (PointPosition pos in RobotManager.positionsToSave)
                {
                    robotDAO.SavePositionToDatabase(ConnectionString, pos.guid, pos.position, applicationName, pos.timeStamp,
                        Convert.ToInt32(lw_positions.Items[count].SubItems[0].Text), pos.mode, pos.gunSettings);
                    count++;
                }
            }

            for(int i = savedPositions.Count; i < lw_positions.Items.Count; i++)
            {
                lw_positions.Items[i].BackColor = GenericPointColor;
            }

            // Aggiunta dei punti salvati alla lista
            savedPositions.AddRange(RobotManager.positionsToSave);
            // Reset della lista dei punti da salvare
            RobotManager.positionsToSave.Clear();

            PositionListUpdated?.Invoke(null, EventArgs.Empty);    

            CustomMessageBox.Show(MessageBoxTypeEnum.INFO, "Salvataggio completato");
        }

        /// <summary>
        /// Aggiunta di una nuova applicazione
        /// </summary>
        private void AddApplication()
        {
           /* // Salvataggio su database della posizione
            FormSetApplicationName formSetApplicationName = new FormSetApplicationName();

            // Se è stato cliccato 'OK'
            if (formSetApplicationName.ShowDialog() == DialogResult.OK)
            {
                // Ottieni la data e l'ora attuali
                DateTime now = DateTime.Now;

                // Calcola il timestamp Unix in millisecondi
                long unixTimestamp = ((DateTimeOffset)now).ToUnixTimeMilliseconds();

                robotDAO.AddRobotApplication(ConnectionString, formSetApplicationName.applicationName, unixTimestamp);
            }

            FillListViewApplications();*/
        }

        /// <summary>
        /// Modifica la modalità di drag mode in PTP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDragModePTP(object sender, EventArgs e)
        {
            RobotManager.dragMode = 0;
            lblDragMode.Text = "Point to point";

            FormHomePage.Instance.LabelHeader = "PTP TEACH MODE";

            btn_PLAY.Enabled = true;
        }

        /// <summary>
        /// Modifica la modalità di drag mode in Linear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDragModeLinear(object sender, EventArgs e)
        {
            RobotManager.dragMode = 1;
            lblDragMode.Text = "Linear";

            FormHomePage.Instance.LabelHeader = "LINEAR TEACH MODE";

            btn_PLAY.Enabled = true;
        }

        /// <summary>
        /// Modifica la modalità di drag mode in Tracker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeDragModeTracker(object sender, EventArgs e)
        {
            RobotManager.dragMode = 2;
            lblDragMode.Text = "Tracker";

            FormHomePage.Instance.LabelHeader = "VR TEACH MODE";

            btn_PLAY.Enabled = true;
        }

        /// <summary>
        /// Aggiunge una posizione alla list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPositionToListView(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                AddPositionToListView(RobotManager.positionToSend);

                lw_positions.Items[lw_positions.Items.Count - 1].BackColor = NewPointColor;
            });
        }

        #endregion

        #region Metodi di debug

        /// <summary>
        /// Impedisce all'utente di usare i pannelli settings, tools e gun durante la drag mode per evitare problemi di 
        /// sincronismo tra i pulsanti.
        /// </summary>
        /// <param name="isDragStarted"></param>
        private void HideSettingsAndToolsPanels(bool isDragStarted)
        {
            btn_debugSettings.Enabled = !isDragStarted;
            btn_debugSettings.BackColor = isDragStarted ? Color.DarkGray : Color.Transparent;
            btn_debugSettings.BackgroundImage = isDragStarted ? Resources.lock_32 : Resources.settings32;
            formDebugSettings.Visible = false;

            btn_debugTools.Enabled = !isDragStarted;
            btn_debugTools.BackColor = isDragStarted ? Color.DarkGray :Color.Transparent;
            btn_debugTools.BackgroundImage = isDragStarted ? Resources.lock_32 : Resources.tools32;
            formDebugTools.Visible = false;

            btn_gunSettings.Enabled = !isDragStarted;
            btn_gunSettings.BackColor = isDragStarted ? Color.DarkGray : Color.Transparent;
            btn_gunSettings.BackgroundImage = isDragStarted ? Resources.lock_32 : Resources.airbrush;
            formGunSettings.Visible = false;

            DecolorMultipleSelectedRows();
            formGunSettings.ResetIndexesLabels();
        }

        /// <summary>
        /// Impedisce l'accesso al pannello delle impostazioni durante il movimento del robot
        /// </summary>
        /// <param name="robotIsMoving"></param>
        private void HideSettingsPanel(bool robotIsMoving)
        {
            btn_debugSettings.Enabled = !robotIsMoving;
            btn_debugSettings.BackColor = robotIsMoving ? Color.DarkGray : Color.Transparent;
            btn_debugSettings.BackgroundImage = robotIsMoving ? Resources.lock_32 : Resources.settings32;
            formDebugSettings.Visible = false;

            btn_gunSettings.Enabled = !robotIsMoving;
            btn_gunSettings.BackColor = robotIsMoving ? Color.DarkGray : Color.Transparent;
            btn_gunSettings.BackgroundImage = robotIsMoving ? Resources.lock_32 : Resources.airbrush;
            formGunSettings.Visible = false;
        }

        /// <summary>
        /// Muove il robot verso l'indice selezionato sia avanti che indietro. 
        /// Colora la riga con l'indice selezionato e poi aspetta che il robot 
        /// arrivi in quella posizione prima di ricolorare la riga.
        /// </summary>
        /// <param name="newIndex">indice di arrivo del movimento</param>
        /// <returns></returns>
        private async Task RobotSingleMovement(int newIndex)
        {
            // Controllo che il thread non stia già girando
            if (!stopCheckMovement)
            {
                log.Warn("RobotSingleMovement: Movimento precedente non terminato");
                return;
            }
            // Inizializzazione delle variabili utili
            int step = 0;
            double x, y, z, rx, ry, rz;
            DescPose pose = new DescPose(0, 0, 0, 0, 0, 0);
            stopCheckMovement = false;

            // Avvio il task che gestisce il movimento a step
            await Task.Run(async () =>
            {
                log.Info($"RobotSingleMovement: avvio del task di movimento verso l'indice: {newIndex}");
                while (!stopCheckMovement)
                {
                    switch (step)
                    {
                        case 0:
                            #region Calcolo del punto di arrivo

                            // Ottieni l'elemento precedente
                            ListViewItem previousItem = lw_positions.Items[newIndex];

                            // Deseleziona tutti gli elementi
                            lw_positions.SelectedItems.Clear();

                            // Seleziona l'elemento precedente
                            previousItem.Selected = true;

                            // Ottieni le coordinate della riga selezionata
                            x = Convert.ToDouble(previousItem.SubItems[3].Text);
                            y = Convert.ToDouble(previousItem.SubItems[4].Text);
                            z = Convert.ToDouble(previousItem.SubItems[5].Text);
                            rx = Convert.ToDouble(previousItem.SubItems[6].Text);
                            ry = Convert.ToDouble(previousItem.SubItems[7].Text);
                            rz = Convert.ToDouble(previousItem.SubItems[8].Text);

                            pose = new DescPose(x, y, z, rx, ry, rz);

                            step = 10;
                            break;
                        #endregion

                        case 10:
                            #region Selezione punto di arrivo

                            lw_positions.SelectedIndices.Clear();
                            lw_positions.Items[newIndex].BackColor = MoveToPointColor;

                            step = 20;
                            break;
                        #endregion

                        case 20:
                            #region Spostamento del robot

                            RobotManager.MoveToPoint(pose);

                            RobotManager.inPosition = false;
                            RobotManager.endingPoint = pose;

                            step = 30;
                            break;
                        #endregion

                        case 30:
                            #region Attesa in position

                            if (RobotManager.inPosition)
                            {
                                step = 40;
                            }
                            break;
                        #endregion

                        case 40:
                            #region Selezione nuovo punto

                            lw_positions.SelectedIndices.Clear();
                            lw_positions.Items[newIndex].BackColor = CurrentPointColor;

                            EnsureVisibleAndCentered(newIndex);

                            if (debugCurrentIndex > -1) // cancellazione vecchia evidenziatura
                                lw_positions.Items[debugCurrentIndex].BackColor = GenericPointColor;
                            
                            if (debugCurrentIndex > newIndex) // movimento all'indietro
                                debugCurrentIndex--;
                            else                              // movimento in avanti
                                debugCurrentIndex++;

                            UpdateDebugLabels();

                            stopCheckMovement = true;
                            log.Info("RobotSingleMovement: movimento completato");
                            break;
                            #endregion
                    }

                    await Task.Delay(DelayMovement);
                }
            });
        }

        /// <summary>
        /// Muove il robot verso l'indice selezionato sia avanti che indietro.
        /// Colora la riga con l'indice selezionato poi aspetta che il robot
        /// arrivi in quella posizione prima di ricolorare la riga.
        /// E' possibile fare anche più di un movimento prima della terminazione 
        /// del metodo in modo da implementare la funzionalirà di goto.
        /// </summary>
        /// <param name="arriveIndex">indice di arrivo del movimento</param>
        /// <returns></returns>
        private async Task RobotMultipleMovement(int arriveIndex)
        {
            // Controllo che il thread non stia già girando
            if (!stopCheckMovement)
            {
                log.Warn("RobotMultipleMovement: Movimento precedente non terminato");
                return;
            }

            // Inizializzazione variabili utili
            int step = 0;
            DescPose pose = new DescPose(0, 0, 0, 0, 0, 0);
            stopCheckMovement = false;
            bool direction = debugCurrentIndex < arriveIndex; ; // true quando va avanti
            int startIndex = -1; // rappreseta il punto che si vuole raggiungere

            // Inizializzazione della lista di posizioni
            List<ApplicationPositions> positions = ApplicationConfig.applicationsManager.getDictionary()[applicationName].positions;

            // Calcolo direzione e colorazione dei punti da attraversare
            if (direction)
            {
                if (debugCurrentIndex == -1) // dobbiamo fare la prima posizione 
                    startIndex = 0;
                else
                    startIndex = debugCurrentIndex + 1;
                
                for (int i = startIndex; i <= arriveIndex; i++)
                {
                    lw_positions.Items[i].BackColor = MoveToPointColor;
                }
            }
            else
            {
                startIndex = debugCurrentIndex - 1;
                for (int i = startIndex; i >= arriveIndex; i--)
                {
                    lw_positions.Items[i].BackColor = MoveToPointColor;
                }
            }

            // Avvio il task a step che gestisce i movimenti
            await Task.Run(async () =>
            {
                log.Info($"RobotMultipleMovement: avvio del task di movimento da {debugCurrentIndex + 1} a {arriveIndex + 1}");
                while (!stopCheckMovement)
                {
                    switch (step)
                    {
                        case 0:
                            #region Calcolo del punto di arrivo
                            pose = new DescPose(
                                positions[startIndex].x,
                                positions[startIndex].y,
                                positions[startIndex].z,
                                positions[startIndex].rx,
                                positions[startIndex].ry,
                                positions[startIndex].rz
                                );

                            step = 10;
                            break;
                        #endregion

                        case 10:
                            #region Cancellazione delle selezioni

                            lw_positions.SelectedIndices.Clear();

                            step = 20;
                            break;
                        #endregion

                        case 20:
                            #region Spostamento del robot

                            RobotManager.MoveToPoint(pose);

                            RobotManager.inPosition = false;
                            RobotManager.endingPoint = pose;

                            step = 30;
                            break;
                        #endregion

                        case 30:
                            #region Attesa in position

                            if (RobotManager.inPosition)
                            {
                                step = 40;
                            }
                            break;
                        #endregion

                        case 40:
                            #region Selezione direzione

                            lw_positions.SelectedIndices.Clear();
                            lw_positions.Items[startIndex].BackColor = CurrentPointColor;
                            EnsureVisibleAndCentered(startIndex);

                            if (direction)
                            {
                                step = 50;
                            }
                            else
                            {
                                step = 60;
                            }
                           
                            break;

                        #endregion

                        case 50:
                            #region Movimento in avanti
                            if (startIndex > 0) // se non è il primo deseleziona il precedente
                            {
                                lw_positions.Items[startIndex - 1].BackColor = GenericPointColor;
                            }
                            debugCurrentIndex++;
                            
                            step = 70;
                            break;
                        #endregion

                        case 60:
                            #region Movimento all'indietro

                            if (startIndex < lw_positions.Items.Count) // se non è l'ultimo deseleziona il successivo
                            {
                                lw_positions.Items[startIndex + 1].BackColor = GenericPointColor;
                            }
                            debugCurrentIndex--;

                            step = 80;
                            break;
                        #endregion

                        case 70:
                            #region Aggiornamento interfaccia del movimento in avanti

                            UpdateDebugLabels();

                            if (startIndex == arriveIndex) // terminazione dei punti
                            {
                                stopCheckMovement = true;
                                log.Info("RobotMultipleMovement: terminazione del task, causa: arrivo a destinazione, movimento: avanti");
                            }
                            else
                            {
                                startIndex++;
                                step = 0;
                                log.Info($"RobotMultipleMovement: robot arrivato al punto {startIndex + 1}");
                            }

                            break;
                        #endregion

                        case 80:
                            #region Aggiornamento interfaccia
                            UpdateDebugLabels();
                            
                            if (startIndex == arriveIndex) // terminazione dei punti
                            {
                                stopCheckMovement = true;
                                log.Info("RobotMultipleMovement: terminazione del task, causa: arrivo a destinazione, movimento: indietro");
                            }
                            else
                            {
                                startIndex--;
                                step = 0;
                                log.Info($"RobotMultipleMovement: robot arrivato al punto {startIndex + 1}");
                            }
                            
                            break;
                        #endregion
                    }

                    await Task.Delay(DelayMovement);
                }
            });
        }

        /// <summary>
        /// Movimento verso punto precedente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PreviousPoint(object sender, EventArgs e)
        {
            if (!RobotManager.isAutomaticMode)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impostare il robot in modalità automatico");
                log.Error("Tentativo di movimento robot in modalità manuale");
                return;
            }

            if (!RobotManager.isInHomePosition && debugCurrentIndex < 0)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Spostare il Robot nella posizione di Home", Resources.safeZone_yellow32);
                log.Error("Tentativo di avvio monitoring con Robot fuori posizione di Home");
                return;
            }

            log.Info($"Richiesta di spostamento al punto precedente, indice corrente: {debugCurrentIndex}");

            // Controllo che non ci siano punti da salvare
            if (RobotManager.positionsToSave.Count > 0)
            {
                log.Warn("Tentativo di spostamento in indietro con dei punti non salvati");
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Salva tutti i punti prima di procedere con l'esecuzione");
                return;
            }
            // Controllo che non stia girando il thread del monitoring
            else if (monitoringThreadStarted)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Impossibile muoversi tra i punti durante il monitoring");
                log.Error($"Tentativo di movimento tra i punti durante il monitoring");
                return;
            }
            // Controlo che ci sia un punto precedente
            else if (debugCurrentIndex <= 0)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Non esiste un punto precedente a questo");
                log.Error($"Non esiste un punto precedente. Indice corrente: {debugCurrentIndex}, operazione effettuata: punto precedente");
                return;
            }
            // Controllo che il robot sia in un punto sicuro
           /* else if (!CheckRobotIsInSafePosition())
            {
                return;
            }*/
            // Disabilito i pulsanti dentro alla form
            formDebugTools.EnableMovementButtons(true);
            HideSettingsPanel(true);

            // Faccio partire il movimento
            await RobotSingleMovement(debugCurrentIndex - 1);

            // Riabilito i pulsanti dentro alla form
            formDebugTools.EnableMovementButtons(false);
            HideSettingsPanel(false);
        }

        /// <summary>
        /// Movimento verso punto successivo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NextPoint(object sender, EventArgs e)
        {
            if (!RobotManager.isAutomaticMode)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impostare il robot in modalità automatico");
                log.Error("Tentativo di movimento robot in modalità manuale");
                return;
            }

            if (!RobotManager.isInHomePosition && debugCurrentIndex < 0)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Spostare il Robot nella posizione di Home", Resources.safeZone_yellow32);
                log.Error("Tentativo di avvio monitoring con Robot fuori posizione di Home");
                return;
            }

            log.Info($"Richiesta di movimento ad un punto successivo, indice corrente {debugCurrentIndex}");

            // Controllo che non ci siano punti da salvare
            if (RobotManager.positionsToSave.Count > 0)
            {
                log.Warn("Tentativo di spostamento in avanti con dei punti non salvati");
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Salva tutti i punti prima di procedere con l'esecuzione");
                return;
            }
            // Controllo che non stia girando il thread del monitoring
            else if (monitoringThreadStarted)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Impossibile muoversi tra i punti durante il monitoring");
                log.Error($"Tentativo di movimento tra i punti durante il monitoring");
                return;
            }
            // Controllo che ci sia almeno un punto successivo a quello corrente
            else if (debugCurrentIndex >= lw_positions.Items.Count - 1)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Non ci sono punti successivi a questo");
                log.Error($"Non esiste un punto successivo. Indice corrente: {debugCurrentIndex}, operazione effettuata: punto successivo");
                return;
            }
            // Controllo che il robot sia in una posizione sicura
           /* else if (!CheckRobotIsInSafePosition())
            {
                return;
            }*/

           

            // Disabilito i pulsanti nella form
            formDebugTools.EnableMovementButtons(true);
            HideSettingsPanel(true);

            // Faccio partire il movimento verso il punto successivo
            await RobotSingleMovement(debugCurrentIndex + 1);

            Thread.Sleep(500);

            // Riabilito i pulsanti nella form
            formDebugTools.EnableMovementButtons(false);
            HideSettingsPanel(false);
        }

        /// <summary>
        /// Controlla che il robot si trovi in una posizione sicura prima di eseguire comandi di movimento
        /// </summary>
        /// <returns></returns>
        private bool CheckRobotIsInSafePosition()
        {
            try
            {
                bool inPosition = false;
                if (debugCurrentIndex > -1) // Se c'è almeno un punto selezionato
                {
                    RobotManager.inPosition = false; // Reset della memoria inPosition

                    ListViewItem item = lw_positions.Items[debugCurrentIndex]; // Get del punto selezionato sulla listView delle posizioni

                    // Creazione del DescPose
                    DescPose pose = new DescPose(
                        Convert.ToDouble(item.SubItems[3].Text), // x
                        Convert.ToDouble(item.SubItems[4].Text), // y
                        Convert.ToDouble(item.SubItems[5].Text), // z
                        Convert.ToDouble(item.SubItems[6].Text), // rx
                        Convert.ToDouble(item.SubItems[7].Text), // ry
                        Convert.ToDouble(item.SubItems[8].Text) // rz
                        );

                    // Assegno il punto appena creato come quello di confronto per test inPosition
                    RobotManager.endingPoint = pose;

                    Thread.Sleep(100);

                    if (RobotManager.inPosition)
                        inPosition = true;


                    if (inPosition)// && RobotManager.isInSafeZone)
                    {
                        // Se si trova nel punto selezionato posso procedere col comando di movimento
                        return true;
                    }
                    else
                    {
                        if (RobotManager.isInSafeZone)
                        {
                            if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Punto selezionato differente dall'attuale posizione del Robot, procedere comunque?")
                                == DialogResult.OK)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (!inPosition)
                        {
                            CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Spostare il Robot nel punto selezionato oppure nell'area sicura", Resources.safeZone_yellow32);
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                else // Se non ci sono punti selezionati
                {
                    if (RobotManager.isInSafeZone)
                    {
                        return true;
                    }
                    else
                    {
                        CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Spostare il Robot nell'area sicura", Resources.safeZone_yellow32);
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                log.Error("Errore durante l'esecuzione del metodo CheckRobotIsInSafePosition: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Movimento verso punto selezionato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GoToPoint(object sender, EventArgs e)
        {
            if (!RobotManager.isAutomaticMode)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impostare il robot in modalità automatico");
                log.Error("Tentativo di movimento robot in modalità manuale");
                return;
            }

            if (!RobotManager.isInHomePosition && debugCurrentIndex < 0)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Spostare il Robot nella posizione di Home", Resources.safeZone_yellow32);
                log.Error("Tentativo di avvio monitoring con Robot fuori posizione di Home");
                return;
            }

            // Controllo che non ci siano punti da salvare
            if (RobotManager.positionsToSave.Count > 0)
            {
                log.Warn("Tentativo di spostamento goto con dei punti non salvati");
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Salva tutti i punti prima di procedere con l'esecuzione");
                return;
            }
            // Controllo che il thread monitoring non stia girando
            if (!monitoringThreadStarted)
            {
                // Se c'è un elemento selezionato
                if (lw_positions.SelectedItems.Count > 0 )
                {
                    log.Info($"Richiesto movimento multiplo da {debugCurrentIndex} a {lw_positions.SelectedItems[0].Index}");

                    // Controllo che il robot sia in una posizione sicura
                  /*  if (!CheckRobotIsInSafePosition())
                    {
                        return;
                    }*/
                    // Controllo che il punto di arrivo non corrisponda a quello corrente
                    if (debugCurrentIndex != lw_positions.SelectedItems[0].Index)
                    {
                        // Disabilito i pulsanti nela form
                        formDebugTools.EnableMovementButtons(true);
                        HideSettingsPanel(true);

                        // Eseguo il task che gestisce i movimenti fino alla destinazione
                        await RobotMultipleMovement(lw_positions.SelectedItems[0].Index);

                        // Riabilito i pulsanti nella form
                        formDebugTools.EnableMovementButtons(false);
                        HideSettingsPanel(false);
                    }
                    else
                    {
                        CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Seleziona un punto diverso da quello di partenza");
                        log.Error("Tentativo di selezione di un punto uguale a quello corrente");
                        return;
                    }
                }
                else // Se non è stata selezionata nessuna riga
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Seleziona un punto a cui andare");
                    log.Error("Nessuna riga selezionata, operazione: goto point");
                    return;
                }
            }
            else
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Impossibile muoversi tra i punti durante il monitoring");
                log.Error($"Tentativo di movimento tra i punti durante il monitoring");
            }
        }

        /// <summary>
        /// Avvio thread di monitoring
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMonitoring(object sender, EventArgs e)
        {
            log.Info("Richiesta di start monitoring");

            if (!RobotManager.isAutomaticMode)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impostare il robot in modalità automatico");
                log.Error("Tentativo di movimento robot in modalità manuale");
                return;
            }

            // Controllo che non ci siano punti da salvare
            if (RobotManager.positionsToSave.Count > 0)
            {
                log.Warn("Tentativo di avviamento del monitoring con dei punti non salvati");
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Salva tutti i punti prima di procedere con l'esecuzione");
                return;
            }

            // Controllo che ci siano dei punti prima di avviare il monitoring
            if (savedPositions.Count < 1)
            {
                log.Warn("Tentativo di avviamento del monitoring senza punti nell'applicazione");
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "L'applicazione non ha punti da eseguire");
                return;
            }

            // Controllo che il thread del monitoring non stia già girando
            if (!monitoringThreadStarted)
            {
                // Controllo che il thread dei movimenti singoli non stia già girando
                if (!stopCheckMovement)
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Impossibile avviare il monitoring durante il movimento del robot");
                    log.Error($"Tentativo di avvio del monitoring durante il movimento del robot");
                    return;
                }
                // Controllo che il robot sia in una posizione sicura
                else if(!RobotManager.isInHomePosition && debugCurrentIndex < 0)
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Spostare il Robot nella posizione di Home", Resources.safeZone_yellow32);
                    log.Error("Tentativo di avvio monitoring con Robot fuori posizione di Home");
                    return;
                }

                if (!pauseMonitoringRequest)
                {
                    if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Procedere con l'avvio del monitoring?") != DialogResult.OK)
                        return;
                }
                else
                {
                    if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Riprendere il monitoring?") != DialogResult.OK)
                        return;
                }

                stopMonitoring = false;
                pauseMonitoringRequest = false;
                formDebugTools.EnableMonitoringButtons(true);

                // Nascondo i panel di debug aperti
                HideSettingsPanel(true);

                // Thread a priorità normale
                monitoringThread = new Thread(new ThreadStart(CheckMonitoring));
                monitoringThread.IsBackground = true;
                monitoringThread.Priority = ThreadPriority.Normal;
                monitoringThread.Start();

                // Faccio partire l'applicazione normalmente, a partire dall'indice corrente
                // RobotManager.StartApplication(lbl_choosenApplication.Text, debugCurrentIndex);

                monitoringThreadStarted = true;
                

                log.Info("Monitoring avviato");
            }
        }
        public static bool riprendiCiclo = false;

        /// <summary>
        /// Pausa movimento robot e thread di monitoring
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseMonitoring(object sender, EventArgs e)
        {
            log.Info($"Richiesta pausa del monitoring, indice corrente: {debugCurrentIndex}");
            pauseMonitoringRequest = true;
        }

        private void StopMonitoring(object sender, EventArgs e)
        {
            stopMonitoring = true; // Alzo richiesta per fermare thread di riproduzione monitoring
            monitoringThreadStarted = false; // Reset variabili che segnala che il thread di monitoring è avviato
            stopCheckMovement = true; // Stop dei metodi nextPoint, previousPoint e GoTo

            formDebugTools.EnableMonitoringButtons(false); // Abilitazione dei tasti ciclo
            HideSettingsPanel(false);

            stopPositionCheckerThread = true; // Richiesta di termine del metodo eseguito dal thread PositionChecker

            RobotManager.robot.PauseMotion(); // Invio comando di pausa al robot
            Thread.Sleep(200); // Leggero ritardo per stabilizzare il robot
            RobotManager.robot.StopMotion(); // Stop Robot con conseguente cancellazione di coda di punti

            pauseMonitoringRequest = false; // Reset richiesta di pausa monitoring

            debugCurrentIndex = -1;

        }

        /// <summary>
        /// Ripresa movimento robot e thread di monitoring
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResumeMonitoring(object sender, EventArgs e)
        {
            pauseEvent.Set(); // Riprendi il thread
            RobotManager.ResumeMotion();
        }

        /// <summary>
        /// Modifica del punto selezionato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyPoint(object sender, EventArgs e)
        {
            log.Info("Richiesta di modifica di un punto");

            if (RobotManager.isAutomaticMode)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impostare il robot in modalità manuale");
                log.Error("Tentativo di modifica di un punto in modalità automatic");
                return;
            }

            // Ci devono essere dei punti selezionati da modificare
            if (lw_positions.SelectedItems.Count > 0) 
            {
                // Non ci devono essere dei punti in sospeso non ancora salvati
                if (RobotManager.positionsToSave.Count > 0) 
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Salvare prima i punti presi");
                    log.Warn("Tentativo di modifica di un punto con lista dei punti da inviare non vuota");
                    return;
                }

                // Ottieni l'indice dell'elemento selezionato, l'ID del punto è indice + 1
                int selectedIndex = lw_positions.SelectedItems[0].Index;
                log.Info($"ID punto selezionato da modificare: {selectedIndex + 1}");

                // Cancellazione degli elementi selezionati per evitare di colorare in modo sbagliato la riga 
                lw_positions.SelectedItems.Clear();

                updateRowIndex = short.Parse(selectedIndex.ToString());

                // Colorazione della riga da modificare
                lw_positions.Items[selectedIndex].BackColor = ModifyPointColor;

                // Richiesta di conferma all'utente
                if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, $"Modificare il punto {selectedIndex + 1}?") == DialogResult.OK)
                {
                    RobotManager.robot.RobotEnable(1);
                    Thread.Sleep(1000);
                    positionUpdateRequested = true;
                    isDragStart = true;
                    EnableScreenElements();
                    log.Info("Avvio DragMode PTP per modifica punto");
                    RobotManager.StartTeachingPTP();
                }
                else // Modifica annullata, rimetto i colori di partenza
                {
                    if (debugCurrentIndex == selectedIndex)
                        lw_positions.Items[selectedIndex].BackColor = CurrentPointColor;
                    else
                        lw_positions.Items[selectedIndex].BackColor = GenericPointColor;
                }
            }
            else // Se non è stata selezionata nessuna riga
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un punto da modificare");
                log.Error("Nessuna riga selezionata, operazione richiesta: modify points");
            }
        }

        /// <summary>
        /// Sovrascrittura dei punti partendo da quello selezionato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OverwritePoints(object sender, EventArgs e)
        {
            if (RobotManager.isAutomaticMode)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impostare il robot in modalità manuale");
                log.Error("Tentativo di sovrascrittura punti in modalità automatic");
                return;
            }

            //Controllo che ci sia almeno una riga selezionata
            if (lw_positions.SelectedItems.Count > 0)
            {
                log.Info($"Richiesta di overWrite dei punti a partire da indice: {lw_positions.SelectedItems[0].Index}");

                // Controllo che non ci siano punti in sospeso da salvare
                if (RobotManager.positionsToSave.Count > 0)
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Salvare prima i punti appena presi");
                    return;
                }
                // Ottieni l'indice dell'elemento selezionato
                int selectedIndex = lw_positions.SelectedIndices[0];

                // Cancellazione degli elementi selezionati per evitare di colorare in modo sbagliato la riga 
                lw_positions.SelectedItems.Clear();

                updateRowIndex = short.Parse(selectedIndex.ToString());

                // Colorazione della riga da modificare
                lw_positions.Items[selectedIndex].BackColor = ModifyPointColor;

                // Colorazione delle righe da eliminare
                for(int i = selectedIndex + 1; i < lw_positions.Items.Count; i++)
                {
                    lw_positions.Items[i].BackColor = DeletePointColor;
                }

                // Richiesta di conferma operazione
                if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING,
                    $"Sovrascrivere il programma a partire dal punto {selectedIndex + 1}?") == DialogResult.OK)
                {
                    RobotManager.robot.RobotEnable(1);
                    Thread.Sleep(1000);

                    robotDAO.DeletePointsStartingFromId(ConnectionString, selectedIndex + 1, applicationName);

                    formGunSettings.ResetIndexesLabels();

                    // Viene fatta partire la drag mode
                    //StartDrag();
                }
                else // Modifica annullata, rimetto i colori di partenza
                {
                    // Ciclo for a partire da selected index fino all'ultimo, comprende sia la riga blu che le rosse
                    for (int i = selectedIndex; i < lw_positions.Items.Count; i++)
                    {
                        if (i == debugCurrentIndex)
                            lw_positions.Items[i].BackColor = CurrentPointColor;
                        else
                            lw_positions.Items[i].BackColor = GenericPointColor;
                    }
                }
            }
            else // Se non è stata selezionata nessuna riga
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un punto di partenza");
                log.Error("Nessuna riga selezionata, operazione richiesta: overwrite points");
            }
        }

        /// <summary>
        /// Cancellazione del punto selezionato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePoint(object sender, EventArgs e)
        {
            // Controllo che non ci siano punti in sospeso da salvare
            if (RobotManager.positionsToSave.Count > 0)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Salvare prima i punti appena presi");
                return;
            }

            // Controllo che ci sia almeno un elemento selezionato da cancellare
            if (lw_positions.SelectedItems.Count > 0)
            {
                // Ottenimento indice elemento da eliminare
                int selectedIndex = lw_positions.SelectedItems[0].Index;

                log.Info($"Richiesta cancellazione del punto avente indice: {selectedIndex}");
          
                // Cancellazione delle selezioni
                lw_positions.SelectedItems.Clear();

                // Colorazione della riga
                lw_positions.Items[selectedIndex].BackColor = DeletePointColor;

                // Richiesta conferma operazione
                if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Vuoi cancellare la riga selezionata?") == DialogResult.OK)
                {
                    // Rimuovo l'elemento dal DB
                    robotDAO.DeleteRobotApplicationPosition(ConnectionString,
                        applicationName, lw_positions.Items[selectedIndex].SubItems[0].Text);

                    formGunSettings.ResetIndexesLabels();
                    PositionListUpdated?.Invoke(null, EventArgs.Empty);
                }
                else // Operazione annullata, ricoloro come in precedenza
                {
                    if (debugCurrentIndex == selectedIndex)
                        lw_positions.Items[selectedIndex].BackColor = CurrentPointColor;
                    else
                        lw_positions.Items[selectedIndex].BackColor = GenericPointColor;
                }
            }
            else // Se non è stata selezionata nessuna riga
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un punto di partenza");
                log.Error("Nessuna riga selezionata, operazione richiesta: overwrite points");
            }
        }

        double CalculateAdaptiveDelay(int numMeasures, double baseTime, double velocity)
        {
            // Regola il peso per numero di misure (valore inverso rispetto al numero di misure)
            double measureFactor = 1.0 / numMeasures;

            // Influenza della velocity (percentuale inversa: più alto il valore, più basso il delay)
            double velocityFactor = 1 - (velocity / 100.0);

            // Calcolo del delay
            double delay = baseTime * measureFactor * velocityFactor;

            return delay;
        }

        #region Struttura positionCheckerThread 

        /// <summary>
        /// Thread che esegue il metodo CheckPosition
        /// </summary>
        private static Thread positionCheckerThread;

        /// <summary>
        /// Tempo di refresh all'interno del metodo CheckPosition del thread positionCheckerThread
        /// </summary>
        private static int positionCheckerThreadRefreshPeriod = 50;

        /// <summary>
        /// A true quando il thread deve essere concluso
        /// </summary>
        private static bool stopPositionCheckerThread = false;

        #endregion

        /// <summary>
        /// Checker utilizzato per la colorazione delle righe all'interna di lw_positions
        /// </summary>
        PositionChecker checker_monitoringPos = new PositionChecker(100.0);

        /// <summary>
        /// A true quando il punto attuale del robot corrisponde con la posizione interrogata della lw_positions
        /// </summary>
        private static bool inPosition = false;

        /// <summary>
        /// Lista di posizioni su cui eseguire l'inPosition per gestire colorazione di lw_positions
        /// </summary>
        private static List<KeyValuePair<int, DescPose>> positionsToCheck = new List<KeyValuePair<int, DescPose>>();

        /// <summary>
        /// Numero di posizioni su cui eseguire l'inPosition e la relativa colorazione su lw_positions
        /// </summary>
        private static int numPositionsToCheck = 5;

        /// <summary>
        /// A true quando l'aggiornamento della lista di posizioni su cui eseguire inPosition 
        /// per gestire colorazione su lw_positions viene terminato
        /// </summary>
        private static bool aggiornamentoFinito = false;

        /// <summary>
        /// Esegue inPosition su una lista di posizioni continuamente aggiornata da CheckMonitoring
        /// </summary>
        private void CheckPosition()
        {
            // Contiene indice della riga precedente a quella attuale
            int previousPoint = debugCurrentIndex;

            while (!stopPositionCheckerThread && !AlarmManager.blockingAlarm) // Fino a quando non viene richiesto il termine del thread
            {
                try
                {
                    inPosition = false; // Reset di inPosition

                    if (aggiornamentoFinito) // Se la lista ha concluso l'aggiornamento procedo
                    {
                        // Creare una copia per evitare problemi di modifica durante l'iterazione
                        var copiaListaDizionario = positionsToCheck.ToList();

                        foreach (var elemento in copiaListaDizionario)
                        {
                            // `elemento.Key` è la chiave, `elemento.Value` è il valore
                            // Per ogni riga della lista, controllo l'inPosition
                            inPosition = checker_monitoringPos.IsInPosition(elemento.Value, RobotManager.TCPCurrentPosition);

                            if (inPosition) // Se la posizione restituisce inPosition a true
                            {
                                // Controllo che la posizione che restituisce inPosition a true, sia successiva a quella precedente
                                // così da evitare che vengano selezionate posizioni precedenti all'interno del buffer della lista
                                // su cui viene eseguito il metodo che restituisce inPosition
                                if (elemento.Key > previousPoint && elemento.Key < previousPoint + 5)
                                {
                                    
                                    // Eseguo deselezione delle righe precedenti (coloro di bianco)
                                    for (int i = 1; i <= 5; i++)
                                    {
                                        if ((elemento.Key - i) >= 0)
                                        {
                                            if (lw_positions.Items[elemento.Key - i].BackColor != GenericPointColor)
                                                lw_positions.Items[elemento.Key - i].BackColor = GenericPointColor;
                                        }
                                    }

                                    // Selezione dell'elemento corrente (coloro di grigio)
                                    lw_positions.Items[elemento.Key].BackColor = CurrentPointColor;

                                    // Selezione dell'elemento successivo (coloro di arancione)
                                    if (elemento.Key < lw_positions.Items.Count - 1)
                                        lw_positions.Items[elemento.Key + 1].BackColor = MoveToPointColor;

                                    

                                    // Assicura che gli elementi selezionati siano visibili e centrati
                                    if (elemento.Key - 1 > 0)
                                        EnsureVisibleAndCentered(elemento.Key - 1);

                                    EnsureVisibleAndCentered(elemento.Key);

                                   

                                    // Aggiorna i riferimenti dei punti
                                    previousPoint = elemento.Key;
                                    debugCurrentIndex = elemento.Key;

                                    UpdateDebugLabels();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
                Thread.Sleep(positionCheckerThreadRefreshPeriod);
            }

        }

        /// <summary>
        /// Avvia la riproduzione dei punti dell'applicazione selezionata
        /// </summary>
        public async void CheckMonitoring()
        {
            // Cancellazione di tutte le selezioni da lw_positions
            foreach (ListViewItem item in lw_positions.Items)
            {
                item.BackColor = GenericPointColor;
            }

            RobotManager.stopCycleRoutine = true; // Stop del thread che riproduce il ciclo nel caso sia attivo 
            stopPositionCheckerThread = false; // Reset della condizione per terminare il thread che gestisce colorazione su lw_positions
            stopMonitoringRequest = false; // Reset della condizione per terminare il thread che gestisce il monitoring
           // positionsToCheck.Clear(); // Reset della lista che viene letta da positionCheckerThread

            // Indice della posizione
            int index = 0;

            // Inizializzazione della variabile cont monitoring in modo che parta dall'indice corrente
            if (debugCurrentIndex < 0)
                index = 0;
            else if (debugCurrentIndex >= lw_positions.Items.Count - 1)
            {
                lw_positions.Items[debugCurrentIndex].BackColor = GenericPointColor;
                debugCurrentIndex = -1;
                index = 0;
            }
            else
                index = debugCurrentIndex + 1;

            // Thread a priorità normale (position checker)
            positionCheckerThread = new Thread(new ThreadStart(CheckPosition));
            positionCheckerThread.IsBackground = true;
            positionCheckerThread.Priority = ThreadPriority.Normal;
            positionCheckerThread.Start();

            RobotManager.stopChainUpdaterThread = false; // Reset della condizione per terminare il thread che gestisce contatore di spostamento catena

            stopMonitoring = false; //// Reset della condizione per terminare il thread che gestisce monitoring

            // Spostamento catena
            RobotManager.chainPos = 0;

            // Soglia di posizioni da eseguire
            int thresholdPos = 5;

            // Indice della posizione presente nella lista delle posizione da riprodurre su cui eseguire inPosition 
            int calculateIndex = 2;

            // Step routine
            int step = 0;

            // Get della posizione di home dal dizionario delle posizioni
            var homePose = ApplicationConfig.applicationsManager.GetPosition("pHome", "RM");

            // Indice della posizione su cui eseguire inPosition
            int indexEndingPoint = 0;

            // Posizione da riprodurre
            DescPose targetPos = new DescPose(0, 0, 0, 0, 0, 0);

            // Creazione del punto di Home
            DescPose pHome = new DescPose(
                homePose.x,
                homePose.y,
                homePose.z,
                homePose.rx,
                homePose.ry,
                homePose.rz
                );

            // Get da database delle posizioni dell'applicazione selezionata
            DataTable positions = robotDAO.GetPointsPosition(ConnectionString, RobotManager.applicationName);

            // Dichiarazione lista che conterrà le posizioni dell'applicazione selezionata
            List<DescPose> pointList = new List<DescPose>();

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

            // Faccio girare processo su un thread esterno a quello principale
            await Task.Run(async () =>
            {
                // Imposto a 1 (true) Automatic_Start, così che parta anche il conteggio dello spostamento della catena
                // (Solo per avvio applicazione senza monitoring)
                // RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 1, "INT16");

                // Lista delle posizioni da riprodurre 
                List<DescPose> targetPositions = new List<DescPose>();

                for (int i = 0; i < pointList.Count; i++)
                {
                  
                    targetPos = new DescPose(
                                pointList[i].tran.x, // + RobotManager.chainPos,
                                pointList[i].tran.y,
                                pointList[i].tran.z,
                                pointList[i].rpy.rx,
                                pointList[i].rpy.ry,
                                pointList[i].rpy.rz);

                    targetPositions.Add(targetPos);
                }


                // Centro e coloro di arancione il primo elemento.
                // Ovvero l'elemento da cui parte il monitoring
                EnsureVisibleAndCentered(index);
                lw_positions.Items[index].BackColor = MoveToPointColor;

                while (!stopMonitoring && !AlarmManager.blockingAlarm) // fino a quando non viene eseguita una richiesta di stop routine e non sono presenti allarmi bloccanti
                {
                    switch (step)
                    {
                        case 0:
                            #region Controllo su richiesta pausa monitoring

                            if (stopMonitoringRequest) // Se è stata richiesta una pausa del monitoring
                            {
                                monitoringThreadStarted = false;

                                // Cancello selezione in arancione della riga successiva a quella in cui sono se metto in pausa
                               if (debugCurrentIndex < lw_positions.Items.Count - 2)
                                lw_positions.Items[debugCurrentIndex + 1].BackColor = GenericPointColor;

                                formDebugTools.EnableMonitoringButtons(false);
                                HideSettingsPanel(false);

                                stopPositionCheckerThread = true; // Richiesta di termine del metodo eseguito dal thread PositionChecker
                                positionCheckerThread.Join(); // Attesa e termine di positionCheckerThread

                                stopMonitoring = true; // Richiesta di termine del metodo eseguito dal thread di monitoring
                                monitoringThread.Join(); // Attesa e termine del thread di monitoring

                                break;
                            }

                            step = 1;

                            break;

                            #endregion

                        case 1:
                            #region Invio delle posizioni da riprodurre

                            // Se l'indice della posizione non supera la lunghezza delle lista di posizioni da riprodurre
                            if (index <= pointList.Count - 1) 
                            {
                                // Calcolo dell'indice della posizione su cui eseguire inPosition prima di inviare al robot nuove posizioni
                                indexEndingPoint = index + calculateIndex;

                                // Reset inPosition del thread ad alta priorità utilizzato in RobotManager
                                RobotManager.inPosition = false;

                                // Segnalo che la lista di posizioni su cui eseguire l'inPosition per gestire colorazione
                                // su lw_positions sta per iniziare
                                aggiornamentoFinito = false; 

                                for (int j = index; j < index + thresholdPos; j++) // Parto dall'indice attuale fino al numero di posizioni che voglio inviare al robot
                                {
                                    if (j <= pointList.Count - 1) // Se la posizione che voglio inviare al robot non supera la lunghezza delle posizioni da riprodurre
                                    {
                                        // Creazione della posizione da inviare alla lista su cui eseguire inPosition
                                        // per gestire la colorazione su lw_positions
                                        targetPos = new DescPose(
                                            targetPositions[j].tran.x,
                                            targetPositions[j].tran.y,
                                            targetPositions[j].tran.z,
                                            targetPositions[j].rpy.rx,
                                            targetPositions[j].rpy.ry,
                                            targetPositions[j].rpy.rz
                                            );

                                        // Aggiungo la posizione alla lista su cui eseguire inPosition in positionCheckerThread
                                        positionsToCheck.Add(new KeyValuePair<int, DescPose>(j, targetPos));
                                    }
                                }

                                // Comunico che l'aggiornamento della lista su cui eseguire inPosition in positionCheckerThread è terminato
                                aggiornamentoFinito = true;

                                // Riproduco le posizioni che partono dall'indice in cui si trova la routine
                                // all'indice composto dalla somma tra l'indice e la soglia che indica il numero di posizioni da riprodurre
                                for (int i = index; i < index + thresholdPos; i++)
                                {
                                    if (i <= pointList.Count - 1)
                                    { 
                                        targetPos = new DescPose(
                                            targetPositions[i].tran.x, 
                                            targetPositions[i].tran.y,
                                            targetPositions[i].tran.z,
                                            targetPositions[i].rpy.rx,
                                            targetPositions[i].rpy.ry,
                                            targetPositions[i].rpy.rz
                                            );

                                        // Se l'indice della posizione creata corrisponde all'indice
                                        // della posizione su cui eseguire l'inPosition, assegno questo punto come endingPoint
                                        if (i == indexEndingPoint)
                                            RobotManager.endingPoint = targetPos;

                                        // Invio delle posizione al robot
                                        int err = RobotManager.robot.MoveCart(targetPos, RobotManager.tool, RobotManager.user, RobotManager.vel, RobotManager.acc, RobotManager.ovl, RobotManager.blendT, RobotManager.config);
                                       
                                        if (err != 0) // Se il movimento ha generato un errore
                                            log.Error("Errore durante il movimento del robot: " + err.ToString());

                                        lw_positions.SelectedIndices.Clear(); // Deselezione gli elementi selezionati

                                    }
                                    
                                }
                                step = 10;
                            }
                            else // Se non ci sono più posizioni da riprodurre
                            {
                                if (pauseMonitoringRequest) // Controllo se tra le ultime posizioni è stata richiesta una pausa
                                {
                                    inPosition = false;
                                    if (debugCurrentIndex < targetPositions.Count - 1)
                                    {
                                        // Se la pausa viene richiesta non all'ultimo punto,
                                        // metto come endingPoint il punto successivo
                                        RobotManager.endingPoint = targetPositions[debugCurrentIndex + 1];
                                        step = 15;
                                    }
                                }
                                else
                                {
                                    // Imposto a 0 (false) Automatic_Start che resetta anche il contatore dello spostamento della catena
                                    // (Solo per avvio applicazione senza monitoring)
                                    // RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16");
                                    RobotManager.inPosition = false;
                                    RobotManager.endingPoint = targetPositions[targetPositions.Count - 1];
                                    monitoringThreadStarted = false; // Reset della variabiale che indicia avvio del thread di monitoring

                                    step = 5;
                                }
                            }

                            break;

                        #endregion

                        case 5:
                            #region Termine routine

                             // Termine ciclo
                             if (RobotManager.inPosition)
                             {
                                formDebugTools.EnableMonitoringButtons(false);
                                HideSettingsPanel(false);

                                stopPositionCheckerThread = true;
                                // Imposto a false il booleano che fa terminare il thread della routine
                                stopMonitoringRequest = true;

                                step = 0;

                             }

                            break;

                        #endregion

                        case 10:
                            #region Attesa inPosition e aggiornamento index

                            if (RobotManager.inPosition) // Se il robot è arrivato all'ending point
                            {
                                // Aggiornamento index
                                index = index + thresholdPos;

                                // Mantengo la lista usata in positionCheckerThread con un numero di posizioni parametrizzato (numPositionsToCheck)
                                if (positionsToCheck.Count >= numPositionsToCheck)
                                {
                                    positionsToCheck.RemoveRange(0,2);
                                }

                                step = 0;
                            }


                            if (pauseMonitoringRequest)
                            {
                                inPosition = false;
                                if (debugCurrentIndex < targetPositions.Count - 1)
                                {
                                    // Se la pausa viene richiesta non all'ultimo punto,
                                    // metto come endingPoint il punto successivo
                                    RobotManager.endingPoint = targetPositions[debugCurrentIndex + 1];
                                    step = 15;
                                }
                             
                               
                            }

                            break;

                        #endregion

                        case 15:
                            #region Stop movimento Robot

                            if (inPosition)
                            {
                                // Stop del Robot
                                RobotManager.robot.PauseMotion();
                                Thread.Sleep(200);
                                RobotManager.robot.StopMotion();

                                inPosition = false; // Reset inPosition

                                stopMonitoringRequest = true;
                                step = 0;
                            }
                            break;

                            #endregion
                    }

                    Thread.Sleep(10); // Delay routine
                }
            });
        }

        /// <summary>
        /// Avvia il monitoraggio della posizione del robot e aggiorna la ListView di conseguenza.
        /// </summary>
        public async void _CheckMonitoring()
        {
            // Dichiaro lista di DescPose in cui inserirò le posizioni dell'applicazione
            List<DescPose> poses = new List<DescPose>();

            // Get delle posizioni dell'applicazione scelta
            var positions = ApplicationConfig.applicationsManager.getDictionary()[lbl_choosenApplication.Text].positions;

            // Inizializzazione della variabile cont monitoring in modo che parta dall'indice corrente
            if(debugCurrentIndex < 0)
                contMonitoring = 0;
            else if(debugCurrentIndex >= lw_positions.Items.Count - 1)
            {
                lw_positions.Items[debugCurrentIndex].BackColor = GenericPointColor;
                contMonitoring = 0;
            }
            else
                contMonitoring = debugCurrentIndex + 1;

            // Riempio la lista di DescPose
            foreach (ApplicationPositions position in positions)
            {
                poses.Add(new DescPose(
                    position.x,
                    position.y,
                    position.z,
                    position.rx,
                    position.ry,
                    position.rz));
            }
            
            log.Info("Avvio del ciclo while del monitoring");

            while (!stopMonitoring)
            {
                switch (monitoringStep)
                {
                    case 0:
                        #region Controllo indice della posizione

                        // Se sono arrivato al termine della lista di posizioni, termino il thread
                        if (contMonitoring >= poses.Count)
                        {
                            contMonitoring = 0;
                            stopMonitoring = true;
                            monitoringThreadStarted = false;
                            stopMonitoringRequest = false;
                            monitoringThread.Join();

                            formDebugTools.EnableMonitoringButtons(false);
                            HideSettingsPanel(false);
                            log.Info("Monitoring terminato, causa: arrivo all'ultimo punto");
                            break;
                        }
                        log.Info($"Monitoring: robot arrivato alla posizione {debugCurrentIndex + 1}");
                        monitoringStep = 5;
                        break;
                    #endregion

                    case 5:
                        #region Controllo su richiesta pausa monitoring
                        if (stopMonitoringRequest)
                        {
                            RobotManager.PauseMotion();
                            await Task.Delay(DelayMonitoringStopPause);
                            RobotManager.robot.StopMotion();

                            contMonitoring = 0;
                            monitoringStep = 0;
                            stopMonitoring = true;
                            monitoringThreadStarted = false;
                            stopMonitoringRequest = false;
                            monitoringThread.Join();

                            formDebugTools.EnableMonitoringButtons(false);
                            HideSettingsPanel(false);
                            break;
                        }

                        monitoringStep = 10;
                        break;
                    #endregion

                    case 10:
                        #region Colorazione preview movimento

                        lw_positions.SelectedIndices.Clear();

                        if (contMonitoring > -1)
                        {
                            lw_positions.Items[contMonitoring].BackColor = MoveToPointColor;
                        }
                        //else if (contMonitoring == 0)
                        //{
                        //    lw_positions.Items[contMonitoring].BackColor = MoveToPointColor;
                        //}

                        // Assicura che gli elementi selezionati siano visibili e centrati
                        EnsureVisibleAndCentered(contMonitoring);
                        if (contMonitoring - 1 > 0)
                            EnsureVisibleAndCentered(contMonitoring - 1);

                        monitoringStep = 15;
                        break;
                    #endregion

                    case 15:
                        #region Assegnazione dell'ending point

                        RobotManager.inPosition = false;
                        RobotManager.endingPoint = poses[contMonitoring];
                        monitoringStep = 20;
                        break;
                    #endregion

                    case 20:
                        #region Attesa inPosition

                        if (RobotManager.inPosition)
                        {
                            monitoringStep = 30;
                        }

                        break;
                    #endregion

                    case 31:
                        #region Selezione su listView

                        lw_positions.Items[contMonitoring].BackColor = CurrentPointColor;

                        if (contMonitoring > 0)
                            lw_positions.Items[contMonitoring - 1].BackColor = GenericPointColor;

                        debugCurrentIndex = contMonitoring;
                        UpdateDebugLabels();

                        //currentIndex = contMonitoring;
                        lw_positions.Items[contMonitoring].Selected = true;
                        lw_positions.EnsureVisible(contMonitoring);
                        contMonitoring++;

                        lw_positions.SelectedIndices.Clear();
                        monitoringStep = 0;
                        break;
                    #endregion

                    case 30:
                        #region Selezione su listView

                        lw_positions.Items[contMonitoring].BackColor = CurrentPointColor;

                        if (contMonitoring > 0)
                            lw_positions.Items[contMonitoring - 1].BackColor = GenericPointColor;

                        debugCurrentIndex = contMonitoring;
                        UpdateDebugLabels();

                        // Seleziona l'elemento corrente e l'elemento successivo
                        lw_positions.Items[contMonitoring].Selected = true;
                        if (contMonitoring + 1 < lw_positions.Items.Count)
                            lw_positions.Items[contMonitoring + 1].Selected = true;

                        // Assicura che gli elementi selezionati siano visibili e centrati
                        //EnsureVisibleAndCentered(contMonitoring);
                        //if (contMonitoring + 1 < lw_positions.Items.Count)
                            //EnsureVisibleAndCentered(contMonitoring + 1);

                        contMonitoring++;

                        lw_positions.SelectedIndices.Clear();
                        monitoringStep = 0;
                        break;
                        #endregion
                }
                await Task.Delay(monitoringRefreshPeriod);
            }
        }

        /// <summary>
        /// Aggiorna le label di current point id e current point time prendendo i valori direttamente dalla list view
        /// </summary>
        private void UpdateDebugLabels()
        {
            if (debugCurrentIndex < 0) // nessun elemento selezionato
            {
                lbl_currentPoint.Text = "-";
                lbl_currentTime.Text = "-";
            }
            else
            {
                lbl_currentPoint.Text = lw_positions.Items[debugCurrentIndex].SubItems[0].Text;
                lbl_currentTime.Text = lw_positions.Items[debugCurrentIndex].SubItems[1].Text;
            }
        }

        /// <summary>
        /// Metodo per centrare un elemento visibile
        /// </summary>
        /// <param name="index"></param>
        private void EnsureVisibleAndCentered(int index)
        {
            if (index >= 0 && index < lw_positions.Items.Count)
            {
                lw_positions.EnsureVisible(index);
                int topIndex = index - lw_positions.ClientSize.Height / lw_positions.Items[0].Bounds.Height / 2;
                if (topIndex < 0) topIndex = 0;
                lw_positions.TopItem = lw_positions.Items[topIndex];
            }
        }

        #endregion

        #region Eventi della form

        /// <summary>
        /// Evento che apre strumenti di debug
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_debugTools(object sender, EventArgs e)
        {
            OpenDebugTools();
        }

        /// <summary>
        /// Evento che apre impostazioni di debug
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_debugSettings(object sender, EventArgs e)
        {
            OpenDebugSettings();
        }

        /// <summary>
        /// Evento che apre impostazioni della pistola 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openGunSettings(object sender, EventArgs e)
        {
            OpenGunSettings();
        }

        /// <summary>
        /// Ritorno a HomePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_HomePage(object sender, EventArgs e)
        {
            CloseForm();
        }

        /// <summary>
        /// Stop dragMode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_StopDrag(object sender, EventArgs e)
        {
            //StopDrag();
            RefresherTask.AddUpdate(PLCTagName.Cmd_dragMode_ON, 0, "INT16");
        }

        /// <summary>
        /// Avvio dragMode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_StartDrag(object sender, EventArgs e)
        {
            /*
            if (RobotManager.isAutomaticMode)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Impostare il robot in modalità manuale");
                log.Error("Tentativo di accesso a Drag Mode con robot in modalità automatic");
                return;
            }
            if (dragMode == 2) // Se modalità VR
            {
                DescPose trackerPose = RobotManager.formTrackerCalibration.trackerTransformedCurrentPose;
                DescPose robotPose = RobotManager.TCPCurrentPosition;
                int deltaTraslation = 20; //2cm
                int deltaRotation = 3;

                bool match = true;
                if(Math.Abs(Math.Abs(robotPose.tran.x) - Math.Abs(trackerPose.tran.x)) > deltaTraslation) match = false;
                if(Math.Abs(Math.Abs(robotPose.tran.y) - Math.Abs(trackerPose.tran.y)) > deltaTraslation) match = false;
                if(Math.Abs(Math.Abs(robotPose.tran.z) - Math.Abs(trackerPose.tran.z)) > deltaTraslation) match = false;
                if(Math.Abs(Math.Abs(robotPose.rpy.rx) - Math.Abs(trackerPose.rpy.rx)) > deltaRotation) match = false;
                if(Math.Abs(Math.Abs(robotPose.rpy.ry) - Math.Abs(trackerPose.rpy.ry)) > deltaRotation) match = false;
                if(Math.Abs(Math.Abs(robotPose.rpy.rz) - Math.Abs(trackerPose.rpy.rz)) > deltaRotation) match = false;

                if (!match)
                {
                    CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "La posizione del tracker calibrato non combacia con quella del robot");
                    log.Error("La posizione del tracker calibrato non combacia con quella del robot");
                    return;
                }
            }
            StartDrag();*/
            RefresherTask.AddUpdate(PLCTagName.Cmd_dragMode_ON, 1, "INT16");
        }

        /// <summary>
        /// Cancella punti non salvati
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_cancelOperation(object sender, EventArgs e)
        {
            CancelOperation();
            DisableOperationButtons();
        }

        /// <summary>
        /// Salva punti acquisiti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_saveOperation(object sender, EventArgs e)
        {
            SaveOperation();
            DisableOperationButtons();
        }

        /// <summary>
        /// Aggiunta applicazione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_addApplication(object sender, EventArgs e)
        {
            AddApplication();
        }

        /// <summary>
        /// Permette di visualizzare le colonne relative alle posizioni o le colonne relative alle proprietà della pistola
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_showHiddenColumns(object sender, EventArgs e)
        {
            gunSettingsColumnsShowed = !gunSettingsColumnsShowed;
            ShowHiddenColumns(gunSettingsColumnsShowed);
        } 

        private void Fill_lw_positions()
        {
            applicationName = RobotManager.applicationName;
            lw_positions.Items.Clear();
            pointIndex = 0;
            savedPositions.Clear();

            // posizioni precedenti (se ci sono)
            DataTable previousPoints = robotDAO.GetPointsPosition(ConnectionString, applicationName);
            foreach (DataRow row in previousPoints.Rows)
            {
                string formattedDateTime = Convert.ToDateTime(row["SampleTime"]).ToString("yyyy-MM-dd HH:mm:ss.fffffff");

                // Variabili necessarie alla creazione dell'oggetto GunSettings
                string guid_gun_settings;
                string guid_pos;
                int? id_gun_settings;
                int? feed_air;
                int? dosage_air;
                int? gun_air;
                int? kV;
                int? microampere;
                int? status;
                string application;

                DescPose pos = new DescPose(
                    Convert.ToDouble(row["x"]),
                    Convert.ToDouble(row["y"]),
                    Convert.ToDouble(row["z"]),
                    Convert.ToDouble(row["rx"]),
                    Convert.ToDouble(row["ry"]),
                    Convert.ToDouble(row["rz"])
                    );

                guid_gun_settings = row["guid_gun_settings"].ToString();
                guid_pos = row["guid_pos"].ToString();
                id_gun_settings = row["id_gun_settings"].ToString() != "" ? Convert.ToInt32(row["id_gun_settings"]) : (int?)null;
                feed_air = row["feed_air"].ToString() != "" ? Convert.ToInt32(row["feed_air"]) : (int?)null;
                dosage_air = row["dosage_air"].ToString() != "" ? Convert.ToInt32(row["dosage_air"]) : (int?)null;
                gun_air = row["gun_air"].ToString() != "" ? Convert.ToInt32(row["gun_air"]) : (int?)null;
                kV = row["kV"].ToString() != "" ? Convert.ToInt32(row["kV"]) : (int?)null;
                microampere = row["microampere"].ToString() != "" ? Convert.ToInt32(row["microampere"]) : (int?)null;
                status = row["status"].ToString() != "" ? Convert.ToInt32(row["status"]) : (int?)null;
                application = row["application"].ToString();

                // Costruisco oggetto gunSettings da inserire nelle posizioni
                GunSettings gunSettings = new GunSettings
                    (
                        guid_gun_settings,
                        guid_pos,
                        id_gun_settings,
                        feed_air,
                        dosage_air,
                        gun_air,
                        kV,
                        microampere,
                        status,
                        application
                    );

                PointPosition point = new PointPosition(row["guid_pos"].ToString(), pos, formattedDateTime, row["Mode"].ToString(), "", gunSettings);

                savedPositions.Add(point);
                AddPositionToListView(point);
            }

            if (debugCurrentIndex > 0)
            {
                EnsureVisibleAndCentered(debugCurrentIndex);
            }
            else
            {
                EnsureVisibleAndCentered(0);
            }

            RobotManager.positionsToSend.Clear();

            InitDebug();
        }

        /// <summary>
        /// Disegno intestazioni colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lw_positions_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (Font headerFont = new Font("Microsoft Sans Serif", 12, FontStyle.Bold | FontStyle.Italic))
            {
                e.Graphics.FillRectangle(SystemBrushes.ControlDarkDark, e.Bounds);
                e.Graphics.DrawRectangle(Pens.White, e.Bounds);

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.White, e.Bounds, sf);
            }
        }

        /// <summary>
        /// Disegno elementi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lw_applications_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true; // Disegna l'elemento in modo predefinito
        }

        /// <summary>
        /// Disegno sub-elementi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lw_applications_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true; // Disegna l'elemento in modo predefinito
        }

        /// <summary>
        /// Disegno elementi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lw_positions_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true; // Disegna l'elemento in modo predefinito
        }

        /// <summary>
        /// Disegno sub-elementi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lw_positions_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true; // Disegna l'elemento in modo predefinito
        }

        #endregion

        #region Handler degli eventi esterni

        /// <summary>
        /// Metodo che intercetta evento di aggiunta applicazione e refresha la lw delle applicazioni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleDictionaryPositionDeleted(object sender, RobotDictionaryChangedEventArgs e)
        {
            // Rimuovi l'elemento selezionato
            lw_positions.Items.Remove(lw_positions.Items[e.Id]);
            savedPositions.RemoveAt(e.Id);
            pointIndex--;

            if(e.Id == debugCurrentIndex) // debug current index non può essere -1 qui
            {
                // se l'elemento cancellato è quello corrente
                debugCurrentIndex = -1;
            }
            else
            {
                // cancellazione di un elemento generico nella lista
                if (debugCurrentIndex > e.Id)
                    debugCurrentIndex--;

                for (int i = e.Id; i < lw_positions.Items.Count; i++)
                {
                    lw_positions.Items[i].SubItems[0].Text = (Convert.ToInt32(lw_positions.Items[i].SubItems[0].Text) - 1).ToString();
                }
            }

            UpdateDebugLabels();
        }

        /// <summary>
        /// Metodo che intercetta evento di aggiunta applicazione e refresha la lw delle applicazioni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleDictionaryPositionDeletedStartingFromId(object sender, RobotDictionaryChangedEventArgs e)
        {
            int startIndex = e.Id;
            // Rimuovi l'elemento selezionato
            for (int i = lw_positions.Items.Count; i >= startIndex; i--)
            {
                lw_positions.Items.RemoveAt(i - 1);
                pointIndex--;
            }

            savedPositions.RemoveRange(e.Id - 1, savedPositions.Count - (e.Id - 1));

            // controllo che debug current +1 sia maggiore di ID (debug current parte da 0)
            if (debugCurrentIndex + 1 >= e.Id) // se cancello a partire da un punto precedente a quello corrente 
            {
                // resetto l'elemento corrente
                debugCurrentIndex = -1;
            }

            UpdateDebugLabels();

            // Deseleziona tutti gli elementi
            lw_positions.SelectedItems.Clear();

            if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING ,"Iniziare la registrazione dei nuovi punti?") == DialogResult.OK)
            {
                isDragStart = true;
                EnableScreenElements();
                //FiltersManagement(true);

                if (RobotManager.dragMode == 0) // PTP
                {
                    RobotManager.StartTeachingPTP();
                }
                else // Linear
                {
                    //RobotManager.velRec = frequency;
                    RobotManager.StartTeachingLineare();
                }
            }
        }

        /// <summary>
        /// Sostituisce l'elemento selezionato
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleDictionaryPositionDeletedFromId(object sender, RobotDictionaryChangedEventArgs e)
        {
            log.Info("Rimozione elemento dalla listView delle posizioni");

            lw_positions.Items.RemoveAt(e.Id);
            pointIndex--;
            log.Info("Rimozione elemento dalla lista delle posizioni salvate");
            savedPositions.RemoveAt(e.Id);

            UpdateDebugLabels();

            // Deseleziona tutti gli elementi
            lw_positions.SelectedItems.Clear();

            if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING ,"Iniziare la registrazione del punto da modificare?") == DialogResult.OK)
            {
                log.Info("Inizio registrazione punto da modificare");
                isDragStart = true;
                EnableScreenElements();
                //FiltersManagement(true);

                if (RobotManager.dragMode == 0) // PTP
                {
                    RobotManager.StartTeachingPTP();
                }
                else // Linear
                {
                    //RobotManager.velRec = frequency;
                    RobotManager.StartTeachingLineare();
                }
            }
        }

        /// <summary>
        /// Colora la riga che sta venendo modificata dalla form gun settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialIndexGunSettingsChanged(object sender, EventArgs e)
        {
            ColorMultipleSelectedRows();
        }

        /// <summary>
        /// Colora la riga che sta venendo modificata dalla form gun settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalIndexGunSettingsChanged(object sender, EventArgs e)
        {
            ColorMultipleSelectedRows();
        }

        /// <summary>
        /// Aggiorna le righe selezionate in modo da prendere i nuovi vlaori dal dizionario delle posizioni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GunSettingsUpdated(object sender, EventArgs e)
        {
            ApplicationPositions positions;
            bool direction = initialIndex <= finalIndex;

            for (int i = direction ? initialIndex : finalIndex; // seleziono l'inizio del for
               direction ? (i <= finalIndex) : (i <= initialIndex); // seleziono la condizione per la fine del for
                i++) // incremento sempre la i perchè mi muovo sempre in avanti
            {
                positions = ApplicationConfig.applicationsManager.getDictionary()[applicationName].positions[i];

                lw_positions.Items[i].SubItems[9].Text =  (float.Parse(positions.gunSettings.feed_air.ToString()) / 100).ToString();
                lw_positions.Items[i].SubItems[10].Text = (float.Parse(positions.gunSettings.dosage_air.ToString()) / 100).ToString();
                lw_positions.Items[i].SubItems[11].Text = (float.Parse(positions.gunSettings.gun_air.ToString()) / 100).ToString();
                lw_positions.Items[i].SubItems[12].Text = (float.Parse(positions.gunSettings.microampere.ToString()) / 100).ToString();
                lw_positions.Items[i].SubItems[13].Text = (float.Parse(positions.gunSettings.kV.ToString()) / 100).ToString();
                lw_positions.Items[i].SubItems[14].Text = positions.gunSettings.status > 0 ? "ON" : "OFF";
            }

            PositionListUpdated?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Colora le righe selezionate per il cambio dei parametri della pistola
        /// </summary>
        private void ColorMultipleSelectedRows()
        {
            if (lw_positions.Items.Count < 1)
                return;

            // Tolgo i colori dagli indici vecchi
            DecolorMultipleSelectedRows();

            // Se la form gun settings non è visibile allora non devo colorare nulla
            if (!formGunSettings.Visible)
                return;

            lw_positions.SelectedItems.Clear();

            // Ottengo i valori degli indici selezionati, -1 perchè devono partire da 0
            initialIndex = formGunSettings.initialPositionIndex - 1;
            finalIndex = formGunSettings.finalPositionIndex - 1;

            // Se gli indici escono dalla lista allora non coloro nulla
            if (
                initialIndex < 0 ||
                initialIndex > lw_positions.Items.Count ||
                finalIndex < 0 ||
                finalIndex > lw_positions.Items.Count
                )
                return;

            // Colorazione delle righe in avanti
            if (initialIndex <= finalIndex)
            {
                for (int i = initialIndex; i <= finalIndex; i++)
                {
                    lw_positions.Items[i].BackColor = ModifyGunPointColor;
                }
                EnsureVisibleAndCentered(initialIndex);
            }
            else // Colorazione delle righe all'indietro
            {
                for (int i = initialIndex; i >= finalIndex; i--)
                {
                    lw_positions.Items[i].BackColor = ModifyGunPointColor;
                }
                EnsureVisibleAndCentered(finalIndex);
            }
          
        }

        /// <summary>
        /// Toglie il colore alle righe selezionate quando la form gun settings non è in vista
        /// </summary>
        private void DecolorMultipleSelectedRows()
        {
            if(RobotManager.positionsToSave.Count > 0)
                return;
            
            if (lw_positions.Items.Count < 1)
                return;

            // Se gli indici escono dalla lista allora non coloro nulla
            if (
            initialIndex < 0 ||
            initialIndex > lw_positions.Items.Count ||
            finalIndex < 0 ||
            finalIndex > lw_positions.Items.Count
            )
            return;

            // Decolorazione in avanti
            if (initialIndex <= finalIndex)
            {
                for(int i = initialIndex; i <= finalIndex; i++)
                {
                    if (i == debugCurrentIndex)
                    {
                        lw_positions.Items[i].BackColor = CurrentPointColor;
                    }
                    else
                    {
                        lw_positions.Items[i].BackColor = GenericPointColor;
                    }
                }
            }
            else // Decolorazione all'indietro
            {
                for (int i = initialIndex; i >= finalIndex; i--)
                {
                    if (i == debugCurrentIndex)
                    {
                        lw_positions.Items[i].BackColor = CurrentPointColor;
                    }
                    else
                    {
                        lw_positions.Items[i].BackColor = GenericPointColor;
                    }
                }
            }

            formGunSettings.ResetModifyLabels();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            RobotManager.formTrackerCalibration.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefresherTask.AddUpdate(PLCTagName.Cmd_teachingRec_ON, 0, "INT16");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefresherTask.AddUpdate(PLCTagName.Cmd_teachingRec_ON, 1, "INT16");
        }
    }
}
