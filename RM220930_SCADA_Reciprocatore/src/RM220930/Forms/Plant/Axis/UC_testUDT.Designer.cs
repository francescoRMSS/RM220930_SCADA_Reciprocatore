namespace RM.src.RM220930.Forms.Plant.Axis
{
    partial class UC_testUDT
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
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label10;
            this.btn_boolValue = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_boolList = new System.Windows.Forms.ComboBox();
            this.lbl_selectedBool = new System.Windows.Forms.Label();
            this.cb_axis = new System.Windows.Forms.ComboBox();
            this.lbl_selectedAxe = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_boolValue
            // 
            this.btn_boolValue.BackColor = System.Drawing.Color.Red;
            this.btn_boolValue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_boolValue.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btn_boolValue.ForeColor = System.Drawing.Color.Black;
            this.btn_boolValue.Location = new System.Drawing.Point(87, 289);
            this.btn_boolValue.Name = "btn_boolValue";
            this.btn_boolValue.Size = new System.Drawing.Size(80, 50);
            this.btn_boolValue.TabIndex = 353;
            this.btn_boolValue.Text = "FALSE";
            this.btn_boolValue.UseVisualStyleBackColor = false;
            this.btn_boolValue.Click += new System.EventHandler(this.ClickEvent_modifyCmd_On_axe);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbl_selectedBool);
            this.panel2.Controls.Add(label8);
            this.panel2.Controls.Add(label2);
            this.panel2.Controls.Add(label4);
            this.panel2.Controls.Add(this.cb_boolList);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_boolValue);
            this.panel2.Location = new System.Drawing.Point(42, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 378);
            this.panel2.TabIndex = 361;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Gray;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(-1, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(260, 39);
            this.label5.TabIndex = 361;
            this.label5.Text = "BOOL";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 362;
            // 
            // lbl_title
            // 
            this.lbl_title.BackColor = System.Drawing.Color.DimGray;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(1024, 39);
            this.lbl_title.TabIndex = 362;
            this.lbl_title.Text = "TEST VARIABILI";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(377, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 378);
            this.panel1.TabIndex = 364;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gray;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(-1, -1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 39);
            this.label3.TabIndex = 361;
            this.label3.Text = "INT16";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(719, 127);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(260, 378);
            this.panel3.TabIndex = 365;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gray;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(-1, -1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(260, 39);
            this.label7.TabIndex = 361;
            this.label7.Text = "FLOAT";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_boolList
            // 
            this.cb_boolList.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.cb_boolList.FormattingEnabled = true;
            this.cb_boolList.Items.AddRange(new object[] {
            "Cmd_On_Axe",
            "Cmd_En_Axe",
            "Cmd_Go_home",
            "Cmd_AutoFrom_Pc",
            "Cmd_Auto",
            "Cmd_Rif_Chain",
            "Cmd_move",
            "Cmd_Start_Pos",
            "Cmd_Jog_Pos",
            "Cmd_Jog_neg",
            "Cmd_Abs_Mode",
            "Cmd_Enable_Fifo",
            "Cmd_Enable_Grouppo",
            "Cmd_Stop_Axe",
            "Cmd_Start_Cam_Table",
            "Cmd_Start_Cam",
            "Cmd_Stop_Cam",
            "Read_No_Piece",
            "Read_Error",
            "Read_Axe_Power_On",
            "Read_Ls_Pos",
            "Read_Ls_Neg",
            "Read_Bit_Min",
            "Read_Home_Ok",
            "Read_In_Pos",
            "Read_Positioning_in_Prog",
            "Read_Jog_in_Prog",
            "Read_Timeout_Home",
            "Read_Home_In_Prog"});
            this.cb_boolList.Location = new System.Drawing.Point(66, 101);
            this.cb_boolList.Name = "cb_boolList";
            this.cb_boolList.Size = new System.Drawing.Size(121, 24);
            this.cb_boolList.TabIndex = 363;
            this.cb_boolList.SelectedIndexChanged += new System.EventHandler(this.cb_boolList_SelectedIndexChanged);
            // 
            // label4
            // 
            label4.BackColor = System.Drawing.Color.Gray;
            label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            label4.ForeColor = System.Drawing.SystemColors.Control;
            label4.Location = new System.Drawing.Point(-4, 59);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(263, 39);
            label4.TabIndex = 364;
            label4.Text = "Selezione variabile:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = System.Drawing.Color.Gray;
            label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            label2.ForeColor = System.Drawing.SystemColors.Control;
            label2.Location = new System.Drawing.Point(-4, 165);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(263, 39);
            label2.TabIndex = 365;
            label2.Text = "Variabile selezionata:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.BackColor = System.Drawing.Color.Gray;
            label8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            label8.ForeColor = System.Drawing.SystemColors.Control;
            label8.Location = new System.Drawing.Point(-2, 246);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(263, 39);
            label8.TabIndex = 367;
            label8.Text = "Valore variabile:";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_selectedBool
            // 
            this.lbl_selectedBool.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_selectedBool.ForeColor = System.Drawing.Color.White;
            this.lbl_selectedBool.Location = new System.Drawing.Point(-1, 204);
            this.lbl_selectedBool.Name = "lbl_selectedBool";
            this.lbl_selectedBool.Size = new System.Drawing.Size(260, 23);
            this.lbl_selectedBool.TabIndex = 368;
            this.lbl_selectedBool.Text = "-";
            this.lbl_selectedBool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.BackColor = System.Drawing.Color.DimGray;
            label6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            label6.ForeColor = System.Drawing.SystemColors.Control;
            label6.Location = new System.Drawing.Point(362, 34);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(121, 39);
            label6.TabIndex = 370;
            label6.Text = "Selezione asse:";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_axis
            // 
            this.cb_axis.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.cb_axis.FormattingEnabled = true;
            this.cb_axis.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cb_axis.Location = new System.Drawing.Point(362, 76);
            this.cb_axis.Name = "cb_axis";
            this.cb_axis.Size = new System.Drawing.Size(121, 24);
            this.cb_axis.TabIndex = 369;
            this.cb_axis.SelectedIndexChanged += new System.EventHandler(this.cb_axis_SelectedIndexChanged);
            // 
            // lbl_selectedAxe
            // 
            this.lbl_selectedAxe.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_selectedAxe.ForeColor = System.Drawing.Color.White;
            this.lbl_selectedAxe.Location = new System.Drawing.Point(515, 77);
            this.lbl_selectedAxe.Name = "lbl_selectedAxe";
            this.lbl_selectedAxe.Size = new System.Drawing.Size(148, 23);
            this.lbl_selectedAxe.TabIndex = 371;
            this.lbl_selectedAxe.Text = "-";
            this.lbl_selectedAxe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            label10.BackColor = System.Drawing.Color.DimGray;
            label10.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            label10.ForeColor = System.Drawing.SystemColors.Control;
            label10.Location = new System.Drawing.Point(515, 34);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(148, 39);
            label10.TabIndex = 370;
            label10.Text = "Asse selezionato:";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(768, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 369;
            // 
            // UC_testUDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.lbl_selectedAxe);
            this.Controls.Add(label6);
            this.Controls.Add(label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cb_axis);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.panel2);
            this.Name = "UC_testUDT";
            this.Size = new System.Drawing.Size(1024, 557);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_boolValue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_boolList;
        private System.Windows.Forms.Label lbl_selectedBool;
        private System.Windows.Forms.ComboBox cb_axis;
        private System.Windows.Forms.Label lbl_selectedAxe;
        private System.Windows.Forms.Label label11;
    }
}
