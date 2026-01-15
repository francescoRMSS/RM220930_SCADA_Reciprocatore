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
using RM.src.RM220930.Classes.Navigator;
using System.Web.Caching;

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
        #region Graphic interface

        #region Custom button

        #region pnl_navigation

        /// <summary>
        /// Button cabin
        /// </summary>
        private CustomButton customBtn_cabin;

        /// <summary>
        /// Button catena
        /// </summary>
        private CustomButton customBtn_chain;

        /// <summary>
        /// Button ricette
        /// </summary>
        private CustomButton customBtn_recipe;

        /// <summary>
        /// Button produzione
        /// </summary>
        private CustomButton customBtn_prod;

        private CustomButton customBtn_service;

        #endregion

        #endregion

        #region Label

        #region pnl_navigation

        /// <summary>
        /// Label CABINA
        /// </summary>
        private Label lb_cabin;

        /// <summary>
        /// Label CHAIN
        /// </summary>
        private Label lb_chain;

        /// <summary>
        /// Label RICETTE
        /// </summary>
        private Label lb_recipe;

        /// <summary>
        /// Label PRODUZIONE
        /// </summary>
        private Label lb_prod;

        #endregion

        #endregion

        #endregion

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
            InitDynamicControls();

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

        private void InitDynamicControls()
        {
            CreateServiceButton();
            CreateCabinButton();
            CreateChainButton();
            CreateRecipeButton();
            CreateProdButton();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "CABINA" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToHomePage"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateCabinButton()
        {
            // Creazione del CustomButton
            customBtn_cabin = new CustomButton
            {
                Name = "customBtn_cabin",
                Size = new Size(55, 55),
                Location = new Point(25, 12), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                BackgroundImage = Resources.cabin,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 1,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_cabin.Click += ClickEvent_goToHomePage;

            // Aggiunge il bottone al pannello
            pnl_navigation.Controls.Add(customBtn_cabin);
            customBtn_cabin.BringToFront();

            // Creazione della label centrata sotto il bottone
            lb_cabin = new Label
            {
                Text = "CABINA",
                AutoSize = true, // importante per centrare correttamente
                ForeColor = Color.White,
                Font = new Font("Arial", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter // centra il testo
            };

            // Calcolo della posizione della label
            int labelX = customBtn_cabin.Left + (customBtn_cabin.Width - lb_cabin.PreferredWidth) / 2;
            int labelY = customBtn_cabin.Bottom + 5; // 5px di margine sotto il bottone
            lb_cabin.Location = new Point(labelX, labelY);

            // Aggiunge la label al pannello
            pnl_navigation.Controls.Add(lb_cabin);
            lb_cabin.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "ASSI" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToChainPage"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateChainButton()
        {
            // Creazione del CustomButton
            customBtn_chain = new CustomButton
            {
                Name = "customBtn_chain",
                Size = new Size(55, 55),
                Location = new Point(125, 12), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                BackgroundImage = Resources.chain32Black,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 1,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_chain.Click += ClickEvent_goToChainPage;

            // Aggiunge il bottone al pannello
            pnl_navigation.Controls.Add(customBtn_chain);
            customBtn_chain.BringToFront();

            // Creazione della label centrata sotto il bottone
            lb_chain = new Label
            {
                Text = "CATENA",
                AutoSize = true, // importante per centrare correttamente
                ForeColor = Color.White,
                Font = new Font("Arial", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter // centra il testo
            };

            // Calcolo della posizione della label
            int labelX = customBtn_chain.Left + (customBtn_chain.Width - lb_chain.PreferredWidth) / 2;
            int labelY = customBtn_chain.Bottom + 5; // 5px di margine sotto il bottone
            lb_chain.Location = new Point(labelX, labelY);

            // Aggiunge la label al pannello
            pnl_navigation.Controls.Add(lb_chain);
            lb_chain.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "ASSI" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToProdPage"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateRecipeButton()
        {
            // Creazione del CustomButton
            customBtn_recipe = new CustomButton
            {
                Name = "customBtn_recipe",
                Size = new Size(55, 55),
                Location = new Point(225, 12), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                BackgroundImage = Resources.recipe32Black,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 1,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_recipe.Click += ClickEvent_goToRecipePage;

            // Aggiunge il bottone al pannello
            pnl_navigation.Controls.Add(customBtn_recipe);
            customBtn_recipe.BringToFront();

            // Creazione della label centrata sotto il bottone
            lb_recipe = new Label
            {
                Text = "RICETTE",
                AutoSize = true, // importante per centrare correttamente
                ForeColor = Color.White,
                Font = new Font("Arial", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter // centra il testo
            };

            // Calcolo della posizione della label
            int labelX = customBtn_recipe.Left + (customBtn_recipe.Width - lb_recipe.PreferredWidth) / 2;
            int labelY = customBtn_recipe.Bottom + 5; // 5px di margine sotto il bottone
            lb_recipe.Location = new Point(labelX, labelY);

            // Aggiunge la label al pannello
            pnl_navigation.Controls.Add(lb_recipe);
            lb_recipe.BringToFront();
        }

        /// <summary>
        /// Crea dinamicamente il bottone "ASSI" all’interno del pannello di navigazione
        /// <see cref="pnl_navigation"/> e aggiunge un'etichetta centrata sotto di esso.
        /// </summary>
        /// <remarks>
        /// Questo metodo esegue le seguenti operazioni:
        /// <list type="number">
        ///     <item>Istanzia un <see cref="CustomButton"/> con dimensioni, posizione, colore di sfondo, immagine, bordi arrotondati e bordo colorato.</item>
        ///     <item>Iscrive l’evento <see cref="ClickEvent_goToProdPage"/> al click del bottone.</item>
        ///     <item>Aggiunge il bottone al <see cref="Panel"/> di navigazione <see cref="pnl_navigation"/> e lo porta in primo piano.</item>
        ///     <item>Istanzia una <see cref="Label"/> con il testo "CABINA", centrata orizzontalmente sotto il bottone.</item>
        ///     <item>Calcola dinamicamente la posizione della label in base alla larghezza del bottone e della label.</item>
        ///     <item>Aggiunge la label al <see cref="Panel"/> di navigazione e la porta in primo piano.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Esempio di utilizzo:
        /// <code>
        /// CreateCabinButton();
        /// </code>
        /// </example>
        private void CreateProdButton()
        {
            // Creazione del CustomButton
            customBtn_prod = new CustomButton
            {
                Name = "customBtn_prod",
                Size = new Size(55, 55),
                Location = new Point(325, 12), // relativo a panelNavigation
                BackgroundColor = SystemColors.ActiveBorder,
                BackgroundImage = Resources.prod32Black,
                BackgroundImageLayout = ImageLayout.Center,
                BorderRadius = 15,
                BorderSize = 1,
                BorderColor = Color.DimGray
            };

            // Evento click per navigare
            customBtn_prod.Click += ClickEvent_goToProdPage;

            // Aggiunge il bottone al pannello
            pnl_navigation.Controls.Add(customBtn_prod);
            customBtn_prod.BringToFront();

            // Creazione della label centrata sotto il bottone
            lb_prod = new Label
            {
                Text = "PRODUZIONE",
                AutoSize = true, // importante per centrare correttamente
                ForeColor = Color.White,
                Font = new Font("Arial", 8, FontStyle.Bold),
                TextAlign = ContentAlignment.TopCenter // centra il testo
            };

            // Calcolo della posizione della label
            int labelX = customBtn_prod.Left + (customBtn_prod.Width - lb_prod.PreferredWidth) / 2;
            int labelY = customBtn_prod.Bottom + 5; // 5px di margine sotto il bottone
            lb_prod.Location = new Point(labelX, labelY);

            // Aggiunge la label al pannello
            pnl_navigation.Controls.Add(lb_prod);
            lb_prod.BringToFront();
        }

      


        private void CreateServiceButton()
        {
            customBtn_service = new CustomButton
            {
                Name = "customBtn_service",
                Text = "Service",
                Size = new Size(55, 55),
                Location = new Point(430, 12), // relativo a panelNavigation
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                BackgroundColor = SystemColors.ActiveBorder,
                BorderRadius = 20
            };

            // customBtn_service.Click += CustomBtn_service_Click;

            pnl_navigation.Controls.Add(customBtn_service);
            customBtn_service.BringToFront();
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
            _navigator.RegisterPage("Chain", typeof(UC_chain));
            _navigator.RegisterPage("Recipe", typeof(UC_recipe));
            _navigator.RegisterPage("Prod", typeof(UC_prod));
            _navigator.RegisterPage("Axis", typeof(UC_axis));
            _navigator.RegisterPage("Test UDT", typeof(UC_testUDT));

        }

        private void ResetButtonColor()
        {
            customBtn_cabin.BackColor = SystemColors.ActiveBorder;
            customBtn_chain.BackColor = SystemColors.ActiveBorder;
            customBtn_recipe.BackColor = SystemColors.ActiveBorder;
            customBtn_prod.BackColor = SystemColors.ActiveBorder;
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
            // Preload sul thread UI
            BeginInvoke(new Action(() =>
            {
                _navigator.PreloadPages("Axis", "Test UDT");
            }));

            // Notifica l'alarmManager
            AlarmManager.isFormReady = true;

            // Configurazione screen saver manager
            screenSaverManager = new ScreenSaverManager(300000, "screenSaver.mp4", false);

            // Aggiorna interfaccia
            ChangeTaskStatus(this, EventArgs.Empty);
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
            ResetButtonColor();
            customBtn_cabin.BackgroundColor = Color.DimGray;
            _navigator.Navigate("Home Page", "HOME PAGE");
        }

        /// <summary>
        /// Apre la pagina della catena
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToChainPage(object sender, EventArgs e)
        {
            ResetButtonColor();
            customBtn_chain.BackgroundColor = Color.DimGray;
            _navigator.Navigate("Chain", "CATENA");
        }

        /// <summary>
        /// Apre la pagina delle ricette
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToRecipePage(object sender, EventArgs e)
        {
            ResetButtonColor();
            customBtn_recipe.BackgroundColor = Color.DimGray;
            _navigator.Navigate("Recipe", "RICETTE");
        }

        /// <summary>
        /// Apre la pagina produzione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_goToProdPage(object sender, EventArgs e)
        {
            ResetButtonColor();
            customBtn_prod.BackgroundColor = Color.DimGray;
            _navigator.Navigate("Prod", "RICETTE");
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
