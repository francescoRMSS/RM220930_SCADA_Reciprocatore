using System;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using RMLib.DataAccess;
using RMLib.Logger;
using RMLib.Alarms;
using RMLib.Keyboards;
using RMLib.MessageBox;
using RMLib.PLC;
using RMLib.Security;
using RM.src.RM240513.Forms.Plant;
using RM.Properties;
using System.Drawing;
using RMLib.VATView;
using RM.src.RM240513.Forms.DragMode;
using fairino;
using System.Data;
using RM.src.RM240513.Classes.PLC;
using RM.src.RM240513.Classes.FR20.Applications.Application;
using RM.src.RM240513.Forms.ScreenSaver;

namespace RM.src.RM240513
{
    /// <summary>
    /// Definisce la struttura, il comportamento e la UI della home page del software, da usare come pannello preimpostato.
    /// </summary>
    public partial class UC_HomePage : UserControl
    {
        #region Variabili per connessione database

        private static readonly RobotDAOSqlite robotDAO = new RobotDAOSqlite();
        private static readonly SqliteConnectionConfiguration DatabaseConnection = new SqliteConnectionConfiguration();
        private static readonly string ConnectionString = DatabaseConnection.GetConnectionString();

        #endregion

        #region Proprietà di UC_HomePage

        #region Colori

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

        #endregion

        /// <summary>
        /// Logger
        /// </summary>
        private static readonly log4net.ILog log = LogHelper.GetLogger();
       
        /// <summary>
        /// Specifica se il robot è in modalità automatica, altrimenti sarà in manuale
        /// </summary>
        private readonly bool isAutomaticMode = false;

        /// <summary>
        /// Indice della posizione da selezionare su lw_positions
        /// </summary>
        public static int index = 0;

        /// <summary>
        /// Indice del punto da aggiungere a lw_positions
        /// </summary>
        static int pointIndex = 0;

        #endregion

        #region Variabili d'istanza
        static UC_HomePage _obj;

        /// <summary>
        /// Definisce una variabile statica da utilizzare come istanza per accedere al contenuto di questa pagina da codice esterno
        /// </summary>
        public static UC_HomePage Instance
        {
            get 
            {
                if (_obj == null)
                {
                    _obj = new UC_HomePage();
                }
                return _obj;
            }
        }

        #endregion

        #region Metodi di UC_HomePage

        /// <summary>
        /// Inizializza lo user control della homepage
        /// </summary>
        public UC_HomePage()
        {
            InitializeComponent();
            _obj = this;

            // Imposta l'intestazione della homepage
            FormHomePage.Instance.LabelHeader = "HOME PAGE";

            // Collegamento all'evento di cambio applicazione al metodo che riempie lw_positions
            UC_ApplicationPage.selectedApplicationChanged += Fill_lw_positions;

            // Imposta la proprietà OwnerDraw su true
          //  lw_positions.OwnerDraw = true;

            // Gestisci l'evento DrawColumnHeader
          //  lw_positions.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(lw_positions_DrawColumnHeader);

            // Collegamento evento Login e Logout per mostrare/nascondere la VAT
            SecurityManager.SecMgrLoginRM += OnRMLogin;
            SecurityManager.SecMgrLogout += OnRMLogout;

            // Collegamento evento cambio velocità Robot al metodo che cambia valore della label velocità Robot
            RobotManager.RobotVelocityChanged += ChangeLblVelocity;

            // Collegamento evento cambio modalità robot al metodo che gestisce l'attivazione/disattivazione dei tasti relativi
            RobotManager.RobotModeChanged += SelectRobotModeButton;

            // Collegamento evento Robot in movimento al metodo che oscura applications e drag per evitare di accedervi
            RobotManager.RobotIsMoving += RobotIsMoving;

            UC_FullDragModePage.PositionListUpdated += PositionListUpdate;

            // Traduce e inizializza i font
            Translate();
            InitFont();

            if (AlarmManager.isPlcConnected)
            {
                InitLbl_velocity();
            }

            RobotManager.CycleRoutineStarted += RobotManager_EnableButtonCycleEvent;
            RobotManager.HomeRoutineStarted += RobotManager_EnableButtonHomeEvent;
            RobotManager.RobotPositionChanged += SelectOnLw_positions;
            RobotManager.CycleRestarted += DeselectLastRowOnLw_positions;

            InitRobotModeButtons();

            ScreenSaverManager.AutoAddClickEvents(this);
        }

        /// <summary>
        /// Deseleziona l'ultimo elemento della listView, se selezionato dalla riproduzione precedente
        /// </summary>
        public void InitSelection_lw_positions()
        {
            // Se non è già colorata di nero, la coloro
            if (lw_positions.Items[index].BackColor != GenericPointColor)
                lw_positions.Items[index].BackColor = GenericPointColor;
        }

        /// <summary>
        /// Esegue colorazione della posizione in cui si trova il robot
        /// </summary>
        /// <param name="sender">Indice della posizione da colorare</param>
        /// <param name="e"></param>
        private void SelectOnLw_positions(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate {
                index = Convert.ToInt16(sender); // Get dell'indice della posizione da colorare
                                                 // Eseguo deselezione delle righe precedenti (coloro di bianco)
                for (int i = 1; i <= 5; i++)
                {
                    if ((index - i) >= 0)
                    {
                        if (lw_positions.Items[index - i].BackColor != GenericPointColor)
                            lw_positions.Items[index - i].BackColor = GenericPointColor;
                    }
                }

                // Selezione dell'elemento corrente (coloro di grigio)
                lw_positions.Items[index].BackColor = CurrentPointColor;

                // Assicura che gli elementi selezionati siano visibili e centrati
                if (index - 1 > 0)
                    EnsureVisibleAndCentered(index - 1);

                EnsureVisibleAndCentered(index);
            });
        }

