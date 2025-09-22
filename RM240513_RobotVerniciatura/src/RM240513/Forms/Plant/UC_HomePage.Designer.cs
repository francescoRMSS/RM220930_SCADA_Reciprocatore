namespace RM.src.RM240513
{
    partial class UC_HomePage
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_buttonExit = new System.Windows.Forms.Label();
            this.lbl_buttonAlarms = new System.Windows.Forms.Label();
            this.lbl_buttonSecurity = new System.Windows.Forms.Label();
            this.btn_permissions = new RMLib.View.CustomButton();
            this.lbl_buttonVAT = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_buttonParameters = new System.Windows.Forms.Label();
            this.lbl_buttonPositions = new System.Windows.Forms.Label();
            this.lbl_buttonConfiguration = new System.Windows.Forms.Label();
            this.btn_configuration = new RMLib.View.CustomButton();
            this.lbl_buttonPermissions = new System.Windows.Forms.Label();
            this.lbl_buttonAuto = new System.Windows.Forms.Label();
            this.lbl_containerMode = new System.Windows.Forms.Label();
            this.lbl_buttonManual = new System.Windows.Forms.Label();
            this.lbl_robot = new System.Windows.Forms.Label();
            this.lbl_homePosition = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_application = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_velocity = new System.Windows.Forms.Label();
            this.pnl_addSpeed = new System.Windows.Forms.Panel();
            this.pnl_removeSpeed = new System.Windows.Forms.Panel();
            this.lbl_percent = new System.Windows.Forms.Label();
            this.lbl_robotSpeed = new System.Windows.Forms.Label();
            this.lb_applicationToExecute = new System.Windows.Forms.Label();
            this.btn_startDemo = new RMLib.View.CustomButton();
            this.lw_positions = new RMLib.View.ScrollableListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.btn_positions = new System.Windows.Forms.Button();
            this.btn_parameters = new System.Windows.Forms.Button();
            this.btn_dragMode = new System.Windows.Forms.Button();
            this.btn_applications = new System.Windows.Forms.Button();
            this.btn_security = new System.Windows.Forms.Button();
            this.btn_alarms = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_VAT = new System.Windows.Forms.Button();
            this.btn_startApp = new System.Windows.Forms.Button();
            this.btn_pauseApp = new System.Windows.Forms.Button();
            this.btn_stopApp = new System.Windows.Forms.Button();
            this.btn_manualMode = new System.Windows.Forms.Button();
            this.btn_autoMode = new System.Windows.Forms.Button();
            this.btn_homePosition = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_buttonExit
            // 
            this.lbl_buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonExit.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonExit.Location = new System.Drawing.Point(623, 636);
            this.lbl_buttonExit.Name = "lbl_buttonExit";
            this.lbl_buttonExit.Size = new System.Drawing.Size(60, 23);
            this.lbl_buttonExit.TabIndex = 1;
            this.lbl_buttonExit.Text = "Esci";
            this.lbl_buttonExit.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_buttonAlarms
            // 
            this.lbl_buttonAlarms.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonAlarms.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonAlarms.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonAlarms.Location = new System.Drawing.Point(521, 636);
            this.lbl_buttonAlarms.Name = "lbl_buttonAlarms";
            this.lbl_buttonAlarms.Size = new System.Drawing.Size(60, 22);
            this.lbl_buttonAlarms.TabIndex = 1;
            this.lbl_buttonAlarms.Text = "Allarmi";
            this.lbl_buttonAlarms.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_buttonSecurity
            // 
            this.lbl_buttonSecurity.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonSecurity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonSecurity.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonSecurity.Location = new System.Drawing.Point(414, 636);
            this.lbl_buttonSecurity.Name = "lbl_buttonSecurity";
            this.lbl_buttonSecurity.Size = new System.Drawing.Size(72, 27);
            this.lbl_buttonSecurity.TabIndex = 1;
            this.lbl_buttonSecurity.Text = "Sicurezza";
            this.lbl_buttonSecurity.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_permissions
            // 
            this.btn_permissions.BackColor = System.Drawing.SystemColors.Control;
            this.btn_permissions.BackgroundColor = System.Drawing.SystemColors.Control;
            this.btn_permissions.BackgroundImage = global::RM.Properties.Resources.check;
            this.btn_permissions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_permissions.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_permissions.BorderRadius = 15;
            this.btn_permissions.BorderSize = 0;
            this.btn_permissions.FlatAppearance.BorderSize = 0;
            this.btn_permissions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_permissions.ForeColor = System.Drawing.Color.White;
            this.btn_permissions.Location = new System.Drawing.Point(1046, 41);
            this.btn_permissions.Name = "btn_permissions";
            this.btn_permissions.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btn_permissions.Size = new System.Drawing.Size(60, 60);
            this.btn_permissions.TabIndex = 2;
            this.btn_permissions.TextColor = System.Drawing.Color.White;
            this.btn_permissions.UseVisualStyleBackColor = false;
            this.btn_permissions.Visible = false;
            this.btn_permissions.Click += new System.EventHandler(this.ClickEvent_openPermissionsPage);
            // 
            // lbl_buttonVAT
            // 
            this.lbl_buttonVAT.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonVAT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonVAT.ForeColor = System.Drawing.Color.White;
            this.lbl_buttonVAT.Location = new System.Drawing.Point(948, 632);
            this.lbl_buttonVAT.Name = "lbl_buttonVAT";
            this.lbl_buttonVAT.Size = new System.Drawing.Size(56, 15);
            this.lbl_buttonVAT.TabIndex = 1;
            this.lbl_buttonVAT.Text = "VAT";
            this.lbl_buttonVAT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(320, 636);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Apps";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(222, 636);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Drag";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_buttonParameters
            // 
            this.lbl_buttonParameters.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonParameters.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonParameters.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonParameters.Location = new System.Drawing.Point(115, 636);
            this.lbl_buttonParameters.Name = "lbl_buttonParameters";
            this.lbl_buttonParameters.Size = new System.Drawing.Size(72, 15);
            this.lbl_buttonParameters.TabIndex = 1;
            this.lbl_buttonParameters.Text = "Parametri";
            this.lbl_buttonParameters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_buttonPositions
            // 
            this.lbl_buttonPositions.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonPositions.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonPositions.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonPositions.Location = new System.Drawing.Point(21, 636);
            this.lbl_buttonPositions.Name = "lbl_buttonPositions";
            this.lbl_buttonPositions.Size = new System.Drawing.Size(67, 22);
            this.lbl_buttonPositions.TabIndex = 1;
            this.lbl_buttonPositions.Text = "Posizioni";
            this.lbl_buttonPositions.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_buttonConfiguration
            // 
            this.lbl_buttonConfiguration.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonConfiguration.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonConfiguration.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonConfiguration.Location = new System.Drawing.Point(1046, 200);
            this.lbl_buttonConfiguration.Name = "lbl_buttonConfiguration";
            this.lbl_buttonConfiguration.Size = new System.Drawing.Size(60, 20);
            this.lbl_buttonConfiguration.TabIndex = 1;
            this.lbl_buttonConfiguration.Text = "Config";
            this.lbl_buttonConfiguration.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_buttonConfiguration.Visible = false;
            // 
            // btn_configuration
            // 
            this.btn_configuration.BackColor = System.Drawing.SystemColors.Control;
            this.btn_configuration.BackgroundColor = System.Drawing.SystemColors.Control;
            this.btn_configuration.BackgroundImage = global::RM.Properties.Resources.config;
            this.btn_configuration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_configuration.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_configuration.BorderRadius = 15;
            this.btn_configuration.BorderSize = 0;
            this.btn_configuration.FlatAppearance.BorderSize = 0;
            this.btn_configuration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_configuration.ForeColor = System.Drawing.Color.White;
            this.btn_configuration.Location = new System.Drawing.Point(1046, 137);
            this.btn_configuration.Name = "btn_configuration";
            this.btn_configuration.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btn_configuration.Size = new System.Drawing.Size(60, 60);
            this.btn_configuration.TabIndex = 2;
            this.btn_configuration.TextColor = System.Drawing.Color.White;
            this.btn_configuration.UseVisualStyleBackColor = false;
            this.btn_configuration.Visible = false;
            this.btn_configuration.Click += new System.EventHandler(this.ClickEvent_openConfigurationPage);
            // 
            // lbl_buttonPermissions
            // 
            this.lbl_buttonPermissions.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonPermissions.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonPermissions.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonPermissions.Location = new System.Drawing.Point(1043, 104);
            this.lbl_buttonPermissions.Name = "lbl_buttonPermissions";
            this.lbl_buttonPermissions.Size = new System.Drawing.Size(69, 20);
            this.lbl_buttonPermissions.TabIndex = 1;
            this.lbl_buttonPermissions.Text = "Consensi";
            this.lbl_buttonPermissions.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_buttonPermissions.Visible = false;
            // 
            // lbl_buttonAuto
            // 
            this.lbl_buttonAuto.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonAuto.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonAuto.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonAuto.Location = new System.Drawing.Point(204, 339);
            this.lbl_buttonAuto.Name = "lbl_buttonAuto";
            this.lbl_buttonAuto.Size = new System.Drawing.Size(70, 15);
            this.lbl_buttonAuto.TabIndex = 260;
            this.lbl_buttonAuto.Text = "Auto";
            this.lbl_buttonAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_containerMode
            // 
            this.lbl_containerMode.BackColor = System.Drawing.Color.Transparent;
            this.lbl_containerMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_containerMode.ForeColor = System.Drawing.Color.White;
            this.lbl_containerMode.Location = new System.Drawing.Point(27, 225);
            this.lbl_containerMode.Name = "lbl_containerMode";
            this.lbl_containerMode.Size = new System.Drawing.Size(297, 43);
            this.lbl_containerMode.TabIndex = 252;
            this.lbl_containerMode.Text = "Modalità";
            this.lbl_containerMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_buttonManual
            // 
            this.lbl_buttonManual.BackColor = System.Drawing.Color.Transparent;
            this.lbl_buttonManual.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonManual.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonManual.Location = new System.Drawing.Point(78, 339);
            this.lbl_buttonManual.Name = "lbl_buttonManual";
            this.lbl_buttonManual.Size = new System.Drawing.Size(70, 15);
            this.lbl_buttonManual.TabIndex = 258;
            this.lbl_buttonManual.Text = "Manuale";
            this.lbl_buttonManual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_robot
            // 
            this.lbl_robot.BackColor = System.Drawing.Color.Transparent;
            this.lbl_robot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_robot.ForeColor = System.Drawing.Color.White;
            this.lbl_robot.Location = new System.Drawing.Point(26, 397);
            this.lbl_robot.Name = "lbl_robot";
            this.lbl_robot.Size = new System.Drawing.Size(298, 45);
            this.lbl_robot.TabIndex = 252;
            this.lbl_robot.Text = "Robot";
            this.lbl_robot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_homePosition
            // 
            this.lbl_homePosition.BackColor = System.Drawing.Color.Transparent;
            this.lbl_homePosition.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_homePosition.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_homePosition.Location = new System.Drawing.Point(248, 511);
            this.lbl_homePosition.Name = "lbl_homePosition";
            this.lbl_homePosition.Size = new System.Drawing.Size(60, 15);
            this.lbl_homePosition.TabIndex = 250;
            this.lbl_homePosition.Text = "Home";
            this.lbl_homePosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(138, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 252;
            this.label3.Text = "Stop";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_application
            // 
            this.lbl_application.BackColor = System.Drawing.Color.Transparent;
            this.lbl_application.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_application.ForeColor = System.Drawing.Color.White;
            this.lbl_application.Location = new System.Drawing.Point(24, 46);
            this.lbl_application.Name = "lbl_application";
            this.lbl_application.Size = new System.Drawing.Size(302, 47);
            this.lbl_application.TabIndex = 252;
            this.lbl_application.Text = "Applicazione";
            this.lbl_application.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(43, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 250;
            this.label5.Text = "Start";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_velocity
            // 
            this.lbl_velocity.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_velocity.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_velocity.ForeColor = System.Drawing.Color.Black;
            this.lbl_velocity.Location = new System.Drawing.Point(130, 462);
            this.lbl_velocity.Name = "lbl_velocity";
            this.lbl_velocity.Size = new System.Drawing.Size(50, 28);
            this.lbl_velocity.TabIndex = 3;
            this.lbl_velocity.Text = "45";
            this.lbl_velocity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_velocity.Click += new System.EventHandler(this.ClickEvent_setVelocity);
            // 
            // pnl_addSpeed
            // 
            this.pnl_addSpeed.BackColor = System.Drawing.Color.Transparent;
            this.pnl_addSpeed.BackgroundImage = global::RM.Properties.Resources.add32;
            this.pnl_addSpeed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnl_addSpeed.Location = new System.Drawing.Point(130, 504);
            this.pnl_addSpeed.Name = "pnl_addSpeed";
            this.pnl_addSpeed.Size = new System.Drawing.Size(28, 28);
            this.pnl_addSpeed.TabIndex = 175;
            this.pnl_addSpeed.Click += new System.EventHandler(this.ClickEvent_addVelocity);
            // 
            // pnl_removeSpeed
            // 
            this.pnl_removeSpeed.BackColor = System.Drawing.Color.Transparent;
            this.pnl_removeSpeed.BackgroundImage = global::RM.Properties.Resources.minus32;
            this.pnl_removeSpeed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnl_removeSpeed.Location = new System.Drawing.Point(61, 504);
            this.pnl_removeSpeed.Name = "pnl_removeSpeed";
            this.pnl_removeSpeed.Size = new System.Drawing.Size(28, 28);
            this.pnl_removeSpeed.TabIndex = 174;
            this.pnl_removeSpeed.Click += new System.EventHandler(this.ClickEvent_removeVelocity);
            // 
            // lbl_percent
            // 
            this.lbl_percent.BackColor = System.Drawing.Color.Transparent;
            this.lbl_percent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_percent.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_percent.Location = new System.Drawing.Point(191, 467);
            this.lbl_percent.Name = "lbl_percent";
            this.lbl_percent.Size = new System.Drawing.Size(26, 24);
            this.lbl_percent.TabIndex = 169;
            this.lbl_percent.Text = "%";
            // 
            // lbl_robotSpeed
            // 
            this.lbl_robotSpeed.BackColor = System.Drawing.Color.Transparent;
            this.lbl_robotSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_robotSpeed.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_robotSpeed.Location = new System.Drawing.Point(36, 467);
            this.lbl_robotSpeed.Name = "lbl_robotSpeed";
            this.lbl_robotSpeed.Size = new System.Drawing.Size(82, 24);
            this.lbl_robotSpeed.TabIndex = 168;
            this.lbl_robotSpeed.Text = "Velocità:";
            // 
            // lb_applicationToExecute
            // 
            this.lb_applicationToExecute.BackColor = System.Drawing.Color.Black;
            this.lb_applicationToExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lb_applicationToExecute.ForeColor = System.Drawing.Color.White;
            this.lb_applicationToExecute.Location = new System.Drawing.Point(0, -4);
            this.lb_applicationToExecute.Name = "lb_applicationToExecute";
            this.lb_applicationToExecute.Size = new System.Drawing.Size(1024, 34);
            this.lb_applicationToExecute.TabIndex = 265;
            this.lb_applicationToExecute.Text = "Selezionare un\'applicazione";
            this.lb_applicationToExecute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_startDemo
            // 
            this.btn_startDemo.BackColor = System.Drawing.SystemColors.Control;
            this.btn_startDemo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.btn_startDemo.BackgroundImage = global::RM.Properties.Resources.demo;
            this.btn_startDemo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_startDemo.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_startDemo.BorderRadius = 20;
            this.btn_startDemo.BorderSize = 0;
            this.btn_startDemo.Enabled = false;
            this.btn_startDemo.FlatAppearance.BorderSize = 0;
            this.btn_startDemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_startDemo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_startDemo.ForeColor = System.Drawing.Color.Black;
            this.btn_startDemo.Location = new System.Drawing.Point(277, 49);
            this.btn_startDemo.Name = "btn_startDemo";
            this.btn_startDemo.Size = new System.Drawing.Size(40, 40);
            this.btn_startDemo.TabIndex = 266;
            this.btn_startDemo.TextColor = System.Drawing.Color.Black;
            this.btn_startDemo.UseVisualStyleBackColor = false;
            this.btn_startDemo.Visible = false;
            this.btn_startDemo.Click += new System.EventHandler(this.btn_startDemo_Click);
            // 
            // lw_positions
            // 
            this.lw_positions.BackColor = System.Drawing.Color.White;
            this.lw_positions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lw_positions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lw_positions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lw_positions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lw_positions.ForeColor = System.Drawing.Color.Black;
            this.lw_positions.FullRowSelect = true;
            this.lw_positions.HideSelection = false;
            this.lw_positions.LabelWrap = false;
            this.lw_positions.Location = new System.Drawing.Point(365, 53);
            this.lw_positions.MultiSelect = false;
            this.lw_positions.Name = "lw_positions";
            this.lw_positions.Size = new System.Drawing.Size(640, 488);
            this.lw_positions.TabIndex = 269;
            this.lw_positions.UseCompatibleStateImageBehavior = false;
            this.lw_positions.View = System.Windows.Forms.View.Details;
            this.lw_positions.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lw_positions_DrawColumnHeader);
            this.lw_positions.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lw_positions_DrawItem);
            this.lw_positions.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lw_positions_DrawSubItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader
            // 
            this.columnHeader.Text = "Feed air";
            this.columnHeader.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Dosage air";
            this.columnHeader11.Width = 100;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Gun air";
            this.columnHeader12.Width = 100;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "μ Ampere";
            this.columnHeader13.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "K Volt";
            this.columnHeader14.Width = 90;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Status";
            this.columnHeader15.Width = 100;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(239, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 15);
            this.label8.TabIndex = 270;
            this.label8.Text = "Pausa";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_positions
            // 
            this.btn_positions.BackgroundImage = global::RM.Properties.Resources.positions;
            this.btn_positions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_positions.Location = new System.Drawing.Point(28, 574);
            this.btn_positions.Name = "btn_positions";
            this.btn_positions.Size = new System.Drawing.Size(55, 55);
            this.btn_positions.TabIndex = 272;
            this.btn_positions.UseVisualStyleBackColor = true;
            this.btn_positions.Click += new System.EventHandler(this.ClickEvent_openPositions);
            // 
            // btn_parameters
            // 
            this.btn_parameters.BackgroundImage = global::RM.Properties.Resources.parameters;
            this.btn_parameters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_parameters.Location = new System.Drawing.Point(124, 574);
            this.btn_parameters.Name = "btn_parameters";
            this.btn_parameters.Size = new System.Drawing.Size(55, 55);
            this.btn_parameters.TabIndex = 273;
            this.btn_parameters.UseVisualStyleBackColor = true;
            this.btn_parameters.Click += new System.EventHandler(this.ClickEvent_openParameters);
            // 
            // btn_dragMode
            // 
            this.btn_dragMode.BackgroundImage = global::RM.Properties.Resources.drag;
            this.btn_dragMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_dragMode.Location = new System.Drawing.Point(224, 574);
            this.btn_dragMode.Name = "btn_dragMode";
            this.btn_dragMode.Size = new System.Drawing.Size(55, 55);
            this.btn_dragMode.TabIndex = 274;
            this.btn_dragMode.UseVisualStyleBackColor = true;
            this.btn_dragMode.Click += new System.EventHandler(this.ClickEvent_openDragMode);
            // 
            // btn_applications
            // 
            this.btn_applications.BackgroundImage = global::RM.Properties.Resources.apps;
            this.btn_applications.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_applications.Location = new System.Drawing.Point(324, 574);
            this.btn_applications.Name = "btn_applications";
            this.btn_applications.Size = new System.Drawing.Size(55, 55);
            this.btn_applications.TabIndex = 275;
            this.btn_applications.UseVisualStyleBackColor = true;
            this.btn_applications.Click += new System.EventHandler(this.ClickEvent_openApplications);
            // 
            // btn_security
            // 
            this.btn_security.BackColor = System.Drawing.SystemColors.Highlight;
            this.btn_security.BackgroundImage = global::RM.Properties.Resources.security_filled;
            this.btn_security.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_security.Location = new System.Drawing.Point(424, 574);
            this.btn_security.Name = "btn_security";
            this.btn_security.Size = new System.Drawing.Size(55, 55);
            this.btn_security.TabIndex = 276;
            this.btn_security.UseVisualStyleBackColor = false;
            this.btn_security.Click += new System.EventHandler(this.ClickEvent_openSecurityManager);
            // 
            // btn_alarms
            // 
            this.btn_alarms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_alarms.BackgroundImage = global::RM.Properties.Resources.alarms_outlined;
            this.btn_alarms.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_alarms.Location = new System.Drawing.Point(524, 574);
            this.btn_alarms.Name = "btn_alarms";
            this.btn_alarms.Size = new System.Drawing.Size(55, 55);
            this.btn_alarms.TabIndex = 277;
            this.btn_alarms.UseVisualStyleBackColor = false;
            this.btn_alarms.Click += new System.EventHandler(this.ClickEvent_openAlarmPage);
            // 
            // btn_exit
            // 
            this.btn_exit.BackgroundImage = global::RM.Properties.Resources.exit_filled;
            this.btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_exit.Location = new System.Drawing.Point(624, 574);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(55, 55);
            this.btn_exit.TabIndex = 278;
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.ClickEvent_exit);
            // 
            // btn_VAT
            // 
            this.btn_VAT.BackgroundImage = global::RM.Properties.Resources.vatView;
            this.btn_VAT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_VAT.Location = new System.Drawing.Point(949, 574);
            this.btn_VAT.Name = "btn_VAT";
            this.btn_VAT.Size = new System.Drawing.Size(55, 55);
            this.btn_VAT.TabIndex = 280;
            this.btn_VAT.UseVisualStyleBackColor = true;
            this.btn_VAT.Click += new System.EventHandler(this.ClickEvent_openVAT);
            // 
            // btn_startApp
            // 
            this.btn_startApp.BackColor = System.Drawing.SystemColors.Control;
            this.btn_startApp.BackgroundImage = global::RM.Properties.Resources.play32;
            this.btn_startApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_startApp.Location = new System.Drawing.Point(47, 106);
            this.btn_startApp.Name = "btn_startApp";
            this.btn_startApp.Size = new System.Drawing.Size(55, 55);
            this.btn_startApp.TabIndex = 281;
            this.btn_startApp.UseVisualStyleBackColor = false;
            this.btn_startApp.Click += new System.EventHandler(this.ClickEvent_startApp);
            // 
            // btn_pauseApp
            // 
            this.btn_pauseApp.BackColor = System.Drawing.SystemColors.Control;
            this.btn_pauseApp.BackgroundImage = global::RM.Properties.Resources.pausemonitoringRed_32;
            this.btn_pauseApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_pauseApp.Location = new System.Drawing.Point(247, 106);
            this.btn_pauseApp.Name = "btn_pauseApp";
            this.btn_pauseApp.Size = new System.Drawing.Size(55, 55);
            this.btn_pauseApp.TabIndex = 282;
            this.btn_pauseApp.UseVisualStyleBackColor = false;
            this.btn_pauseApp.Click += new System.EventHandler(this.ClickEvent_pauseApp);
            // 
            // btn_stopApp
            // 
            this.btn_stopApp.BackColor = System.Drawing.SystemColors.Control;
            this.btn_stopApp.BackgroundImage = global::RM.Properties.Resources.stop;
            this.btn_stopApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_stopApp.Location = new System.Drawing.Point(147, 106);
            this.btn_stopApp.Name = "btn_stopApp";
            this.btn_stopApp.Size = new System.Drawing.Size(55, 55);
            this.btn_stopApp.TabIndex = 283;
            this.btn_stopApp.UseVisualStyleBackColor = false;
            this.btn_stopApp.Click += new System.EventHandler(this.ClickEvent_stopApp);
            // 
            // btn_manualMode
            // 
            this.btn_manualMode.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_manualMode.BackgroundImage = global::RM.Properties.Resources.letter_m;
            this.btn_manualMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_manualMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_manualMode.Location = new System.Drawing.Point(85, 278);
            this.btn_manualMode.Name = "btn_manualMode";
            this.btn_manualMode.Size = new System.Drawing.Size(55, 55);
            this.btn_manualMode.TabIndex = 284;
            this.btn_manualMode.UseVisualStyleBackColor = false;
            this.btn_manualMode.Click += new System.EventHandler(this.ClickEvent_manualMode);
            // 
            // btn_autoMode
            // 
            this.btn_autoMode.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_autoMode.BackgroundImage = global::RM.Properties.Resources.a;
            this.btn_autoMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_autoMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_autoMode.Location = new System.Drawing.Point(211, 279);
            this.btn_autoMode.Name = "btn_autoMode";
            this.btn_autoMode.Size = new System.Drawing.Size(55, 55);
            this.btn_autoMode.TabIndex = 285;
            this.btn_autoMode.UseVisualStyleBackColor = false;
            this.btn_autoMode.Click += new System.EventHandler(this.ClickEvent_autoMode);
            // 
            // btn_homePosition
            // 
            this.btn_homePosition.BackColor = System.Drawing.SystemColors.Control;
            this.btn_homePosition.BackgroundImage = global::RM.Properties.Resources.home;
            this.btn_homePosition.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_homePosition.Location = new System.Drawing.Point(250, 451);
            this.btn_homePosition.Name = "btn_homePosition";
            this.btn_homePosition.Size = new System.Drawing.Size(55, 55);
            this.btn_homePosition.TabIndex = 286;
            this.btn_homePosition.UseVisualStyleBackColor = false;
            this.btn_homePosition.Click += new System.EventHandler(this.ClickEvent_GoToHomePosition);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::RM.Properties.Resources.apps;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Location = new System.Drawing.Point(887, 574);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 55);
            this.button1.TabIndex = 287;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::RM.Properties.Resources.exit_filled;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(750, 574);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 55);
            this.button2.TabIndex = 288;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::RM.Properties.Resources.exit_filled;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Location = new System.Drawing.Point(811, 574);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 55);
            this.button3.TabIndex = 289;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // UC_HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::RM.Properties.Resources._20250317_UC_homePage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_homePosition);
            this.Controls.Add(this.btn_autoMode);
            this.Controls.Add(this.btn_manualMode);
            this.Controls.Add(this.btn_stopApp);
            this.Controls.Add(this.btn_pauseApp);
            this.Controls.Add(this.btn_startApp);
            this.Controls.Add(this.btn_VAT);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_alarms);
            this.Controls.Add(this.btn_security);
            this.Controls.Add(this.btn_applications);
            this.Controls.Add(this.btn_dragMode);
            this.Controls.Add(this.btn_parameters);
            this.Controls.Add(this.btn_positions);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lbl_buttonVAT);
            this.Controls.Add(this.btn_startDemo);
            this.Controls.Add(this.lbl_percent);
            this.Controls.Add(this.pnl_addSpeed);
            this.Controls.Add(this.lbl_velocity);
            this.Controls.Add(this.pnl_removeSpeed);
            this.Controls.Add(this.lbl_buttonExit);
            this.Controls.Add(this.lb_applicationToExecute);
            this.Controls.Add(this.lbl_robotSpeed);
            this.Controls.Add(this.lbl_buttonAlarms);
            this.Controls.Add(this.lbl_buttonSecurity);
            this.Controls.Add(this.lbl_application);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_permissions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_containerMode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_buttonParameters);
            this.Controls.Add(this.lbl_robot);
            this.Controls.Add(this.lbl_buttonPositions);
            this.Controls.Add(this.lbl_buttonManual);
            this.Controls.Add(this.lbl_buttonConfiguration);
            this.Controls.Add(this.btn_configuration);
            this.Controls.Add(this.lbl_buttonPermissions);
            this.Controls.Add(this.lbl_buttonAuto);
            this.Controls.Add(this.lbl_homePosition);
            this.Controls.Add(this.lw_positions);
            this.DoubleBuffered = true;
            this.Name = "UC_HomePage";
            this.Size = new System.Drawing.Size(1024, 658);
            this.Tag = "UC_HomePage";
            this.Load += new System.EventHandler(this.UC_HomePage_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_buttonVAT;
        private System.Windows.Forms.Label lbl_buttonAlarms;
        private RMLib.View.CustomButton btn_permissions;
        private System.Windows.Forms.Label lbl_buttonPermissions;
        private RMLib.View.CustomButton btn_configuration;
        private System.Windows.Forms.Label lbl_buttonConfiguration;
        private System.Windows.Forms.Label lbl_buttonExit;
        private System.Windows.Forms.Label lbl_buttonSecurity;
        private System.Windows.Forms.Label lbl_buttonParameters;
        private System.Windows.Forms.Label lbl_buttonPositions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_buttonAuto;
        private System.Windows.Forms.Label lbl_containerMode;
        private System.Windows.Forms.Label lbl_buttonManual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_robot;
        private System.Windows.Forms.Label lbl_homePosition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_application;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnl_addSpeed;
        private System.Windows.Forms.Panel pnl_removeSpeed;
        private System.Windows.Forms.Label lbl_percent;
        private System.Windows.Forms.Label lbl_velocity;
        private System.Windows.Forms.Label lbl_robotSpeed;
        private System.Windows.Forms.Label lb_applicationToExecute;
        private RMLib.View.CustomButton btn_startDemo;
        private RMLib.View.ScrollableListView lw_positions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_positions;
        private System.Windows.Forms.Button btn_parameters;
        private System.Windows.Forms.Button btn_dragMode;
        private System.Windows.Forms.Button btn_applications;
        private System.Windows.Forms.Button btn_security;
        private System.Windows.Forms.Button btn_alarms;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_VAT;
        private System.Windows.Forms.Button btn_startApp;
        private System.Windows.Forms.Button btn_pauseApp;
        private System.Windows.Forms.Button btn_stopApp;
        private System.Windows.Forms.Button btn_manualMode;
        private System.Windows.Forms.Button btn_autoMode;
        private System.Windows.Forms.Button btn_homePosition;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
