using System;
using System.Windows.Forms;
using System.Drawing;
using RMLib.Alarms;
using RM.Properties;
using System.Diagnostics;
using RMLib.Logger;
using RMLib.PLC;
using System.Threading.Tasks;
using RMLib.Versions;
using RMLib.Keyboards;
using RMLib.MessageBox;
using RMLib.Translations;
using RMLib.VATView;
using RMLib.View;
using RMLib.Security;
using System.Collections.Generic;
using RM.src.RM220930.Forms.ScreenSaver;
using RM.src.RM220930.Forms.Plant;
using RM.src.RM220930.Forms.Plant.Axis;
using RM.src.RM220930.Classes;

namespace RM.src.RM220930
{
    /// <summary>
    /// Definisce la struttura, il comportamento e la UI della form principale da cui si può arrivare a tutte le altre funzionalità. Non va mai chiusa
    /// piuttosto, per cambiare schermata bisogna cambiare il pannello (User control o UC). Per aprire altre pagine invece basta aprire una nuova
    /// form sopra di questa possibilmente come dialog.
    /// <br>Impostare _obj per usare poi la variabile di istanza così che gli UC possano accedervi come se fosse una variabile statica</br>
    /// </summary> 
    public partial class FormHomePage : Form
    {
        #region Variabili d'istanza

        static FormHomePage _obj;

        /// <summary>
        /// Definisce una istanza statica per la classe
        /// </summary>
        public static FormHomePage Instance 
        { 
            get 
            {
                if (_obj == null) _obj = new FormHomePage();
                return _obj;
            } 
        }

        /// <summary>
        /// Definisce una variabile per settare e ottenere la pagina corrente della form
        /// </summary>
        public Panel PnlContainer
        {
            get { return pnl_pageContainer; }
            set { pnl_pageContainer = value; }
        }

        /// <summary>
        /// Definisce una variabile per settare ed ottenere il nome della pagina corrente della form
        /// </summary>
        public string LabelHeader
        {
            get { return lbl_pageTitle.Text; }
            set { lbl_pageTitle.Text = value; } // Lbl_title.Font = ProjectVariables.FontHeader;
        }

        /// <summary>
        /// Definisce una variabile per settare ed ottenere l'allarme del PLC
        /// </summary>
        public Panel PlcBlinkPanel
        {
            get { return Pnl_PLC_alarm; }
            set { Pnl_PLC_alarm = value; }
        }

        #endregion

        #region Proprietà di FormHomePage

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        /// <summary>
        /// Serve per cambiare il colore di sfondo
        /// </summary>
        public Color ChangeBackColor
        {
            get { return BackColor; }
            set { BackColor = value; }
        }

        /// <summary>
        /// Istanza dell'oggetto BlinkManager
        /// </summary>
        readonly private BlinkManager blinkMgr;

        /// <summary>
        /// A true quando ausiliari connessi
        /// </summary>
        private bool emergencyOK = false;

        /// <summary>
        /// Gestisce la schermata con il video screen saver per una fiera
        /// </summary>
        private ScreenSaverManager screenSaverManager;

        /// <summary>
        /// Gestisce switch tra le varie userControl
        /// </summary>
        public static Navigator _navigator;

        #endregion

        /// <summary>
        /// Costruisce la form di homepage
        /// </summary>
        public FormHomePage()
        {
            InitializeComponent();

            //EnterFullScreenMode();
            CheckForIllegalCrossThreadCalls = false;

            // Avvio timer per la data
            timer_dateTime_clock.Tick += new EventHandler(Update_lbl_dateTime_clock);
            timer_dateTime_clock.Start();

            Translate();
            InitFont();

            NavigatorSetup();

            // 3. Crea l'istanza del BlinkManager
            blinkMgr = new BlinkManager(true, Pnl_PLC_alarm, Resources.plc_connection_ok, Resources.connection_error );

            blinkMgr.StartBlinking(); // Avvio servizio di blink

            // Iscrizione al metodo OnAllarmeGenerato quando generato evento AllarmeGenerato
            SCADAManager.AllarmeGenerato += OnAllarmeGenerato;

            // Iscrizione al metodo OnAllarmeResettato quando generato evento AllarmeResettato
            SCADAManager.AllarmeResettato += OnAllarmeResettato;

            ScreenSaverManager.AutoAddClickEvents(this); 
        }

        #region Metodi di FormHomePage

        /// <summary>
        /// Istanzia l'oggetto navigatore e registra le pagine che utililzzerà
        /// </summary>
        private void NavigatorSetup()
        {
            // Istanzio oggetto navigatore
            _navigator = new Navigator(pnl_pageContainer);

            // Registrazione della pagine dell'applicazione
            RegisterPages();
        }

