namespace RM.src.RM240513.Forms.Plant.ViveTracker
{
    partial class UC_ViveTrackerPage
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_home = new System.Windows.Forms.Button();
            this.lbl_buttonHome = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.lbl_rzVal = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lbl_ryVal = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lbl_rxVal = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lbl_zVal = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbl_yVal = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbl_xVal = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_stopLive = new System.Windows.Forms.Button();
            this.btn_startLive = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_stopRec = new System.Windows.Forms.Button();
            this.btn_startRec = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lw_positions = new RMLib.View.ScrollableListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_stopMov = new System.Windows.Forms.Button();
            this.btn_startMov = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnl_recStatus = new System.Windows.Forms.Panel();
            this.pnl_liveStatus = new System.Windows.Forms.Panel();
            this.pnl_movementStatus = new System.Windows.Forms.Panel();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Controls.Add(this.btn_home);
            this.panel5.Controls.Add(this.lbl_buttonHome);
            this.panel5.Location = new System.Drawing.Point(0, 556);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1025, 102);
            this.panel5.TabIndex = 324;
            // 
            // btn_home
            // 
            this.btn_home.BackgroundImage = global::RM.Properties.Resources.back32;
            this.btn_home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_home.Location = new System.Drawing.Point(28, 18);
            this.btn_home.Name = "btn_home";
            this.btn_home.Size = new System.Drawing.Size(55, 55);
            this.btn_home.TabIndex = 326;
            this.btn_home.UseVisualStyleBackColor = true;
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // lbl_buttonHome
            // 
            this.lbl_buttonHome.BackColor = System.Drawing.Color.Black;
            this.lbl_buttonHome.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_buttonHome.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_buttonHome.Location = new System.Drawing.Point(24, 79);
            this.lbl_buttonHome.Name = "lbl_buttonHome";
            this.lbl_buttonHome.Size = new System.Drawing.Size(60, 22);
            this.lbl_buttonHome.TabIndex = 1;
            this.lbl_buttonHome.Text = "Home";
            this.lbl_buttonHome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel12);
            this.panel1.Controls.Add(this.panel11);
            this.panel1.Controls.Add(this.panel10);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Location = new System.Drawing.Point(17, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 243);
            this.panel1.TabIndex = 325;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.SystemColors.Window;
            this.panel12.Controls.Add(this.lbl_rzVal);
            this.panel12.Controls.Add(this.label23);
            this.panel12.Location = new System.Drawing.Point(246, 150);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(80, 80);
            this.panel12.TabIndex = 329;
            // 
            // lbl_rzVal
            // 
            this.lbl_rzVal.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_rzVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_rzVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_rzVal.ForeColor = System.Drawing.Color.Black;
            this.lbl_rzVal.Location = new System.Drawing.Point(0, 0);
            this.lbl_rzVal.Name = "lbl_rzVal";
            this.lbl_rzVal.Size = new System.Drawing.Size(80, 60);
            this.lbl_rzVal.TabIndex = 268;
            this.lbl_rzVal.Text = "0.00";
            this.lbl_rzVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Black;
            this.label23.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(0, 60);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 20);
            this.label23.TabIndex = 267;
            this.label23.Text = "rz";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.Window;
            this.panel11.Controls.Add(this.lbl_ryVal);
            this.panel11.Controls.Add(this.label21);
            this.panel11.Location = new System.Drawing.Point(136, 150);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(80, 80);
            this.panel11.TabIndex = 328;
            // 
            // lbl_ryVal
            // 
            this.lbl_ryVal.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_ryVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ryVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_ryVal.ForeColor = System.Drawing.Color.Black;
            this.lbl_ryVal.Location = new System.Drawing.Point(0, 0);
            this.lbl_ryVal.Name = "lbl_ryVal";
            this.lbl_ryVal.Size = new System.Drawing.Size(80, 60);
            this.lbl_ryVal.TabIndex = 268;
            this.lbl_ryVal.Text = "0.00";
            this.lbl_ryVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Black;
            this.label21.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(0, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 20);
            this.label21.TabIndex = 267;
            this.label21.Text = "ry";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.Window;
            this.panel10.Controls.Add(this.lbl_rxVal);
            this.panel10.Controls.Add(this.label19);
            this.panel10.Location = new System.Drawing.Point(26, 150);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(80, 80);
            this.panel10.TabIndex = 327;
            // 
            // lbl_rxVal
            // 
            this.lbl_rxVal.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_rxVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_rxVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_rxVal.ForeColor = System.Drawing.Color.Black;
            this.lbl_rxVal.Location = new System.Drawing.Point(0, 0);
            this.lbl_rxVal.Name = "lbl_rxVal";
            this.lbl_rxVal.Size = new System.Drawing.Size(80, 60);
            this.lbl_rxVal.TabIndex = 268;
            this.lbl_rxVal.Text = "0.00";
            this.lbl_rxVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Black;
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(0, 60);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 20);
            this.label19.TabIndex = 267;
            this.label19.Text = "rx";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.Window;
            this.panel9.Controls.Add(this.lbl_zVal);
            this.panel9.Controls.Add(this.label17);
            this.panel9.Location = new System.Drawing.Point(246, 53);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(80, 80);
            this.panel9.TabIndex = 326;
            // 
            // lbl_zVal
            // 
            this.lbl_zVal.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_zVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_zVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_zVal.ForeColor = System.Drawing.Color.Black;
            this.lbl_zVal.Location = new System.Drawing.Point(0, 0);
            this.lbl_zVal.Name = "lbl_zVal";
            this.lbl_zVal.Size = new System.Drawing.Size(80, 60);
            this.lbl_zVal.TabIndex = 268;
            this.lbl_zVal.Text = "0.00";
            this.lbl_zVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Black;
            this.label17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(0, 60);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 20);
            this.label17.TabIndex = 267;
            this.label17.Text = "z";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.Window;
            this.panel8.Controls.Add(this.lbl_yVal);
            this.panel8.Controls.Add(this.label15);
            this.panel8.Location = new System.Drawing.Point(136, 53);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(80, 80);
            this.panel8.TabIndex = 326;
            // 
            // lbl_yVal
            // 
            this.lbl_yVal.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_yVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_yVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_yVal.ForeColor = System.Drawing.Color.Black;
            this.lbl_yVal.Location = new System.Drawing.Point(0, 0);
            this.lbl_yVal.Name = "lbl_yVal";
            this.lbl_yVal.Size = new System.Drawing.Size(80, 60);
            this.lbl_yVal.TabIndex = 268;
            this.lbl_yVal.Text = "0.00";
            this.lbl_yVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Black;
            this.label15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(0, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 20);
            this.label15.TabIndex = 267;
            this.label15.Text = "y";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Window;
            this.panel7.Controls.Add(this.lbl_xVal);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Location = new System.Drawing.Point(27, 53);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(80, 80);
            this.panel7.TabIndex = 325;
            // 
            // lbl_xVal
            // 
            this.lbl_xVal.BackColor = System.Drawing.SystemColors.Window;
            this.lbl_xVal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_xVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_xVal.ForeColor = System.Drawing.Color.Black;
            this.lbl_xVal.Location = new System.Drawing.Point(0, 0);
            this.lbl_xVal.Name = "lbl_xVal";
            this.lbl_xVal.Size = new System.Drawing.Size(80, 60);
            this.lbl_xVal.TabIndex = 268;
            this.lbl_xVal.Text = "0.00";
            this.lbl_xVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(0, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 20);
            this.label12.TabIndex = 267;
            this.label12.Text = "x";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::RM.Properties.Resources.parametersWhite32;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.TabIndex = 324;
            this.pictureBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(354, 40);
            this.label11.TabIndex = 266;
            this.label11.Text = "Coordinate tracker";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnl_liveStatus);
            this.panel2.Controls.Add(this.btn_stopLive);
            this.panel2.Controls.Add(this.btn_startLive);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(17, 263);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(355, 133);
            this.panel2.TabIndex = 326;
            // 
            // btn_stopLive
            // 
            this.btn_stopLive.BackColor = System.Drawing.SystemColors.Control;
            this.btn_stopLive.BackgroundImage = global::RM.Properties.Resources.stop;
            this.btn_stopLive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_stopLive.Location = new System.Drawing.Point(191, 49);
            this.btn_stopLive.Name = "btn_stopLive";
            this.btn_stopLive.Size = new System.Drawing.Size(55, 55);
            this.btn_stopLive.TabIndex = 328;
            this.btn_stopLive.UseVisualStyleBackColor = false;
            this.btn_stopLive.Click += new System.EventHandler(this.ClickEvent_stopLiveMovement);
            // 
            // btn_startLive
            // 
            this.btn_startLive.BackColor = System.Drawing.SystemColors.Control;
            this.btn_startLive.BackgroundImage = global::RM.Properties.Resources.play32;
            this.btn_startLive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_startLive.Location = new System.Drawing.Point(91, 49);
            this.btn_startLive.Name = "btn_startLive";
            this.btn_startLive.Size = new System.Drawing.Size(55, 55);
            this.btn_startLive.TabIndex = 327;
            this.btn_startLive.UseVisualStyleBackColor = false;
            this.btn_startLive.Click += new System.EventHandler(this.ClickEvent_startLiveMovement);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(182, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 326;
            this.label5.Text = "Stop";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(87, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 325;
            this.label6.Text = "Start";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Image = global::RM.Properties.Resources.parametersWhite32;
            this.pictureBox2.Location = new System.Drawing.Point(2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox2.TabIndex = 324;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 40);
            this.label1.TabIndex = 266;
            this.label1.Text = "Movimento in live";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.pnl_recStatus);
            this.panel3.Controls.Add(this.btn_stopRec);
            this.panel3.Controls.Add(this.btn_startRec);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(17, 413);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(354, 132);
            this.panel3.TabIndex = 327;
            // 
            // btn_stopRec
            // 
            this.btn_stopRec.BackColor = System.Drawing.SystemColors.Control;
            this.btn_stopRec.BackgroundImage = global::RM.Properties.Resources.stop;
            this.btn_stopRec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_stopRec.Location = new System.Drawing.Point(191, 48);
            this.btn_stopRec.Name = "btn_stopRec";
            this.btn_stopRec.Size = new System.Drawing.Size(55, 55);
            this.btn_stopRec.TabIndex = 332;
            this.btn_stopRec.UseVisualStyleBackColor = false;
            this.btn_stopRec.Click += new System.EventHandler(this.ClickEvent_stopRecord);
            // 
            // btn_startRec
            // 
            this.btn_startRec.BackColor = System.Drawing.SystemColors.Control;
            this.btn_startRec.BackgroundImage = global::RM.Properties.Resources.play32;
            this.btn_startRec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_startRec.Location = new System.Drawing.Point(91, 48);
            this.btn_startRec.Name = "btn_startRec";
            this.btn_startRec.Size = new System.Drawing.Size(55, 55);
            this.btn_startRec.TabIndex = 331;
            this.btn_startRec.UseVisualStyleBackColor = false;
            this.btn_startRec.Click += new System.EventHandler(this.ClickEvent_startRecord);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(182, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 15);
            this.label7.TabIndex = 330;
            this.label7.Text = "Stop";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(87, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 15);
            this.label8.TabIndex = 329;
            this.label8.Text = "Start";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Image = global::RM.Properties.Resources.parametersWhite32;
            this.pictureBox3.Location = new System.Drawing.Point(2, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 35);
            this.pictureBox3.TabIndex = 324;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 40);
            this.label2.TabIndex = 266;
            this.label2.Text = "Registrazione punti";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gainsboro;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lw_positions);
            this.panel4.Controls.Add(this.pictureBox4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(398, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(617, 393);
            this.panel4.TabIndex = 328;
            // 
            // lw_positions
            // 
            this.lw_positions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lw_positions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lw_positions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lw_positions.FullRowSelect = true;
            this.lw_positions.HideSelection = false;
            this.lw_positions.LabelWrap = false;
            this.lw_positions.Location = new System.Drawing.Point(0, 40);
            this.lw_positions.MultiSelect = false;
            this.lw_positions.Name = "lw_positions";
            this.lw_positions.Size = new System.Drawing.Size(615, 352);
            this.lw_positions.TabIndex = 325;
            this.lw_positions.UseCompatibleStateImageBehavior = false;
            this.lw_positions.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Timestamp";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "x";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 75;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "y";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 75;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "z";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 75;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "rx";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 75;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "ry";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 75;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "rz";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader9.Width = 75;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Black;
            this.pictureBox4.Image = global::RM.Properties.Resources.parametersWhite32;
            this.pictureBox4.Location = new System.Drawing.Point(2, 2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 35);
            this.pictureBox4.TabIndex = 324;
            this.pictureBox4.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(615, 40);
            this.label3.TabIndex = 266;
            this.label3.Text = "Lista punti";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Gainsboro;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.pnl_movementStatus);
            this.panel6.Controls.Add(this.btn_stopMov);
            this.panel6.Controls.Add(this.btn_startMov);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Controls.Add(this.pictureBox5);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Location = new System.Drawing.Point(398, 417);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(612, 128);
            this.panel6.TabIndex = 329;
            // 
            // btn_stopMov
            // 
            this.btn_stopMov.BackColor = System.Drawing.SystemColors.Control;
            this.btn_stopMov.BackgroundImage = global::RM.Properties.Resources.stop;
            this.btn_stopMov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_stopMov.Location = new System.Drawing.Point(327, 44);
            this.btn_stopMov.Name = "btn_stopMov";
            this.btn_stopMov.Size = new System.Drawing.Size(55, 55);
            this.btn_stopMov.TabIndex = 336;
            this.btn_stopMov.UseVisualStyleBackColor = false;
            this.btn_stopMov.Click += new System.EventHandler(this.ClickEventStopMovement);
            // 
            // btn_startMov
            // 
            this.btn_startMov.BackColor = System.Drawing.SystemColors.Control;
            this.btn_startMov.BackgroundImage = global::RM.Properties.Resources.play32;
            this.btn_startMov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_startMov.Location = new System.Drawing.Point(227, 44);
            this.btn_startMov.Name = "btn_startMov";
            this.btn_startMov.Size = new System.Drawing.Size(55, 55);
            this.btn_startMov.TabIndex = 335;
            this.btn_startMov.UseVisualStyleBackColor = false;
            this.btn_startMov.Click += new System.EventHandler(this.ClickEvent_startMovement);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(318, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 15);
            this.label9.TabIndex = 334;
            this.label9.Text = "Stop";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.Location = new System.Drawing.Point(223, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 15);
            this.label10.TabIndex = 333;
            this.label10.Text = "Start";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Black;
            this.pictureBox5.Image = global::RM.Properties.Resources.parametersWhite32;
            this.pictureBox5.Location = new System.Drawing.Point(2, 2);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(35, 35);
            this.pictureBox5.TabIndex = 324;
            this.pictureBox5.TabStop = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(610, 40);
            this.label4.TabIndex = 266;
            this.label4.Text = "Riproduzione punti robot";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_recStatus
            // 
            this.pnl_recStatus.BackColor = System.Drawing.Color.Firebrick;
            this.pnl_recStatus.Location = new System.Drawing.Point(327, 13);
            this.pnl_recStatus.Name = "pnl_recStatus";
            this.pnl_recStatus.Size = new System.Drawing.Size(15, 15);
            this.pnl_recStatus.TabIndex = 326;
            // 
            // pnl_liveStatus
            // 
            this.pnl_liveStatus.BackColor = System.Drawing.Color.Firebrick;
            this.pnl_liveStatus.Location = new System.Drawing.Point(327, 12);
            this.pnl_liveStatus.Name = "pnl_liveStatus";
            this.pnl_liveStatus.Size = new System.Drawing.Size(15, 15);
            this.pnl_liveStatus.TabIndex = 329;
            // 
            // pnl_movementStatus
            // 
            this.pnl_movementStatus.BackColor = System.Drawing.Color.Firebrick;
            this.pnl_movementStatus.Location = new System.Drawing.Point(585, 9);
            this.pnl_movementStatus.Name = "pnl_movementStatus";
            this.pnl_movementStatus.Size = new System.Drawing.Size(15, 15);
            this.pnl_movementStatus.TabIndex = 337;
            // 
            // UC_ViveTrackerPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Name = "UC_ViveTrackerPage";
            this.Size = new System.Drawing.Size(1024, 658);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_home;
        private System.Windows.Forms.Label lbl_buttonHome;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_stopLive;
        private System.Windows.Forms.Button btn_startLive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_stopRec;
        private System.Windows.Forms.Button btn_startRec;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_stopMov;
        private System.Windows.Forms.Button btn_startMov;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private RMLib.View.ScrollableListView lw_positions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label lbl_rzVal;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label lbl_ryVal;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lbl_rxVal;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lbl_zVal;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lbl_yVal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbl_xVal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnl_liveStatus;
        private System.Windows.Forms.Panel pnl_recStatus;
        private System.Windows.Forms.Panel pnl_movementStatus;
    }
}