        /// <summary>
        /// Colora di bianco l'ultima riga della lw_positions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeselectLastRowOnLw_positions(object sender, EventArgs e)
        {
            // Controllo che la lista di punti non sia vuota
            Invoke((MethodInvoker)delegate {
                if (lw_positions.Items.Count > 0)
                {
                    // Get dell'ultimo elemento
                    ListViewItem lastItem = lw_positions.Items[lw_positions.Items.Count - 1];

                    // Coloro la riga di bianco
                    lastItem.BackColor = GenericPointColor;
                }
            });
        }

        /// <summary>
        /// Riempie la lw_positions ad ogni cambio di applicazione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fill_lw_positions(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                string applicationName = RobotManager.applicationName;
                lw_positions.Items.Clear();
                pointIndex = 0;
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
                    AddPositionToListView(point);
                }

                EnsureVisibleAndCentered(0);
            });
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

            item.SubItems.Add((float.Parse(point.gunSettings.feed_air.ToString()) / 100).ToString());
            item.SubItems.Add((float.Parse(point.gunSettings.dosage_air.ToString()) / 100).ToString());
            item.SubItems.Add((float.Parse(point.gunSettings.gun_air.ToString()) / 100).ToString());
            item.SubItems.Add((float.Parse(point.gunSettings.microampere.ToString()) / 100).ToString());
            item.SubItems.Add((float.Parse(point.gunSettings.kV.ToString()) / 100).ToString());
            item.SubItems.Add(point.gunSettings.status > 0 ? "ON" : "OFF");

            lw_positions.Items.Add(item);
            EnsureVisibleAndCentered(lw_positions.Items.Count - 1);
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

        /// <summary>
        /// Imposta i button relativi alla modalità del Robot quando viene avviata l'applicazione
        /// </summary>
        private void InitRobotModeButtons()
        {
            int mode = Convert.ToInt16(PLCConfig.appVariables.getValue(PLCTagName.Operating_Mode));

            switch (mode)
            {
                case 2:
                    #region Manuale

                    btn_autoMode.BackColor = SystemColors.ControlDark;
                    btn_manualMode.BackColor = Color.LimeGreen;
                    btn_homePosition.Enabled = true;
                    btn_homePosition.BackColor = SystemColors.Control;
                    btn_homePosition.BackgroundImage = Resources.home;

                    btn_startApp.Enabled = false;
                    btn_startApp.BackColor = SystemColors.ControlDark;
                    btn_startApp.BackgroundImage = null;

                    btn_stopApp.Enabled = false;
                    btn_stopApp.BackColor = SystemColors.ControlDark;
                    btn_stopApp.BackgroundImage = null;

                    btn_pauseApp.Enabled = false;
                    btn_pauseApp.BackColor = SystemColors.ControlDark;
                    btn_pauseApp.BackgroundImage = null;

                    break;

                #endregion

                case 1:
                    #region Automatico

                    btn_autoMode.BackColor = Color.DodgerBlue;
                    btn_manualMode.BackColor = SystemColors.ControlDark;
                    btn_homePosition.Enabled = false;
                    btn_homePosition.BackColor = SystemColors.ControlDark;
                    btn_homePosition.BackgroundImage = null;

                    btn_startApp.Enabled = true;
                    btn_startApp.BackColor = SystemColors.Control;
                    btn_startApp.BackgroundImage = Resources.play32;

                    btn_stopApp.Enabled = false;
                    btn_stopApp.BackColor = SystemColors.ControlDark;
                    btn_stopApp.BackgroundImage = null;

                    btn_pauseApp.Enabled = false;
                    btn_pauseApp.BackColor = SystemColors.ControlDark;
                    btn_pauseApp.BackgroundImage = null;

                    break;

                #endregion

                case 0:
                    #region Off

                    btn_autoMode.BackColor = SystemColors.ControlDark;
                    btn_manualMode.BackColor = SystemColors.ControlDark;
                    btn_homePosition.Enabled = false;
                    btn_homePosition.BackColor = SystemColors.ControlDark;
                    btn_homePosition.BackgroundImage = null;

                    btn_startApp.Enabled = false;
                    btn_startApp.BackColor = SystemColors.ControlDark;
                    btn_startApp.BackgroundImage = null;

                    btn_stopApp.Enabled = false;
                    btn_stopApp.BackColor = SystemColors.ControlDark;
                    btn_stopApp.BackgroundImage = null;

                    btn_pauseApp.Enabled = false;
                    btn_pauseApp.BackColor = SystemColors.ControlDark;
                    btn_pauseApp.BackgroundImage = null;

                    break;

                    #endregion
            }
        }

        /// <summary>
        /// Gestisce attivazione e disattivazione dei tasti start, stop e pausa quando
        /// viene cambiata la modalità di lavoro del Robot
        /// </summary>
        /// <param name="sender">1 --> Auto / 0 --> Man</param>
        /// <param name="e"></param>
        private void SelectRobotModeButton(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate {
                int mode = Convert.ToInt16(sender.ToString());

                switch (mode)
                {
                    case 0:
                        #region Manuale

                        btn_autoMode.BackColor = SystemColors.ControlDark;
                        btn_manualMode.BackColor = Color.LimeGreen;
                        btn_homePosition.Enabled = true;
                        btn_homePosition.BackColor = SystemColors.Control;
                        btn_homePosition.BackgroundImage = Resources.home;

                        btn_startApp.Enabled = false;
                        btn_startApp.BackColor = SystemColors.ControlDark;
                        btn_startApp.BackgroundImage = null;

                        btn_stopApp.Enabled = false;
                        btn_stopApp.BackColor = SystemColors.ControlDark;
                        btn_stopApp.BackgroundImage = null;

                        btn_pauseApp.Enabled = false;
                        btn_pauseApp.BackColor = SystemColors.ControlDark;
                        btn_pauseApp.BackgroundImage = null;

                        break;

                    #endregion

                    case 1:
                        #region Automatico

                        btn_autoMode.BackColor = Color.DodgerBlue;
                        btn_manualMode.BackColor = SystemColors.ControlDark;
                        btn_homePosition.Enabled = false;
                        btn_homePosition.BackColor = SystemColors.ControlDark;
                        btn_homePosition.BackgroundImage = null;

                        btn_startApp.Enabled = true;
                        btn_startApp.BackColor = SystemColors.Control;
                        btn_startApp.BackgroundImage = Resources.play32;

                        btn_stopApp.Enabled = false;
                        btn_stopApp.BackColor = SystemColors.ControlDark;
                        btn_stopApp.BackgroundImage = null;

                        btn_pauseApp.Enabled = false;
                        btn_pauseApp.BackColor = SystemColors.ControlDark;
                        btn_pauseApp.BackgroundImage = null;

                        break;

                    #endregion

                    case 3:
                        #region Off

                        btn_autoMode.BackColor = SystemColors.ControlDark;
                        btn_manualMode.BackColor = SystemColors.ControlDark;
                        btn_homePosition.Enabled = false;
                        btn_homePosition.BackColor = SystemColors.ControlDark;
                        btn_homePosition.BackgroundImage = null;

                        btn_startApp.Enabled = false;
                        btn_startApp.BackColor = SystemColors.ControlDark;
                        btn_startApp.BackgroundImage = null;

                        btn_stopApp.Enabled = false;
                        btn_stopApp.BackColor = SystemColors.ControlDark;
                        btn_stopApp.BackgroundImage = null;

                        btn_pauseApp.Enabled = false;
                        btn_pauseApp.BackColor = SystemColors.ControlDark;
                        btn_pauseApp.BackgroundImage = null;

                        break;

                        #endregion
                }
            });

        }

        /// <summary>
        /// Modifica il valore contenuto in lbl_velocity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeLblVelocity(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate { lbl_velocity.Text = sender.ToString(); });
            //lbl_velocity.Text = sender.ToString();
        }

        /// <summary>
        /// Gestisce l'attivazione e disattivazione dei tasti start, stop e pausa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RobotManager_EnableButtonCycleEvent(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate {
                if (Convert.ToInt16(sender) == 1) // Attiva start, disattiva pausa e stop
                {
                    btn_stopApp.Enabled = false;
                    btn_stopApp.BackColor = SystemColors.ControlDark;
                    btn_stopApp.BackgroundImage = null;

                    btn_pauseApp.Enabled = false;
                    btn_pauseApp.BackColor = SystemColors.ControlDark;
                    btn_pauseApp.BackgroundImage = null;

                    btn_startApp.Enabled = true;
                    btn_startApp.BackColor = SystemColors.Control;
                    btn_startApp.BackgroundImage = Resources.play32;
                }
                else if (Convert.ToInt16(sender) == 0) // Disattiva start, attiva pausa e stop
                {
                    btn_stopApp.Enabled = true;
                    btn_stopApp.BackColor = SystemColors.Control;
                    btn_stopApp.BackgroundImage = Resources.stop;

                    btn_pauseApp.Enabled = true;
                    btn_pauseApp.BackColor = SystemColors.Control;
                    btn_pauseApp.BackgroundImage = Resources.pausemonitoringRed_32;

                    btn_startApp.Enabled = false;
                    btn_startApp.BackColor = SystemColors.ControlDark;
                    btn_startApp.BackgroundImage = null;
                }
        });
            
        }

        private void RobotManager_EnableButtonHomeEvent(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                if (Convert.ToInt16(sender) == 1)
                {
                    btn_startApp.Enabled = false;
                    btn_startApp.BackColor = SystemColors.ControlDark;
                    btn_startApp.BackgroundImage = null;

                    btn_stopApp.Enabled = false;
                    btn_stopApp.BackColor = SystemColors.ControlDark;
                    btn_stopApp.BackgroundImage = null;

                    btn_homePosition.Enabled = false;
                    btn_homePosition.BackColor = SystemColors.ControlDark;
                    btn_homePosition.BackgroundImage = null;
                }
                else if (Convert.ToInt16(sender) == 0)
                {

                    btn_homePosition.Enabled = true;
                    btn_homePosition.BackColor = SystemColors.Control;
                    btn_homePosition.BackgroundImage = Resources.home;

                }
            });
        }

        /// <summary>
        /// Abilita e disabilita i tasti di start, stop e pausa
        /// </summary>
        /// <param name="status">//0: Disattiva stop, disattiva pause, attiva start - 1: Attiva stop, attiva pause, disattiva start</param>
        public void EnableCycleButton(int status)
        {
            if (status == 0) // Disattiva stop, disattiva pause, attiva start
            {
                btn_stopApp.Enabled = false;
                btn_stopApp.BackColor = SystemColors.ControlDark;
                btn_stopApp.BackgroundImage = null;

                btn_pauseApp.Enabled = false;
                btn_pauseApp.BackColor = SystemColors.ControlDark;
                btn_pauseApp.BackgroundImage = null;

                btn_startApp.Enabled = true;
                btn_startApp.BackColor = SystemColors.Control;
                btn_startApp.BackgroundImage = Resources.play32;
            }
            else if (status == 1) // Attiva stop, attiva pause, disattiva start
            {
                btn_startApp.Enabled = false;
                btn_startApp.BackColor = SystemColors.ControlDark;
                btn_startApp.BackgroundImage = null;

                btn_stopApp.Enabled = true;
                btn_stopApp.BackColor = SystemColors.Control;
                btn_stopApp.BackgroundImage = Resources.stop;

                btn_pauseApp.Enabled = true;
                btn_pauseApp.BackColor = SystemColors.Control;
                btn_pauseApp.BackgroundImage = Resources.pausemonitoringRed_32;
            }

        }

        /// <summary>
        /// TODO
        /// </summary>
        private void Translate()
        {

        }

        /// <summary>
        /// TODO
        /// </summary>
        private void InitFont()
        {

        }

        /// <summary>
        /// Inizializza label relativa alla velocità del Robot
        /// </summary>
        private void InitLbl_velocity()
        {
            /*Invoke((MethodInvoker)delegate
            {
                try
                {
                    if (RobotManager.robotProperties != null)
                        lbl_velocity.Text = RobotManager.robotProperties.Velocity.ToString();
                    else
                        lbl_velocity.Text = "0";
                }
                catch
                {
                    log.Error("Configurazione robot fallita, inserimento velocità : 0");
                    lbl_velocity.Text = "0";
                }
            });*/

            try
            {
                if (RobotManager.robotProperties != null)
                    lbl_velocity.Text = RobotManager.robotProperties.Velocity.ToString();
                else
                    lbl_velocity.Text = "0";
            }
            catch
            {
                log.Error("Configurazione robot fallita, inserimento velocità : 0");
                lbl_velocity.Text = "0";
            }
        }

        /// <summary>
        /// Oscura o rende visibili i tasti di drag e applications, scatenato quando il Robot manager si accorge che il robot si sta muovendo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RobotIsMoving(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                if (Convert.ToBoolean(sender))
                {
                    // Robot ha iniziato a muoversi
                    btn_dragMode.Enabled = false;
                    btn_dragMode.BackColor = SystemColors.ControlDark;
                    btn_dragMode.BackgroundImage = null;

                    btn_applications.Enabled = false;
                    btn_applications.BackColor = SystemColors.ControlDark;
                    btn_applications.BackgroundImage = null;
                }
                else
                {
                    // Robot ha smesso di muoversi
                    btn_dragMode.Enabled = true;
                    btn_dragMode.BackColor = SystemColors.Control;
                    btn_dragMode.BackgroundImage = Resources.drag;

                    btn_applications.Enabled = true;
                    btn_applications.BackColor = SystemColors.Control;
                    btn_applications.BackgroundImage = Resources.apps;
                }
            });
        }

        /// <summary>
        /// Evento generato quando ci si logga con un account di livello 3 o superiore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRMLogin(object sender, EventArgs e)
        {
            //pnl_buttonVAT.Visible = true;
            btn_VAT.Visible = true;
            lbl_buttonVAT.Visible = true;
        }

        /// <summary>
        /// Evento generato quando si fa il logout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnRMLogout(object sender, EventArgs e)
        {
            //pnl_buttonVAT.Visible = false;
            btn_VAT.Visible = false;
            lbl_buttonVAT.Visible = false;
            VATViewManager.CloseAll();
        }

        /// <summary>
        /// Imposta il valore della label dell'applicazione da eseguire
        /// </summary>
        /// <param name="application"></param>
        public void SetApplicationToExecute(string application)
        {
            lb_applicationToExecute.Text = application;
        }

        /// <summary>
        /// Evento generato quando la lista dei punti dell'applicazione viene aggiornata
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionListUpdate(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                Fill_lw_positions(sender, e);
            });
        }

        #endregion

        #region Eventi di UC_HomePage

        /// <summary>
        /// Caricamento dell HomePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UC_HomePage_Load(object sender, EventArgs e)
        {
            User actualUser = SecurityManager.GetActualUser();
            if (actualUser != null && actualUser.SecurityLevel >= 3)
            {
                btn_VAT.Visible = true;
                lbl_buttonVAT.Visible = true;
            }
        }

        /// <summary>
        /// Apre la form VAT view per la gestione delle variabili PLC. Accessibile solo agli utenti RM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openVAT(object sender, EventArgs e)
        {
            VATViewManager.ShowVAT();
        }

        /// <summary>
        /// Permette all'utente di uscire e chiudere l'applicativo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_exit(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("exit")) return;

            if(CustomMessageBox.ShowTranslated(MessageBoxTypeEnum.WARNING, "MSG_CLOSING_APP") == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Apre la form per la gestione della sicurezza e degli utenti.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openSecurityManager(object sender, EventArgs e)
        {
            FormSecurityView securityView = new FormSecurityView();
            securityView.ShowDialog();
        }

        /// <summary>
        /// Apre la form degli allarmi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openAlarmPage(object sender, EventArgs e)
        {
            AlarmManager.OpenAlarmFormPage(RobotManager.formAlarmPage);
        }

        /// <summary>
        /// Apre la form dei permessi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openPermissionsPage(object sender, EventArgs e)
        {
            return; 

            UC_permissions uc;
            if (!FormHomePage.Instance.PnlContainer.Controls.ContainsKey("UC_permissions"))
            {
                uc = new UC_permissions()
                {
                    Dock = DockStyle.Fill
                };
                FormHomePage.Instance.PnlContainer.Controls.Add(uc);
            }
            FormHomePage.Instance.PnlContainer.Controls["UC_permissions"].BringToFront();
        }

        /// <summary>
        /// Apre la form delle configurazioni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openConfigurationPage(object sender, EventArgs e)
        {
            return;

            UC_configuration uc;
            if (!FormHomePage.Instance.PnlContainer.Controls.ContainsKey("UC_configuration"))
            {
                uc = new UC_configuration()
                {
                    Dock = DockStyle.Fill
                };
                FormHomePage.Instance.PnlContainer.Controls.Add(uc);
            }
            FormHomePage.Instance.PnlContainer.Controls["UC_configuration"].BringToFront();
        }

        /// <summary>
        /// Evento attivato quando si vuole aggiungere speed da tasto con incremento di 1
        /// </summary>
        private void ClickEvent_addVelocity(object sender, EventArgs e)
        {
            RobotManager.CmdAddSpeed = true;
        }
   
        /// <summary>
        /// Evento attivato quando si vuole rimuovere speed da tasto con decremento di 1
        /// </summary>
        private void ClickEvent_removeVelocity(object sender, EventArgs e)
        {
            RobotManager.CmdRemoveSpeed = true;
        }

        /// <summary>
        /// Apre la pagina delle posizioni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openPositions(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("positions")) return;

            UC_positions uc;
            if (!FormHomePage.Instance.PnlContainer.Controls.ContainsKey("UC_positions"))
            {
                uc = new UC_positions()
                {
                    Dock = DockStyle.Fill
                };
                FormHomePage.Instance.PnlContainer.Controls.Add(uc);
            }
            FormHomePage.Instance.PnlContainer.Controls["UC_positions"].BringToFront();

            uc = (UC_positions)FormHomePage.Instance.PnlContainer.Controls["UC_positions"];
            uc.ShowLoadingScreen();
        }

        /// <summary>
        /// Apre pagina parametri Robot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openParameters(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("parameters")) return;

            UC_parameters uc;
            if (!FormHomePage.Instance.PnlContainer.Controls.ContainsKey("UC_parameters"))
            {
                uc = new UC_parameters()
                {
                    Dock = DockStyle.Fill
                };
                FormHomePage.Instance.PnlContainer.Controls.Add(uc);
            }
            FormHomePage.Instance.PnlContainer.Controls["UC_parameters"].BringToFront();

            uc = (UC_parameters)FormHomePage.Instance.PnlContainer.Controls["UC_parameters"];
            uc.ShowLoadingScreen();
        }

        /// <summary>
        /// Invio richiesta di modifica velocità robot al PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_setVelocity(object sender, EventArgs e)
        {
            if (!SecurityManager.ActionRequestCheck("modifyRobotSpeed")) return;

            string newVelocity = VK_Manager.OpenIntVK("0",1,100);

            if (newVelocity.Equals(VK_Manager.CANCEL_STRING)) return;
            if (Convert.ToInt16(newVelocity) < 1 || Convert.ToInt16(newVelocity) > 100)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare un valore da 1 a 100");
                return;
            }
            else
            {
                RobotManager.CmdSetSpeed = Convert.ToInt16(newVelocity);
            }
        }   

        /// <summary>
        /// Imposta la modalità manuale al robot (mode 1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_manualMode(object sender, EventArgs e)
        {
          
        }

        /// <summary>
        /// Imposta la modalità automatica al robot (mode 0)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_autoMode(object sender, EventArgs e)
        {
       
        }

        /// <summary>
        /// Apre la pagine di DragMode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openDragMode(object sender, EventArgs e)
        {
            // Se non ho l'utenza corretta non entro
            if (!SecurityManager.ActionRequestCheck("dragMode")) return;

            // Se il ciclo del Robot era stato messo precedentemente in pausa, mostro un avviso che chiede
            // di terminare il ciclo nel caso si voglia entrare in dragMode
            if (RobotManager.pauseCycleRequested)
            {
                if (CustomMessageBox.Show(MessageBoxTypeEnum.WARNING, "Il ciclo Robot è in pausa, terminarlo per accedere alla DragMode?") != DialogResult.OK)
                {
                    return; // Se viene cliccato cancel annullo l'operazione
                }
                else // Se viene cliccato Ok
                {
                    // Alzo richiesta per fermare thread di riproduzione ciclo
                    RobotManager.stopCycleRoutine = true; 

                    // Imposto a 0 (false) Automatic_Start che resetta anche il contatore dello spostamento della catena
                    RefresherTask.AddUpdate(PLCTagName.Automatic_Start, 0, "INT16");

                    // Imposto a 1 (true) Auto_Cycle_End che segnala che il ciclo automatico è terminato
                    RefresherTask.AddUpdate(PLCTagName.Auto_Cycle_End, 1, "INT16");

                    // Reset dell'indice corrente della posizione che riproduce il Robot
                    RobotManager.currentIndex = -1; 

                    // Reset della richiesta di pausa ciclo precedentemente a True
                    RobotManager.pauseCycleRequested = false;
                }
            }

            // Se è in esecuzione la home routine, aspetto di terminarla prima di accedere alla dragMode
            if (RobotManager.homeRoutineStarted)
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.WARNING_OK, "Attendi il termine della Home routine");
                log.Error("Tentativo di accesso a Drag Mode durante home routine");
                return;
            }

            // Se non ci sono applicazioni selezionate stampo messaggio di errore
            if (string.IsNullOrEmpty(RobotManager.applicationName))
            {
                CustomMessageBox.Show(MessageBoxTypeEnum.ERROR, "Selezionare prima un'applicazione da usare");
                log.Error("Tentativo di accesso a Drag Mode senza nessun'applicazione selezionata");
                return;
            }

            // Se ho l'utenza per accedere in dragMode
            // Se non ho cicli Robot in sospeso
            // Se non è in corso la homeRoutine
            // Se l'applicazione è selezionata

            UC_FullDragModePage uc; // Dichiarazione istanza UC_FullDragModePage

            // Accedo a DragMode

            if (!FormHomePage.Instance.PnlContainer.Controls.ContainsKey("UC_FullDragModePage"))
            {
                uc = new UC_FullDragModePage()
                {
                    Dock = DockStyle.Fill
                };
                FormHomePage.Instance.PnlContainer.Controls.Add(uc);
            }
            FormHomePage.Instance.PnlContainer.Controls["UC_FullDragModePage"].BringToFront();

            uc = (UC_FullDragModePage)FormHomePage.Instance.PnlContainer.Controls["UC_FullDragModePage"];
            uc.ShowLoadingScreen();
        }

        /// <summary>
        /// Apre la pagina delle applicazioni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_openApplications(object sender, EventArgs e)
        {
            UC_ApplicationPage uc;
            if (!FormHomePage.Instance.PnlContainer.Controls.ContainsKey("UC_ApplicationPage"))
            {
                uc = new UC_ApplicationPage()
                {
                    Dock = DockStyle.Fill
                };
                FormHomePage.Instance.PnlContainer.Controls.Add(uc);
            }
            FormHomePage.Instance.PnlContainer.Controls["UC_ApplicationPage"].BringToFront();

            uc = (UC_ApplicationPage)FormHomePage.Instance.PnlContainer.Controls["UC_ApplicationPage"];
            uc.ShowLoadingScreen();
        }

        /// <summary>
        /// Avvio dell'applicazione scelta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_startApp(object sender, EventArgs e)
        {
            RobotManager.CmdStartCycle = true;
        }

        /// <summary>
        /// Ritorno a casa del Robot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_GoToHomePosition(object sender, EventArgs e)
        {
            RobotManager.CmdHomeRoutine = true;
        }

        /// <summary>
        /// Stop applicazione
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_stopApp(object sender, EventArgs e)
        {
            RobotManager.CmdStopCycle = true;
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

        /// <summary>
        ///  Alza richiesta per mettere in pausa il ciclo del Robot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickEvent_pauseApp(object sender, EventArgs e)
        {
            RobotManager.CmdPauseCycle = true;  
        }

        #endregion

        #region Demo

        /// <summary>
        /// A true quando il thread demo è stato avviato
        /// </summary>
        private static bool demoThreadStarted = false;

        /// <summary>
        /// Thread che esegue metodo DemoRoutine
        /// </summary>
        private static Thread demoThread;

        /// <summary>
        /// Avvia il thread di demo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_startDemo_Click(object sender, EventArgs e)
        {
            // Controllo che il thread demo non stia già girando
            if (!demoThreadStarted)
            {
                // Thread a priorità normale
                demoThread = new Thread(new ThreadStart(DemoRoutine));
                demoThread.IsBackground = true;
                demoThread.Priority = ThreadPriority.Normal;
                demoThread.Start();

                demoThreadStarted = true;

                log.Info("Demo avviata");
            }
        }

        /// <summary>
        /// Metodo che esegue la routine di demo
        /// </summary>
        private static async void DemoRoutine()
        {
            #region Variabili necessarie alla routine

            // Parametri ciclo
            int stepDemo = 0;
            int refreshDemoDelay = 50;

            //Riferimenti movimento robot
            ExaxisPos ePos = new ExaxisPos(0,0,0,0);
            DescPose pose = new DescPose(0,0,0,0,0,0);
            DescPose offset = new DescPose(0, 0, 0, 0, 0, 0);

            // Parametri movimento robot
            int tool = 0;
            int user = 0;
            float vel = 5.0f;
            float acc = 100.0f;
            float ovl = 100.0f;
            float blendT = 500.0f;
            byte flag = 0;

            #endregion

            #region Dichiarazione punti necessari alla routine

            var pullPose = ApplicationConfig.applicationsManager.GetPosition("pPull","RM");
            var pickPose = ApplicationConfig.applicationsManager.GetPosition("pPick", "RM");
            var placePose = ApplicationConfig.applicationsManager.GetPosition("pPlace", "RM");
            var placeHome = ApplicationConfig.applicationsManager.GetPosition("pHomeDemo", "RM");

            #region Pull
            
            DescPose pPreApproachPull = new DescPose(pullPose.x - 200.0, pullPose.y, pullPose.z, pullPose.rx, pullPose.ry, pullPose.rz);
            JointPos jPreApproachPull = new JointPos(0, 0, 0, 0, 0, 0);
           
            DescPose pApproachPull = new DescPose(pullPose.x, pullPose.y, pullPose.z, pullPose.rx, pullPose.ry, pullPose.rz);
            DescPose pTakeTray = new DescPose(pApproachPull.tran.x, pApproachPull.tran.y, pApproachPull.tran.z - 30.0, pApproachPull.rpy.rx, pApproachPull.rpy.ry, pApproachPull.rpy.rz);
            DescPose pPull = new DescPose(pTakeTray.tran.x - 320.0, pTakeTray.tran.y, pTakeTray.tran.z, pTakeTray.rpy.rx, pTakeTray.rpy.ry, pTakeTray.rpy.rz);

            #endregion

            #region Pick

            DescPose pRetract1 = new DescPose(pPull.tran.x, pPull.tran.y, pPull.tran.z + 30, pPull.rpy.rx, pPull.rpy.ry, pPull.rpy.rz);
            DescPose pRetract2 = new DescPose(pRetract1.tran.x - 50, pRetract1.tran.y, pRetract1.tran.z, pRetract1.rpy.rx, pRetract1.rpy.ry, pRetract1.rpy.rz);
            DescPose pRetract3 = new DescPose(pRetract2.tran.x, pRetract2.tran.y, pRetract2.tran.z + 100, pRetract2.rpy.rx, pRetract2.rpy.ry, pRetract2.rpy.rz);
            
            DescPose pApproachPick = new DescPose(pickPose.x, pickPose.y - 50, pickPose.z + 100, pickPose.rx, pickPose.ry, pickPose.rz);
            JointPos jApproachPick = new JointPos(0, 0, 0, 0, 0, 0);

            DescPose pPrePick = new DescPose(pApproachPick.tran.x, pApproachPick.tran.y, pApproachPick.tran.z - 100, pApproachPick.rpy.rx, pApproachPick.rpy.ry, pApproachPick.rpy.rz);
            DescPose pPick = new DescPose(pickPose.x, pickPose.y, pickPose.z, pickPose.rx, pickPose.ry, pickPose.rz);
            DescPose pMovePick = new DescPose(pickPose.x - 800, pickPose.y, pickPose.z, pickPose.rx, pickPose.ry, pickPose.rz);

            #endregion

            #region Place

            DescPose pPrePlace = new DescPose(placePose.x, placePose.y, placePose.z + 50, placePose.rx, placePose.ry, placePose.rz);
            DescPose pPlace = new DescPose(placePose.x, placePose.y, placePose.z, placePose.rx, placePose.ry, placePose.rz);
            DescPose pRetractPlace = new DescPose(placePose.x, placePose.y, placePose.z + 80, placePose.rx, placePose.ry, placePose.rz);
            DescPose pPush = new DescPose(pRetractPlace.tran.x, pRetractPlace.tran.y + 100, pRetractPlace.tran.z, pRetractPlace.rpy.rx, pRetractPlace.rpy.ry, pRetractPlace.rpy.rz);
            DescPose pHome = new DescPose(placeHome.x, placeHome.y, placeHome.z, placeHome.rx, placeHome.ry, placeHome.rz);

            #endregion

            #endregion

            while (true)
            {
                switch (stepDemo)
                {
                    case 0:
                        #region Avvicinamento al punto di Pull

                        // Cambio velocità ed accelerazione
                        acc = 100;
                        vel = 90;
                        Thread.Sleep(250);

                        // Movimento in Joint
                        RobotManager.robot.GetInverseKin(0, pPreApproachPull, -1, ref jPreApproachPull);
                        int ret = RobotManager.robot.GetForwardKin(jPreApproachPull, ref pose);
                        RobotManager.robot.MoveJ(jPreApproachPull, pose, tool, user, vel, acc, ovl, ePos, blendT, flag, offset);

                        // Assegno l'ending point
                        RobotManager.inPosition = false;
                        RobotManager.endingPoint = pPreApproachPull;
                        stepDemo = 5;

                        break;

                    #endregion

                    case 1:
                        #region Attesa inPosition

                        if (RobotManager.inPosition)
                        {
                            vel = 20;

                            stepDemo = 5;
                        }
                        break;

                    #endregion

                    case 5:
                        #region Posizionamento sopra la teglia

                        vel = 20;
                        RobotManager.robot.MoveCart(pApproachPull, tool, user, vel, acc, ovl, blendT, RobotManager.config);
                        stepDemo = 10;
                        
                        break;

                    #endregion

                    case 10:
                        #region Presa della teglia

                        vel = 10;

                        RobotManager.robot.MoveCart(pTakeTray, tool, user, vel, acc, ovl, blendT, RobotManager.config);

                        // Assegnazione ending point
                        RobotManager.inPosition = false;
                        RobotManager.endingPoint = pTakeTray;
                        stepDemo = 20;

                        break;

                    #endregion

                    case 20:
                        #region Attesa inPosition

                        if (RobotManager.inPosition)
                        {
                            await Task.Delay(250);
                            vel = 70;
                            stepDemo = 30;
                        }

                        break;

                    #endregion

                    case 30:
                        #region Pull della teglia
                        
                        RobotManager.robot.MoveCart(pPull, tool, user, vel, acc, ovl, blendT, RobotManager.config);

                        // Assegnazione ending point
                        RobotManager.inPosition = false;
                        RobotManager.endingPoint = pPull;
                        
                        stepDemo = 50;

                        break;

                    #endregion

                    case 35:
                        #region Attesa inPosition

                        if (RobotManager.inPosition)
                            stepDemo = 50;

                        break;

                    #endregion

                    case 50:
                        #region Spostamento dalla teglia

                        vel = 8;

                        RobotManager.robot.MoveCart(pRetract1, tool, user, vel, acc, ovl, blendT, RobotManager.config);
                        RobotManager.robot.MoveCart(pRetract2, tool, user, vel, acc, ovl, blendT, RobotManager.config);
                        RobotManager.robot.MoveCart(pRetract3, tool, user, vel, acc, ovl, blendT, RobotManager.config);
                        stepDemo = 55;

                        break;

                    #endregion

                    case 55:
                        #region Movimento al punto di pick

                        vel = 90;

                        // Movimento in joint al punto di avvicinamento Pick
                        RobotManager.robot.GetInverseKin(0, pApproachPick, -1, ref jApproachPick);
                        ret = RobotManager.robot.GetForwardKin(jApproachPick, ref pose);
                        RobotManager.robot.MoveJ(jApproachPick, pose, tool, user, vel, acc, ovl, ePos, blendT, flag, offset);

                        vel = 70;

                        RobotManager.robot.MoveCart(pPick, tool, user, vel, acc, ovl, blendT, RobotManager.config);
                        
                        // Assegnazione ending point
                        RobotManager.inPosition = false;
                        RobotManager.endingPoint = pPick;
                        stepDemo = 120;
                        break;

                    #endregion

                    case 120:
                        #region Attesa inPosition

                        if (RobotManager.inPosition)
                        {
                            await Task.Delay(500);
                            stepDemo = 130;
                        }
                        break;

                    #endregion

                    case 130:
                        #region Estrazione teglia

                        RobotManager.robot.MoveCart(pMovePick, tool, user, vel, acc, ovl, blendT, RobotManager.config);
                        
                        // Assegnazione ending point
                        RobotManager.inPosition = false;
                        RobotManager.endingPoint = pMovePick;

                        stepDemo = 150;

                        break;

                    #endregion

                    case 140:
                        #region Attesa inPosition

                        if (RobotManager.inPosition)
                        {
                            vel = 70;
                            stepDemo = 150;
                           
                        }

                        break;

                    #endregion

                    case 150:
                        #region Movimento a punto di place

                        RobotManager.robot.MoveCart(pPrePlace, tool, user, vel, acc, ovl, blendT, RobotManager.config);

                        vel = 5;

                        RobotManager.robot.MoveCart(pPlace, tool, user, vel, acc, ovl, blendT, RobotManager.config);
                        
                        // Assegnazione ending point
                        RobotManager.inPosition = false;
                        RobotManager.endingPoint = pPlace;

                        stepDemo = 155;

                        break;

                    #endregion

                    case 155:
                        #region Attesa inPosition

                        if (RobotManager.inPosition)
                        {
                            await Task.Delay(500);
                            vel = 20;
                            stepDemo = 160;
                        }

                        break;

                    #endregion

                    case 160:
                        #region Place
                       
                        RobotManager.robot.MoveCart(pRetractPlace, tool, user, vel, acc, ovl, blendT, RobotManager.config);
                        // Scommentando questa riga di codice il robot esegue anche la spinta della teglia dopo il place
                        //RobotManager.robot.MoveCart(pPush, RobotManager.tool, RobotManager.user, vel, RobotManager.acc, RobotManager.ovl, RobotManager.blendT, RobotManager.config);

                        acc = 100;
                        vel = 100;
                       // Thread.Sleep(250);
                        RobotManager.robot.MoveCart(pHome, tool, user, vel, acc, ovl, blendT, RobotManager.config);

                        // Assegnazione ending point
                        RobotManager.inPosition = false;
                        RobotManager.endingPoint = pHome;
                        stepDemo = 200;

                        break;

                    #endregion

                    case 200:
                        #region Attesa inPosition e riavvio ciclo

                        if (RobotManager.inPosition)
                        {
                            stepDemo = 0;
                        }
                        break;

                        #endregion
                }
                await Task.Delay(refreshDemoDelay);
            }
        }

        #endregion

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!FormHomePage.Instance.PnlContainer.Controls.ContainsKey("UC_ViveTrackerPage"))
            {
                FormHomePage.Instance.PnlContainer.Controls.Add(RobotManager.viveTrackerPage);
            }
            FormHomePage.Instance.PnlContainer.Controls["UC_ViveTrackerPage"].BringToFront();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            RobotManager.taskManager.StartAllTasks();
            //RobotManager.taskManager.StartTask("CheckHighPriority");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RobotManager.taskManager.StopAllTasks();
            //RobotManager.taskManager.StopTask("CheckHighPriority");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //start
            RobotManager.taskManager.StartTaskChecker();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //stop
            RobotManager.taskManager.StopTaskChecker();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RobotManager.taskManager.AddTask(nameof(Test), Test, TaskType.LongRunning, false);
            RobotManager.taskManager.StartTask("Test");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RobotManager.taskManager.StopTask("Test");
        }

        public async Task Test(CancellationToken token)
        {
            double d = 0;
            try
            {
                log.Warn("[Task] Test avviato.");

                while (!token.IsCancellationRequested && d < 1000)
                {
                    d += 100;
                    await Task.Delay(1000, token);
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
                log.Warn("[Task] Test terimnato bene.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RobotManager.francesco = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            RobotManager.francesco = false;
        }
    }
}