        /// <summary>
        /// Entra in modalità full screen
        /// </summary>
        private void EnterFullScreenMode()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Bounds = Screen.PrimaryScreen.Bounds; // Imposta i confini della finestra sui confini dello schermo
        }

        /// <summary>
        /// Metodo che mette sfondo pannello allarme in rosso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAllarmeGenerato(object sender, EventArgs e)
        {
            // Cambia lo sfondo del pannello
            pnl_ActiveAlarms.BackgroundImage = Resources.alarm_popup_red;
        }

        /// <summary>
        /// Metodo che mette sfondo pannello allarme in grigio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAllarmeResettato(object sender, EventArgs e)
        {
            // Cambia lo sfondo del pannello
            pnl_ActiveAlarms.BackgroundImage = Resources.alarm_popup_grey;
        }

        /// <summary>
        /// (TODO) Traduzione della pagina 
        /// </summary>
        private void Translate()
        {

        }

        /// <summary>
        /// (TODO) Set del font della pagina
        /// </summary>
        private void InitFont()
        {

        }

        /// <summary>
        /// Contiene i nomi dei task gestiti di cui si osserva lo stato per le spie a schermo
        /// </summary>
        private readonly HashSet<string> allowedNames = new HashSet<string>
        {
           "CheckHighPriority",
            "CheckLowPriority",
            "AuxiliaryWorker",
            "CheckRobotConnection",
            "ApplicationTaskManager",
            "PlcComHandler",
            "SafetyTaskManager"
        };

