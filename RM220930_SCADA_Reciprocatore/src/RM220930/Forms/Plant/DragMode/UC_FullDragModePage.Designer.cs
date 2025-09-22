namespace RM.src.RM220930.Forms.DragMode
{
    partial class UC_FullDragModePage
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
            this.lw_positions = new RMLib.View.ScrollableListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDragMode = new System.Windows.Forms.Label();
            this.lbl_choosenApplication = new System.Windows.Forms.Label();
            this.lbl_choosenModeText = new System.Windows.Forms.Label();
            this.lbl_currentTime = new System.Windows.Forms.Label();
            this.lbl_currentTimeText = new System.Windows.Forms.Label();
            this.lbl_currentPoint = new System.Windows.Forms.Label();
            this.lbl_currentPointText = new System.Windows.Forms.Label();
            this.lbl_Monitor = new System.Windows.Forms.Label();
            this.lbl_saveOperation = new System.Windows.Forms.Label();
            this.lbl_cancelOperation = new System.Windows.Forms.Label();
            this.lbl_buttonConfiguration = new System.Windows.Forms.Label();
            this.lbl_home = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_STOP = new System.Windows.Forms.Button();
            this.btn_homePage = new System.Windows.Forms.Button();
            this.btn_PLAY = new System.Windows.Forms.Button();
            this.btn_saveOperation = new System.Windows.Forms.Button();
            this.btn_cancelOperation = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_trackerCalibration = new System.Windows.Forms.Button();
            this.btn_showColumns = new System.Windows.Forms.Button();
            this.btn_debugSettings = new System.Windows.Forms.Button();
            this.btn_gunSettings = new System.Windows.Forms.Button();
            this.btn_debugTools = new System.Windows.Forms.Button();
            this.pb_LoadingGif = new System.Windows.Forms.PictureBox();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_LoadingGif)).BeginInit();
            this.SuspendLayout();
            // 
            // lw_positions
            // 
            this.lw_positions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lw_positions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lw_positions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lw_positions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lw_positions.FullRowSelect = true;
            this.lw_positions.HideSelection = false;
            this.lw_positions.LabelWrap = false;
            this.lw_positions.Location = new System.Drawing.Point(-1, 37);
            this.lw_positions.MultiSelect = false;
            this.lw_positions.Name = "lw_positions";
            this.lw_positions.Size = new System.Drawing.Size(895, 417);
            this.lw_positions.TabIndex = 67;
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
            // columnHeader2
            // 
            this.columnHeader2.Text = "Timestamp";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Mode";
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "x";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 0;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "y";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 0;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "z";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "rx";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 0;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "ry";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 0;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "rz";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 0;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Feed air";
            this.columnHeader10.Width = 100;
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
            this.columnHeader14.Width = 100;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Status";
            this.columnHeader15.Width = 110;
            // 
            // lblDragMode
            // 
            this.lblDragMode.BackColor = System.Drawing.Color.Transparent;
            this.lblDragMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblDragMode.ForeColor = System.Drawing.Color.Black;
            this.lblDragMode.Location = new System.Drawing.Point(718, 478);
            this.lblDragMode.Name = "lblDragMode";
            this.lblDragMode.Size = new System.Drawing.Size(174, 34);
            this.lblDragMode.TabIndex = 124;
            this.lblDragMode.Text = "Point to point";
            this.lblDragMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_choosenApplication
            // 
            this.lbl_choosenApplication.BackColor = System.Drawing.Color.Black;
            this.lbl_choosenApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_choosenApplication.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_choosenApplication.Location = new System.Drawing.Point(0, 0);
            this.lbl_choosenApplication.Name = "lbl_choosenApplication";
            this.lbl_choosenApplication.Size = new System.Drawing.Size(895, 37);
            this.lbl_choosenApplication.TabIndex = 120;
            this.lbl_choosenApplication.Text = "-";
            this.lbl_choosenApplication.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_choosenModeText
            // 
            this.lbl_choosenModeText.BackColor = System.Drawing.Color.Transparent;
            this.lbl_choosenModeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_choosenModeText.ForeColor = System.Drawing.Color.Black;
            this.lbl_choosenModeText.Location = new System.Drawing.Point(554, 478);
            this.lbl_choosenModeText.Name = "lbl_choosenModeText";
            this.lbl_choosenModeText.Size = new System.Drawing.Size(228, 34);
            this.lbl_choosenModeText.TabIndex = 117;
            this.lbl_choosenModeText.Text = "Modalità scelta:";
            this.lbl_choosenModeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_currentTime
            // 
            this.lbl_currentTime.BackColor = System.Drawing.Color.Transparent;
            this.lbl_currentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_currentTime.ForeColor = System.Drawing.Color.Black;
            this.lbl_currentTime.Location = new System.Drawing.Point(259, 484);
            this.lbl_currentTime.Name = "lbl_currentTime";
            this.lbl_currentTime.Size = new System.Drawing.Size(141, 50);
            this.lbl_currentTime.TabIndex = 109;
            this.lbl_currentTime.Text = "-";
            this.lbl_currentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_currentTimeText
            // 
            this.lbl_currentTimeText.BackColor = System.Drawing.Color.Transparent;
            this.lbl_currentTimeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_currentTimeText.ForeColor = System.Drawing.Color.Black;
            this.lbl_currentTimeText.Location = new System.Drawing.Point(100, 484);
            this.lbl_currentTimeText.Name = "lbl_currentTimeText";
            this.lbl_currentTimeText.Size = new System.Drawing.Size(151, 50);
            this.lbl_currentTimeText.TabIndex = 108;
            this.lbl_currentTimeText.Text = "Tempo corrente:";
            this.lbl_currentTimeText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_currentPoint
            // 
            this.lbl_currentPoint.BackColor = System.Drawing.Color.Transparent;
            this.lbl_currentPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_currentPoint.ForeColor = System.Drawing.Color.Black;
            this.lbl_currentPoint.Location = new System.Drawing.Point(257, 455);
            this.lbl_currentPoint.Name = "lbl_currentPoint";
            this.lbl_currentPoint.Size = new System.Drawing.Size(141, 43);
            this.lbl_currentPoint.TabIndex = 107;
            this.lbl_currentPoint.Text = "0";
            this.lbl_currentPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_currentPointText
            // 
            this.lbl_currentPointText.BackColor = System.Drawing.Color.Transparent;
            this.lbl_currentPointText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_currentPointText.ForeColor = System.Drawing.Color.Black;
            this.lbl_currentPointText.Location = new System.Drawing.Point(100, 455);
            this.lbl_currentPointText.Name = "lbl_currentPointText";
            this.lbl_currentPointText.Size = new System.Drawing.Size(137, 43);
            this.lbl_currentPointText.TabIndex = 106;
            this.lbl_currentPointText.Text = "Punto corrente: ";
            this.lbl_currentPointText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Monitor
            // 
            this.lbl_Monitor.BackColor = System.Drawing.Color.Black;
            this.lbl_Monitor.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Monitor.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_Monitor.Location = new System.Drawing.Point(431, 565);
            this.lbl_Monitor.Name = "lbl_Monitor";
            this.lbl_Monitor.Size = new System.Drawing.Size(144, 40);
            this.lbl_Monitor.TabIndex = 114;
            this.lbl_Monitor.Text = "Drag mode: OFF";
            this.lbl_Monitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_saveOperation
            // 
            this.lbl_saveOperation.BackColor = System.Drawing.Color.Black;
            this.lbl_saveOperation.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_saveOperation.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_saveOperation.Location = new System.Drawing.Point(934, 79);
            this.lbl_saveOperation.Name = "lbl_saveOperation";
            this.lbl_saveOperation.Size = new System.Drawing.Size(60, 20);
            this.lbl_saveOperation.TabIndex = 1;
            this.lbl_saveOperation.Text = "Salva";
            this.lbl_saveOperation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_saveOperation.Visible = false;
            // 
            // lbl_cancelOperation
            // 
            this.lbl_cancelOperation.BackColor = System.Drawing.Color.Black;
            this.lbl_cancelOperation.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cancelOperation.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_cancelOperation.Location = new System.Drawing.Point(830, 79);
            this.lbl_cancelOperation.Name = "lbl_cancelOperation";
            this.lbl_cancelOperation.Size = new System.Drawing.Size(68, 20);
            this.lbl_cancelOperation.TabIndex = 1;
            this.lbl_cancelOperation.Text = "Annulla";
            this.lbl_cancelOperation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_cancelOperation.Visible = false;
            // 
            // lbl_buttonConfiguration
            // 
            this.lbl_buttonConfiguration.BackColor = System.Drawing.Color.Black;
            this.lbl_buttonConfiguration.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonConfiguration.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonConfiguration.Location = new System.Drawing.Point(103, 79);
            this.lbl_buttonConfiguration.Name = "lbl_buttonConfiguration";
            this.lbl_buttonConfiguration.Size = new System.Drawing.Size(92, 20);
            this.lbl_buttonConfiguration.TabIndex = 1;
            this.lbl_buttonConfiguration.Text = "Applicazione";
            this.lbl_buttonConfiguration.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_buttonConfiguration.Visible = false;
            // 
            // lbl_home
            // 
            this.lbl_home.BackColor = System.Drawing.Color.Black;
            this.lbl_home.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_home.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_home.Location = new System.Drawing.Point(24, 79);
            this.lbl_home.Name = "lbl_home";
            this.lbl_home.Size = new System.Drawing.Size(60, 20);
            this.lbl_home.TabIndex = 1;
            this.lbl_home.Text = "Home";
            this.lbl_home.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.btn_add);
            this.panel5.Controls.Add(this.btn_STOP);
            this.panel5.Controls.Add(this.btn_homePage);
            this.panel5.Controls.Add(this.btn_PLAY);
            this.panel5.Controls.Add(this.lbl_saveOperation);
            this.panel5.Controls.Add(this.btn_saveOperation);
            this.panel5.Controls.Add(this.btn_cancelOperation);
            this.panel5.Controls.Add(this.lbl_cancelOperation);
            this.panel5.Controls.Add(this.lbl_home);
            this.panel5.Controls.Add(this.lbl_buttonConfiguration);
            this.panel5.Location = new System.Drawing.Point(0, 556);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1025, 102);
            this.panel5.TabIndex = 324;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::RM.Properties.Resources.stop;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Location = new System.Drawing.Point(675, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 55);
            this.button2.TabIndex = 294;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::RM.Properties.Resources.play32;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Location = new System.Drawing.Point(729, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 55);
            this.button1.TabIndex = 293;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_add
            // 
            this.btn_add.BackgroundImage = global::RM.Properties.Resources.addApp32;
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_add.Location = new System.Drawing.Point(124, 19);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(55, 55);
            this.btn_add.TabIndex = 288;
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Visible = false;
            this.btn_add.Click += new System.EventHandler(this.ClickEvent_addApplication);
            // 
            // btn_STOP
            // 
            this.btn_STOP.BackgroundImage = global::RM.Properties.Resources.stop;
            this.btn_STOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_STOP.Location = new System.Drawing.Point(347, 19);
            this.btn_STOP.Name = "btn_STOP";
            this.btn_STOP.Size = new System.Drawing.Size(55, 55);
            this.btn_STOP.TabIndex = 289;
            this.btn_STOP.UseVisualStyleBackColor = true;
            this.btn_STOP.Visible = false;
            this.btn_STOP.Click += new System.EventHandler(this.ClickEvent_StopDrag);
            // 
            // btn_homePage
            // 
            this.btn_homePage.BackgroundImage = global::RM.Properties.Resources.back32;
            this.btn_homePage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_homePage.Location = new System.Drawing.Point(28, 18);
            this.btn_homePage.Name = "btn_homePage";
            this.btn_homePage.Size = new System.Drawing.Size(55, 55);
            this.btn_homePage.TabIndex = 287;
            this.btn_homePage.UseVisualStyleBackColor = true;
            this.btn_homePage.Click += new System.EventHandler(this.ClickEvent_HomePage);
            // 
            // btn_PLAY
            // 
            this.btn_PLAY.BackgroundImage = global::RM.Properties.Resources.play32;
            this.btn_PLAY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_PLAY.Location = new System.Drawing.Point(596, 18);
            this.btn_PLAY.Name = "btn_PLAY";
            this.btn_PLAY.Size = new System.Drawing.Size(55, 55);
            this.btn_PLAY.TabIndex = 290;
            this.btn_PLAY.UseVisualStyleBackColor = true;
            this.btn_PLAY.Visible = false;
            this.btn_PLAY.Click += new System.EventHandler(this.ClickEvent_StartDrag);
            // 
            // btn_saveOperation
            // 
            this.btn_saveOperation.BackgroundImage = global::RM.Properties.Resources.save32;
            this.btn_saveOperation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_saveOperation.Location = new System.Drawing.Point(937, 18);
            this.btn_saveOperation.Name = "btn_saveOperation";
            this.btn_saveOperation.Size = new System.Drawing.Size(55, 55);
            this.btn_saveOperation.TabIndex = 292;
            this.btn_saveOperation.UseVisualStyleBackColor = true;
            this.btn_saveOperation.Visible = false;
            this.btn_saveOperation.Click += new System.EventHandler(this.ClickEvent_saveOperation);
            // 
            // btn_cancelOperation
            // 
            this.btn_cancelOperation.BackgroundImage = global::RM.Properties.Resources.cancelOperation_32;
            this.btn_cancelOperation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_cancelOperation.Location = new System.Drawing.Point(837, 18);
            this.btn_cancelOperation.Name = "btn_cancelOperation";
            this.btn_cancelOperation.Size = new System.Drawing.Size(55, 55);
            this.btn_cancelOperation.TabIndex = 291;
            this.btn_cancelOperation.UseVisualStyleBackColor = true;
            this.btn_cancelOperation.Visible = false;
            this.btn_cancelOperation.Click += new System.EventHandler(this.ClickEvent_cancelOperation);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblDragMode);
            this.panel1.Controls.Add(this.lbl_currentTimeText);
            this.panel1.Controls.Add(this.lbl_currentPoint);
            this.panel1.Controls.Add(this.lbl_currentPointText);
            this.panel1.Controls.Add(this.lbl_currentTime);
            this.panel1.Controls.Add(this.lw_positions);
            this.panel1.Controls.Add(this.lbl_choosenModeText);
            this.panel1.Controls.Add(this.lbl_choosenApplication);
            this.panel1.Location = new System.Drawing.Point(61, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(895, 535);
            this.panel1.TabIndex = 328;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::RM.Properties.Resources.dragWhite32;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.TabIndex = 324;
            this.pictureBox1.TabStop = false;
            // 
            // btn_trackerCalibration
            // 
            this.btn_trackerCalibration.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_trackerCalibration.BackgroundImage = global::RM.Properties.Resources.tracker_icon;
            this.btn_trackerCalibration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_trackerCalibration.Location = new System.Drawing.Point(962, 371);
            this.btn_trackerCalibration.Name = "btn_trackerCalibration";
            this.btn_trackerCalibration.Size = new System.Drawing.Size(56, 80);
            this.btn_trackerCalibration.TabIndex = 329;
            this.btn_trackerCalibration.UseVisualStyleBackColor = false;
            this.btn_trackerCalibration.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_showColumns
            // 
            this.btn_showColumns.BackgroundImage = global::RM.Properties.Resources.airbrush;
            this.btn_showColumns.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_showColumns.Location = new System.Drawing.Point(962, 4);
            this.btn_showColumns.Name = "btn_showColumns";
            this.btn_showColumns.Size = new System.Drawing.Size(56, 56);
            this.btn_showColumns.TabIndex = 327;
            this.btn_showColumns.UseVisualStyleBackColor = true;
            this.btn_showColumns.Click += new System.EventHandler(this.ClickEvent_showHiddenColumns);
            // 
            // btn_debugSettings
            // 
            this.btn_debugSettings.BackgroundImage = global::RM.Properties.Resources.settings32;
            this.btn_debugSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_debugSettings.Location = new System.Drawing.Point(962, 211);
            this.btn_debugSettings.Name = "btn_debugSettings";
            this.btn_debugSettings.Size = new System.Drawing.Size(56, 80);
            this.btn_debugSettings.TabIndex = 326;
            this.btn_debugSettings.UseVisualStyleBackColor = true;
            this.btn_debugSettings.Click += new System.EventHandler(this.ClickEvent_debugSettings);
            // 
            // btn_gunSettings
            // 
            this.btn_gunSettings.BackgroundImage = global::RM.Properties.Resources.airbrush;
            this.btn_gunSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_gunSettings.Location = new System.Drawing.Point(962, 291);
            this.btn_gunSettings.Name = "btn_gunSettings";
            this.btn_gunSettings.Size = new System.Drawing.Size(56, 80);
            this.btn_gunSettings.TabIndex = 325;
            this.btn_gunSettings.UseVisualStyleBackColor = true;
            this.btn_gunSettings.Click += new System.EventHandler(this.ClickEvent_openGunSettings);
            // 
            // btn_debugTools
            // 
            this.btn_debugTools.BackgroundImage = global::RM.Properties.Resources.tools32;
            this.btn_debugTools.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_debugTools.Location = new System.Drawing.Point(962, 131);
            this.btn_debugTools.Name = "btn_debugTools";
            this.btn_debugTools.Size = new System.Drawing.Size(56, 80);
            this.btn_debugTools.TabIndex = 293;
            this.btn_debugTools.UseVisualStyleBackColor = true;
            this.btn_debugTools.Click += new System.EventHandler(this.ClickEvent_debugTools);
            // 
            // pb_LoadingGif
            // 
            this.pb_LoadingGif.BackColor = System.Drawing.Color.Black;
            this.pb_LoadingGif.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_LoadingGif.Image = global::RM.Properties.Resources.loading_gif;
            this.pb_LoadingGif.Location = new System.Drawing.Point(479, 608);
            this.pb_LoadingGif.Name = "pb_LoadingGif";
            this.pb_LoadingGif.Size = new System.Drawing.Size(45, 40);
            this.pb_LoadingGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_LoadingGif.TabIndex = 115;
            this.pb_LoadingGif.TabStop = false;
            this.pb_LoadingGif.Visible = false;
            // 
            // UC_FullDragModePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Controls.Add(this.btn_trackerCalibration);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_showColumns);
            this.Controls.Add(this.btn_debugSettings);
            this.Controls.Add(this.btn_gunSettings);
            this.Controls.Add(this.btn_debugTools);
            this.Controls.Add(this.lbl_Monitor);
            this.Controls.Add(this.pb_LoadingGif);
            this.Controls.Add(this.panel5);
            this.Name = "UC_FullDragModePage";
            this.Size = new System.Drawing.Size(1024, 658);
            this.Load += new System.EventHandler(this.UC_FullDragModePage_Load);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_LoadingGif)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_choosenApplication;
        private System.Windows.Forms.Label lbl_choosenModeText;
        private System.Windows.Forms.PictureBox pb_LoadingGif;
        private System.Windows.Forms.Label lbl_Monitor;
        private System.Windows.Forms.Label lbl_currentTime;
        private System.Windows.Forms.Label lbl_currentTimeText;
        private System.Windows.Forms.Label lbl_currentPoint;
        private System.Windows.Forms.Label lbl_currentPointText;
        private RMLib.View.ScrollableListView lw_positions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Label lblDragMode;
        private System.Windows.Forms.Label lbl_saveOperation;
        private System.Windows.Forms.Label lbl_cancelOperation;
        private System.Windows.Forms.Label lbl_home;
        private System.Windows.Forms.Label lbl_buttonConfiguration;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.Button btn_homePage;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_STOP;
        private System.Windows.Forms.Button btn_PLAY;
        private System.Windows.Forms.Button btn_cancelOperation;
        private System.Windows.Forms.Button btn_saveOperation;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_debugTools;
        private System.Windows.Forms.Button btn_gunSettings;
        private System.Windows.Forms.Button btn_debugSettings;
        private System.Windows.Forms.Button btn_showColumns;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_trackerCalibration;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}
