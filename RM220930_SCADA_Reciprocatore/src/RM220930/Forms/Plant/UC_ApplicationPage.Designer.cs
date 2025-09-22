using RM.Properties;

namespace RM.src.RM220930
{
    partial class UC_ApplicationPage
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
            this.lbl_home = new System.Windows.Forms.Label();
            this.Ud_max_id_filter = new System.Windows.Forms.NumericUpDown();
            this.Ud_min_id_filter = new System.Windows.Forms.NumericUpDown();
            this.Tb_name_filter = new System.Windows.Forms.TextBox();
            this.Tb_note_filter = new System.Windows.Forms.TextBox();
            this.lw_Applications = new RMLib.View.ScrollableListView();
            this.lb_Note = new System.Windows.Forms.Label();
            this.lb_Name = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Bttn_AvailableChoiches_EnableFilter = new System.Windows.Forms.Button();
            this.Btn_min_id_filter = new System.Windows.Forms.Button();
            this.Btn_max_id_filter = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_deleteApplication = new System.Windows.Forms.Button();
            this.btn_addApplication = new System.Windows.Forms.Button();
            this.btn_Home = new System.Windows.Forms.Button();
            this.btn_editID = new System.Windows.Forms.Button();
            this.btn_rename = new System.Windows.Forms.Button();
            this.btn_editNote = new System.Windows.Forms.Button();
            this.btn_loadProgram = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ud_max_id_filter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ud_min_id_filter)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_home
            // 
            this.lbl_home.BackColor = System.Drawing.Color.Black;
            this.lbl_home.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbl_home.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_home.Location = new System.Drawing.Point(20, 79);
            this.lbl_home.Name = "lbl_home";
            this.lbl_home.Size = new System.Drawing.Size(67, 15);
            this.lbl_home.TabIndex = 1;
            this.lbl_home.Text = "Home";
            this.lbl_home.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ud_max_id_filter
            // 
            this.Ud_max_id_filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ud_max_id_filter.Location = new System.Drawing.Point(226, 5);
            this.Ud_max_id_filter.Name = "Ud_max_id_filter";
            this.Ud_max_id_filter.Size = new System.Drawing.Size(61, 35);
            this.Ud_max_id_filter.TabIndex = 73;
            this.Ud_max_id_filter.Visible = false;
            this.Ud_max_id_filter.ValueChanged += new System.EventHandler(this.Ud_max_id_filter_ValueChanged);
            // 
            // Ud_min_id_filter
            // 
            this.Ud_min_id_filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ud_min_id_filter.Location = new System.Drawing.Point(106, 5);
            this.Ud_min_id_filter.Name = "Ud_min_id_filter";
            this.Ud_min_id_filter.Size = new System.Drawing.Size(61, 35);
            this.Ud_min_id_filter.TabIndex = 72;
            this.Ud_min_id_filter.Visible = false;
            this.Ud_min_id_filter.ValueChanged += new System.EventHandler(this.Ud_min_id_filter_ValueChanged);
            // 
            // Tb_name_filter
            // 
            this.Tb_name_filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.Tb_name_filter.Location = new System.Drawing.Point(582, 4);
            this.Tb_name_filter.Name = "Tb_name_filter";
            this.Tb_name_filter.Size = new System.Drawing.Size(140, 35);
            this.Tb_name_filter.TabIndex = 71;
            this.Tb_name_filter.Visible = false;
            this.Tb_name_filter.WordWrap = false;
            this.Tb_name_filter.Click += new System.EventHandler(this.Tb_name_filter_Click);
            this.Tb_name_filter.TextChanged += new System.EventHandler(this.Tb_name_filter_TextChanged);
            // 
            // Tb_note_filter
            // 
            this.Tb_note_filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.Tb_note_filter.Location = new System.Drawing.Point(822, 4);
            this.Tb_note_filter.Name = "Tb_note_filter";
            this.Tb_note_filter.Size = new System.Drawing.Size(140, 35);
            this.Tb_note_filter.TabIndex = 70;
            this.Tb_note_filter.Visible = false;
            this.Tb_note_filter.WordWrap = false;
            this.Tb_note_filter.Click += new System.EventHandler(this.Tb_note_filter_Click);
            this.Tb_note_filter.TextChanged += new System.EventHandler(this.Tb_note_filter_TextChanged);
            // 
            // lw_Applications
            // 
            this.lw_Applications.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lw_Applications.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lw_Applications.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lw_Applications.FullRowSelect = true;
            this.lw_Applications.HideSelection = false;
            this.lw_Applications.LabelWrap = false;
            this.lw_Applications.Location = new System.Drawing.Point(2, 40);
            this.lw_Applications.MultiSelect = false;
            this.lw_Applications.Name = "lw_Applications";
            this.lw_Applications.Size = new System.Drawing.Size(988, 453);
            this.lw_Applications.TabIndex = 55;
            this.lw_Applications.UseCompatibleStateImageBehavior = false;
            this.lw_Applications.View = System.Windows.Forms.View.Details;
            this.lw_Applications.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lw_Applications_DrawColumnHeader);
            this.lw_Applications.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lw_Applications_DrawItem);
            this.lw_Applications.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lw_Applications_DrawSubItem);
            // 
            // lb_Note
            // 
            this.lb_Note.AutoSize = true;
            this.lb_Note.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.lb_Note.Location = new System.Drawing.Point(759, 11);
            this.lb_Note.Name = "lb_Note";
            this.lb_Note.Size = new System.Drawing.Size(60, 24);
            this.lb_Note.TabIndex = 76;
            this.lb_Note.Text = "Note:";
            this.lb_Note.Visible = false;
            // 
            // lb_Name
            // 
            this.lb_Name.AutoSize = true;
            this.lb_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.lb_Name.Location = new System.Drawing.Point(509, 11);
            this.lb_Name.Name = "lb_Name";
            this.lb_Name.Size = new System.Drawing.Size(71, 24);
            this.lb_Name.TabIndex = 77;
            this.lb_Name.Text = "Name:";
            this.lb_Name.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(619, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Carica";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(520, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Note";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(421, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Location = new System.Drawing.Point(318, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 1;
            this.label9.Text = "ID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Location = new System.Drawing.Point(117, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Aggiungi";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Black;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Location = new System.Drawing.Point(218, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "Cancella";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(993, 39);
            this.label6.TabIndex = 266;
            this.label6.Text = "Applicazioni";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Bttn_AvailableChoiches_EnableFilter
            // 
            this.Bttn_AvailableChoiches_EnableFilter.AccessibleDescription = "";
            this.Bttn_AvailableChoiches_EnableFilter.BackgroundImage = global::RM.Properties.Resources.filter_filled;
            this.Bttn_AvailableChoiches_EnableFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Bttn_AvailableChoiches_EnableFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Bttn_AvailableChoiches_EnableFilter.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Bttn_AvailableChoiches_EnableFilter.Location = new System.Drawing.Point(60, 0);
            this.Bttn_AvailableChoiches_EnableFilter.Name = "Bttn_AvailableChoiches_EnableFilter";
            this.Bttn_AvailableChoiches_EnableFilter.Size = new System.Drawing.Size(40, 40);
            this.Bttn_AvailableChoiches_EnableFilter.TabIndex = 69;
            this.Bttn_AvailableChoiches_EnableFilter.UseVisualStyleBackColor = true;
            this.Bttn_AvailableChoiches_EnableFilter.Visible = false;
            this.Bttn_AvailableChoiches_EnableFilter.Click += new System.EventHandler(this.Bttn_AvailableChoiches_EnableFilter_Click);
            // 
            // Btn_min_id_filter
            // 
            this.Btn_min_id_filter.BackgroundImage = global::RM.Properties.Resources.keypad_filled_black;
            this.Btn_min_id_filter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_min_id_filter.Location = new System.Drawing.Point(173, 5);
            this.Btn_min_id_filter.Name = "Btn_min_id_filter";
            this.Btn_min_id_filter.Size = new System.Drawing.Size(35, 35);
            this.Btn_min_id_filter.TabIndex = 74;
            this.Btn_min_id_filter.UseVisualStyleBackColor = true;
            this.Btn_min_id_filter.Visible = false;
            this.Btn_min_id_filter.Click += new System.EventHandler(this.Btn_min_id_filter_Click);
            // 
            // Btn_max_id_filter
            // 
            this.Btn_max_id_filter.BackgroundImage = global::RM.Properties.Resources.keypad_filled_black;
            this.Btn_max_id_filter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_max_id_filter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_max_id_filter.Location = new System.Drawing.Point(293, 5);
            this.Btn_max_id_filter.Name = "Btn_max_id_filter";
            this.Btn_max_id_filter.Size = new System.Drawing.Size(35, 35);
            this.Btn_max_id_filter.TabIndex = 75;
            this.Btn_max_id_filter.UseVisualStyleBackColor = true;
            this.Btn_max_id_filter.Visible = false;
            this.Btn_max_id_filter.Click += new System.EventHandler(this.Btn_max_id_filter_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Black;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.btn_loadProgram);
            this.panel5.Controls.Add(this.btn_editNote);
            this.panel5.Controls.Add(this.btn_rename);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.btn_editID);
            this.panel5.Controls.Add(this.btn_Home);
            this.panel5.Controls.Add(this.lbl_home);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.btn_addApplication);
            this.panel5.Controls.Add(this.btn_deleteApplication);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label9);
            this.panel5.Location = new System.Drawing.Point(0, 556);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1025, 102);
            this.panel5.TabIndex = 323;
            // 
            // btn_deleteApplication
            // 
            this.btn_deleteApplication.BackgroundImage = global::RM.Properties.Resources.delete32;
            this.btn_deleteApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_deleteApplication.Location = new System.Drawing.Point(224, 18);
            this.btn_deleteApplication.Name = "btn_deleteApplication";
            this.btn_deleteApplication.Size = new System.Drawing.Size(55, 55);
            this.btn_deleteApplication.TabIndex = 287;
            this.btn_deleteApplication.UseVisualStyleBackColor = true;
            this.btn_deleteApplication.Click += new System.EventHandler(this.ClickEvent_deleteApplication);
            // 
            // btn_addApplication
            // 
            this.btn_addApplication.BackgroundImage = global::RM.Properties.Resources.addApp32;
            this.btn_addApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_addApplication.Location = new System.Drawing.Point(124, 18);
            this.btn_addApplication.Name = "btn_addApplication";
            this.btn_addApplication.Size = new System.Drawing.Size(55, 55);
            this.btn_addApplication.TabIndex = 288;
            this.btn_addApplication.UseVisualStyleBackColor = true;
            this.btn_addApplication.Click += new System.EventHandler(this.ClickEvent_addApplication);
            // 
            // btn_Home
            // 
            this.btn_Home.BackgroundImage = global::RM.Properties.Resources.back32;
            this.btn_Home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Home.Location = new System.Drawing.Point(28, 18);
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.Size = new System.Drawing.Size(55, 55);
            this.btn_Home.TabIndex = 289;
            this.btn_Home.UseVisualStyleBackColor = true;
            this.btn_Home.Click += new System.EventHandler(this.btn_Home_Click);
            // 
            // btn_editID
            // 
            this.btn_editID.BackgroundImage = global::RM.Properties.Resources.edit32;
            this.btn_editID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_editID.Location = new System.Drawing.Point(324, 18);
            this.btn_editID.Name = "btn_editID";
            this.btn_editID.Size = new System.Drawing.Size(55, 55);
            this.btn_editID.TabIndex = 290;
            this.btn_editID.UseVisualStyleBackColor = true;
            this.btn_editID.Click += new System.EventHandler(this.ClickEvent_EditID);
            // 
            // btn_rename
            // 
            this.btn_rename.BackgroundImage = global::RM.Properties.Resources.edit32;
            this.btn_rename.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_rename.Location = new System.Drawing.Point(424, 18);
            this.btn_rename.Name = "btn_rename";
            this.btn_rename.Size = new System.Drawing.Size(55, 55);
            this.btn_rename.TabIndex = 291;
            this.btn_rename.UseVisualStyleBackColor = true;
            this.btn_rename.Click += new System.EventHandler(this.ClickEvent_rename);
            // 
            // btn_editNote
            // 
            this.btn_editNote.BackgroundImage = global::RM.Properties.Resources.edit32;
            this.btn_editNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_editNote.Location = new System.Drawing.Point(524, 18);
            this.btn_editNote.Name = "btn_editNote";
            this.btn_editNote.Size = new System.Drawing.Size(55, 55);
            this.btn_editNote.TabIndex = 292;
            this.btn_editNote.UseVisualStyleBackColor = true;
            this.btn_editNote.Click += new System.EventHandler(this.ClickEvent_EditNote);
            // 
            // btn_loadProgram
            // 
            this.btn_loadProgram.BackgroundImage = global::RM.Properties.Resources.load32;
            this.btn_loadProgram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_loadProgram.Location = new System.Drawing.Point(624, 18);
            this.btn_loadProgram.Name = "btn_loadProgram";
            this.btn_loadProgram.Size = new System.Drawing.Size(55, 55);
            this.btn_loadProgram.TabIndex = 293;
            this.btn_loadProgram.UseVisualStyleBackColor = true;
            this.btn_loadProgram.Click += new System.EventHandler(this.ClickEvent_loadProgram);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lw_Applications);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(17, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 497);
            this.panel1.TabIndex = 324;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::RM.Properties.Resources.appWhite32;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.TabIndex = 324;
            this.pictureBox1.TabStop = false;
            // 
            // UC_ApplicationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb_Name);
            this.Controls.Add(this.Ud_max_id_filter);
            this.Controls.Add(this.Bttn_AvailableChoiches_EnableFilter);
            this.Controls.Add(this.Ud_min_id_filter);
            this.Controls.Add(this.lb_Note);
            this.Controls.Add(this.Btn_min_id_filter);
            this.Controls.Add(this.Tb_note_filter);
            this.Controls.Add(this.Tb_name_filter);
            this.Controls.Add(this.Btn_max_id_filter);
            this.Controls.Add(this.panel5);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UC_ApplicationPage";
            this.Size = new System.Drawing.Size(1024, 658);
            ((System.ComponentModel.ISupportInitialize)(this.Ud_max_id_filter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ud_min_id_filter)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private RMLib.View.ScrollableListView lw_Applications;
        private System.Windows.Forms.Label lbl_home;
        private System.Windows.Forms.Button Btn_max_id_filter;
        private System.Windows.Forms.Button Btn_min_id_filter;
        private System.Windows.Forms.NumericUpDown Ud_max_id_filter;
        private System.Windows.Forms.NumericUpDown Ud_min_id_filter;
        private System.Windows.Forms.TextBox Tb_name_filter;
        private System.Windows.Forms.TextBox Tb_note_filter;
        private System.Windows.Forms.Button Bttn_AvailableChoiches_EnableFilter;
        private System.Windows.Forms.Label lb_Note;
        private System.Windows.Forms.Label lb_Name;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_loadProgram;
        private System.Windows.Forms.Button btn_editNote;
        private System.Windows.Forms.Button btn_rename;
        private System.Windows.Forms.Button btn_editID;
        private System.Windows.Forms.Button btn_Home;
        private System.Windows.Forms.Button btn_addApplication;
        private System.Windows.Forms.Button btn_deleteApplication;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