        private void ChangeTaskStatus(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                pnl_highTask.Visible = false;
                pnl_lowTask.Visible = false;
                pnl_auxTask.Visible = false;
                pnl_comRobotTask.Visible = false;
                pnl_appTask.Visible = false;
                pnl_safetyTask.Visible = false;
                pnl_plcTaskStatus.Visible = false;
            });

            List<TaskModel> taskStructs = SCADAManager.taskManager.GetTaskList();

            foreach (TaskModel taskStruct in taskStructs)
            {
                if (!allowedNames.Contains(taskStruct.Name)) //Se il nome non è nel set di nomi osservati
                    continue;

                Color fill_color = Color.White; //0:canceled/red, -1:faulted/black, 1:running/green, 2:completed/orange
                bool taskCreated = taskStruct.Task != null;

                //Impostazione colori
                if (taskCreated)
                {
                    switch (taskStruct.Task.Status)
                    {
                        case TaskStatus.WaitingForActivation:
                            fill_color = Color.LimeGreen;
                            break;
                        case TaskStatus.Running:
                            fill_color = Color.LimeGreen;
                            break;
                        case TaskStatus.Canceled:
                            fill_color = Color.Crimson;
                            break;
                        case TaskStatus.Faulted:
                            fill_color = Color.Black;
                            break;
                        case TaskStatus.RanToCompletion:
                            fill_color = Color.DarkOrange;
                            break;
                        default:
                            fill_color = Color.White;
                            break;
                    }
                }
                Invoke((MethodInvoker)delegate
                {
                    //Impostazioni visibilità
                    switch (taskStruct.Name)
                    {
                        case "CheckHighPriority":
                            pnl_highTask.Visible = taskCreated;
                            pnl_highTaskStatus.BackColor = fill_color;
                            break;
                        case "CheckLowPriority":
                            pnl_lowTask.Visible = taskCreated;
                            pnl_lowTaskStatus.BackColor = fill_color;
                            break;
                        case "AuxiliaryWorker":
                            pnl_auxTask.Visible = taskCreated;
                            pnl_auxTaskStatus.BackColor = fill_color;
                            break;
                        case "CheckRobotConnection":
                            pnl_comRobotTask.Visible = taskCreated;
                            pnl_comRobotTaskStatus.BackColor = fill_color;
                            break;
                        case "ApplicationTaskManager":
                            pnl_appTask.Visible = taskCreated;
                            pnl_appTaskStatus.BackColor = fill_color;
                            break;
                        case "PlcComHandler":
                            pnl_plcTaskStatus.Visible = taskCreated;
                            pnl_plcTaskStatus.BackColor = fill_color;
                            break;
                        case "SafetyTaskManager":
                            pnl_safetyTask.Visible = taskCreated;
                            pnl_safetyTaskStatus.BackColor = fill_color;
                            break;
                    }
                });
            }
        }

        /// <summary>
        /// Registra pagine che utilizza il navigator
        /// </summary>
        private void RegisterPages()
        {
            // Registrazione delle pagine
            _navigator.RegisterPage("Home Page", typeof(UC_HomePage));
            _navigator.RegisterPage("Axis", typeof(UC_axis));
            _navigator.RegisterPage("Test UDT", typeof(UC_testUDT));
        }

        

        

        #endregion

        #region Eventi di FormHomePage

        /// <summary>
        /// Aggiorna e stampa orario attuale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_lbl_dateTime_clock(object sender, EventArgs e)
        {
            lbl_dateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Caricamento della home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormHomePage_Load(object sender, EventArgs e)
        {
            _navigator.Navigate("Home Page","HOME PAGE");
            SCADAManager.taskManager.OneTaskChangedStatus += ChangeTaskStatus;
        }

        /// <summary>
        /// Evento di visualizzazione della home page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormHomePage_Shown(object sender, EventArgs e)
        {
            // Notifica l'alarmManager che la form è stata caricata e quindi è possibile procedere con la gestione degli allarmi 
            AlarmManager.isFormReady = true;

            //Configurazione screen saver manager - 5m
            screenSaverManager = new ScreenSaverManager(300000, "screenSaver.mp4", false);

            ChangeTaskStatus(this, EventArgs.Empty); // Chiamo il metodo per aggiornare l'interfaccia la prima volta
        }

        /// <summary>
        /// Apertura pagina allarmi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_alarms(object sender, EventArgs e)
        {
            AlarmManager.OpenAlarmFormPage(SCADAManager.formAlarmPage);
        }

        /// <summary>
        /// Visualizzazione versione delle librerie utilizzate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoubleClickEvent_showSwVersion(object sender, EventArgs e)
        {
            Dictionary<string, string> versions = new Dictionary<string, string>
            {
                { "Project", "RM220930 - Robot verniciatura" },
                //{ "Hmi", "2024/12/05 - V1.0" },
                { "Software", "2025/01/29 - V1.2" },
                { "Alarms", AlarmManager.Version },
                { "DataAccess", RMLib.DataAccess.SqlConnectionConfiguration.DataAccessManager.Version },
                { "Environment", RMLib.Environment.Environment.Version },
                { "Keyboards", VK_Manager.Version },
                { "Logger", LogHelper.Version },
                { "MessageBox", CustomMessageBox.Version },
                { "Plc", PLCConfig.Version },
                //{ "Recipes", RecipeConfig.Version },
                { "Security", SecurityManager.Version },
                { "Translations", TranslationManager.Version },
                { "Utils", RMLib.Utils.ProjectVariables.Version },
                { "VatView", VATViewManager.Version },
                { "Versions", VersionManager.Version },
                { "View", CustomViewManager.Version }
            };

            VersionManager.ShowVersions(versions, VersionsAppType.ROBOT_FAIRINO);
        }

        /// <summary>
        /// Evento generato alla chiusura dell'app, termina tutti i thread in modo non-safe e distrugge tutti gli elementi subito.
        /// Metodo aggressivo per la chiusura che risolve il problema dei thread che rimangono in background impedendo la 
        /// riapertura del sw per via della doppia istanza.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingEvent_homePageClosing(object sender, FormClosingEventArgs e)
        {
            log.Info("Form Homepage: GUI chiusa, terminazione del programma e liberazione delle risorse");
            //Application.Exit(); // non basta
            //Environment.Exit(0); // metodo drastico, termina il processo e libera le risorse in questo momento
            if(!Global.shouldReset)
                Process.GetCurrentProcess().Kill(); //aspetta che i thread termino e libera le risorse 
        }

        /// <summary>
        /// Apertura pannello diagnostica
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pnl_diagnostics_Click(object sender, EventArgs e)
        {
            
        }

        private void pnl_showScrnSvrMgr_Paint(object sender, PaintEventArgs e)
        {
            // screenSaverManager.RestoreLocation();
        }

        /// <summary>
        /// Apre la VAT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openVAT(object sender, EventArgs e)
        {
            VATViewManager.ShowVAT();
        }

        /// <summary>
        /// Chiude l'applicazione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_exit(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("exit")) return;

            if (CustomMessageBox.ShowTranslated(MessageBoxTypeEnum.WARNING, "MSG_CLOSING_APP") == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Apre la pagina degli assi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToAxis(object sender, EventArgs e)
        {
            _navigator.Navigate("Axis", "AXIS SETUP");
        }

        /// <summary>
        /// Apre la pagina di Home
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToHomePage(object sender, EventArgs e)
        {
            _navigator.Navigate("Home Page", "HOME PAGE");
        }

        /// <summary>
        /// Apre la pagina di test UDT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_testUDT(object sender, EventArgs e)
        {
            _navigator.Navigate("Test UDT");
        }

        #endregion
    }
}
