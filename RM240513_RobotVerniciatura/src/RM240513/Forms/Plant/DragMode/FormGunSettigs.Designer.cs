namespace RM.src.RM240513.Forms.Plant.DragMode
{
    partial class FormGunSettigs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_header = new System.Windows.Forms.Label();
            this.lbl_selectedPoints = new System.Windows.Forms.Label();
            this.lbl_selectedFromText = new System.Windows.Forms.Label();
            this.lbl_selectedToText = new System.Windows.Forms.Label();
            this.pnl_modifySettings = new System.Windows.Forms.Panel();
            this.btn_enableStatus = new RMLib.View.CustomButton();
            this.pb_currentIcon = new System.Windows.Forms.PictureBox();
            this.lbl_currentText = new System.Windows.Forms.Label();
            this.pb_pressureIcon = new System.Windows.Forms.PictureBox();
            this.lbl_pressureText = new System.Windows.Forms.Label();
            this.btn_disableStatus = new RMLib.View.CustomButton();
            this.lbl_valStatusModText = new System.Windows.Forms.Label();
            this.lbl_valVModText = new System.Windows.Forms.Label();
            this.lbl_valAModText = new System.Windows.Forms.Label();
            this.lbl_valValve3ModText = new System.Windows.Forms.Label();
            this.lbl_valValve2ModText = new System.Windows.Forms.Label();
            this.lbl_valValve1ModText = new System.Windows.Forms.Label();
            this.pb_separator = new System.Windows.Forms.PictureBox();
            this.pb_icon = new System.Windows.Forms.PictureBox();
            this.lbl_selectedFrom = new System.Windows.Forms.Label();
            this.lbl_selectedTo = new System.Windows.Forms.Label();
            this.lbl_valValve1Mod = new System.Windows.Forms.Label();
            this.lbl_valValve2Mod = new System.Windows.Forms.Label();
            this.lbl_valValve3Mod = new System.Windows.Forms.Label();
            this.lbl_valAMod = new System.Windows.Forms.Label();
            this.lbl_valVMod = new System.Windows.Forms.Label();
            this.pnl_modifySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_currentIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_pressureIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_separator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_header
            // 
            this.lbl_header.BackColor = System.Drawing.Color.Transparent;
            this.lbl_header.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_header.ForeColor = System.Drawing.Color.White;
            this.lbl_header.Location = new System.Drawing.Point(1, 2);
            this.lbl_header.Name = "lbl_header";
            this.lbl_header.Size = new System.Drawing.Size(400, 32);
            this.lbl_header.TabIndex = 257;
            this.lbl_header.Text = "Proprietà della pistola";
            this.lbl_header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownEvent_startMove);
            this.lbl_header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveEvent_moving);
            this.lbl_header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUpEvent_stopMove);
            // 
            // lbl_selectedPoints
            // 
            this.lbl_selectedPoints.BackColor = System.Drawing.Color.Transparent;
            this.lbl_selectedPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_selectedPoints.ForeColor = System.Drawing.Color.White;
            this.lbl_selectedPoints.Location = new System.Drawing.Point(2, 9);
            this.lbl_selectedPoints.Name = "lbl_selectedPoints";
            this.lbl_selectedPoints.Size = new System.Drawing.Size(285, 19);
            this.lbl_selectedPoints.TabIndex = 271;
            this.lbl_selectedPoints.Text = "Punti selezionati :";
            this.lbl_selectedPoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_selectedFromText
            // 
            this.lbl_selectedFromText.AutoSize = true;
            this.lbl_selectedFromText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_selectedFromText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_selectedFromText.Location = new System.Drawing.Point(80, 42);
            this.lbl_selectedFromText.Name = "lbl_selectedFromText";
            this.lbl_selectedFromText.Size = new System.Drawing.Size(30, 20);
            this.lbl_selectedFromText.TabIndex = 273;
            this.lbl_selectedFromText.Text = "Da";
            // 
            // lbl_selectedToText
            // 
            this.lbl_selectedToText.AutoSize = true;
            this.lbl_selectedToText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_selectedToText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_selectedToText.Location = new System.Drawing.Point(217, 39);
            this.lbl_selectedToText.Name = "lbl_selectedToText";
            this.lbl_selectedToText.Size = new System.Drawing.Size(20, 20);
            this.lbl_selectedToText.TabIndex = 275;
            this.lbl_selectedToText.Text = "A";
            // 
            // pnl_modifySettings
            // 
            this.pnl_modifySettings.Controls.Add(this.lbl_valVMod);
            this.pnl_modifySettings.Controls.Add(this.lbl_valAMod);
            this.pnl_modifySettings.Controls.Add(this.lbl_valValve3Mod);
            this.pnl_modifySettings.Controls.Add(this.lbl_valValve2Mod);
            this.pnl_modifySettings.Controls.Add(this.lbl_valValve1Mod);
            this.pnl_modifySettings.Controls.Add(this.lbl_selectedTo);
            this.pnl_modifySettings.Controls.Add(this.lbl_selectedFrom);
            this.pnl_modifySettings.Controls.Add(this.btn_enableStatus);
            this.pnl_modifySettings.Controls.Add(this.pb_currentIcon);
            this.pnl_modifySettings.Controls.Add(this.lbl_currentText);
            this.pnl_modifySettings.Controls.Add(this.pb_pressureIcon);
            this.pnl_modifySettings.Controls.Add(this.lbl_pressureText);
            this.pnl_modifySettings.Controls.Add(this.btn_disableStatus);
            this.pnl_modifySettings.Controls.Add(this.lbl_valStatusModText);
            this.pnl_modifySettings.Controls.Add(this.lbl_valVModText);
            this.pnl_modifySettings.Controls.Add(this.lbl_valAModText);
            this.pnl_modifySettings.Controls.Add(this.lbl_valValve3ModText);
            this.pnl_modifySettings.Controls.Add(this.lbl_valValve2ModText);
            this.pnl_modifySettings.Controls.Add(this.lbl_valValve1ModText);
            this.pnl_modifySettings.Controls.Add(this.pb_separator);
            this.pnl_modifySettings.Controls.Add(this.lbl_selectedPoints);
            this.pnl_modifySettings.Controls.Add(this.lbl_selectedFromText);
            this.pnl_modifySettings.Controls.Add(this.lbl_selectedToText);
            this.pnl_modifySettings.Location = new System.Drawing.Point(4, 43);
            this.pnl_modifySettings.Name = "pnl_modifySettings";
            this.pnl_modifySettings.Size = new System.Drawing.Size(397, 369);
            this.pnl_modifySettings.TabIndex = 294;
            // 
            // btn_enableStatus
            // 
            this.btn_enableStatus.BackColor = System.Drawing.SystemColors.Control;
            this.btn_enableStatus.BackgroundColor = System.Drawing.SystemColors.Control;
            this.btn_enableStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_enableStatus.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_enableStatus.BorderRadius = 0;
            this.btn_enableStatus.BorderSize = 0;
            this.btn_enableStatus.FlatAppearance.BorderSize = 0;
            this.btn_enableStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_enableStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enableStatus.ForeColor = System.Drawing.Color.Black;
            this.btn_enableStatus.Location = new System.Drawing.Point(323, 321);
            this.btn_enableStatus.Name = "btn_enableStatus";
            this.btn_enableStatus.Size = new System.Drawing.Size(49, 29);
            this.btn_enableStatus.TabIndex = 304;
            this.btn_enableStatus.Text = "ON";
            this.btn_enableStatus.TextColor = System.Drawing.Color.Black;
            this.btn_enableStatus.UseVisualStyleBackColor = false;
            this.btn_enableStatus.Click += new System.EventHandler(this.ClickEvent_enableStatus);
            // 
            // pb_currentIcon
            // 
            this.pb_currentIcon.BackgroundImage = global::RM.Properties.Resources.current;
            this.pb_currentIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pb_currentIcon.Location = new System.Drawing.Point(214, 100);
            this.pb_currentIcon.Name = "pb_currentIcon";
            this.pb_currentIcon.Size = new System.Drawing.Size(30, 30);
            this.pb_currentIcon.TabIndex = 302;
            this.pb_currentIcon.TabStop = false;
            // 
            // lbl_currentText
            // 
            this.lbl_currentText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_currentText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_currentText.Location = new System.Drawing.Point(250, 101);
            this.lbl_currentText.Name = "lbl_currentText";
            this.lbl_currentText.Size = new System.Drawing.Size(139, 29);
            this.lbl_currentText.TabIndex = 301;
            this.lbl_currentText.Text = "Elettrostatica";
            this.lbl_currentText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pb_pressureIcon
            // 
            this.pb_pressureIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pb_pressureIcon.Image = global::RM.Properties.Resources.pressure;
            this.pb_pressureIcon.Location = new System.Drawing.Point(10, 101);
            this.pb_pressureIcon.Name = "pb_pressureIcon";
            this.pb_pressureIcon.Size = new System.Drawing.Size(30, 30);
            this.pb_pressureIcon.TabIndex = 300;
            this.pb_pressureIcon.TabStop = false;
            // 
            // lbl_pressureText
            // 
            this.lbl_pressureText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pressureText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_pressureText.Location = new System.Drawing.Point(54, 103);
            this.lbl_pressureText.Name = "lbl_pressureText";
            this.lbl_pressureText.Size = new System.Drawing.Size(139, 29);
            this.lbl_pressureText.TabIndex = 298;
            this.lbl_pressureText.Text = "Pressione";
            this.lbl_pressureText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_disableStatus
            // 
            this.btn_disableStatus.BackColor = System.Drawing.SystemColors.Control;
            this.btn_disableStatus.BackgroundColor = System.Drawing.SystemColors.Control;
            this.btn_disableStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_disableStatus.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_disableStatus.BorderRadius = 0;
            this.btn_disableStatus.BorderSize = 0;
            this.btn_disableStatus.FlatAppearance.BorderSize = 0;
            this.btn_disableStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_disableStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_disableStatus.ForeColor = System.Drawing.Color.Black;
            this.btn_disableStatus.Location = new System.Drawing.Point(258, 322);
            this.btn_disableStatus.Name = "btn_disableStatus";
            this.btn_disableStatus.Size = new System.Drawing.Size(49, 29);
            this.btn_disableStatus.TabIndex = 297;
            this.btn_disableStatus.Text = "OFF";
            this.btn_disableStatus.TextColor = System.Drawing.Color.Black;
            this.btn_disableStatus.UseVisualStyleBackColor = false;
            this.btn_disableStatus.Click += new System.EventHandler(this.ClickEvent_disableStatus);
            // 
            // lbl_valStatusModText
            // 
            this.lbl_valStatusModText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_valStatusModText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_valStatusModText.Location = new System.Drawing.Point(176, 323);
            this.lbl_valStatusModText.Name = "lbl_valStatusModText";
            this.lbl_valStatusModText.Size = new System.Drawing.Size(67, 29);
            this.lbl_valStatusModText.TabIndex = 296;
            this.lbl_valStatusModText.Text = "Status";
            this.lbl_valStatusModText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_valVModText
            // 
            this.lbl_valVModText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_valVModText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_valVModText.Location = new System.Drawing.Point(210, 207);
            this.lbl_valVModText.Name = "lbl_valVModText";
            this.lbl_valVModText.Size = new System.Drawing.Size(77, 29);
            this.lbl_valVModText.TabIndex = 294;
            this.lbl_valVModText.Text = "K Volt";
            this.lbl_valVModText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_valAModText
            // 
            this.lbl_valAModText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_valAModText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_valAModText.Location = new System.Drawing.Point(210, 151);
            this.lbl_valAModText.Name = "lbl_valAModText";
            this.lbl_valAModText.Size = new System.Drawing.Size(77, 29);
            this.lbl_valAModText.TabIndex = 292;
            this.lbl_valAModText.Text = "µ Ampere";
            this.lbl_valAModText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_valValve3ModText
            // 
            this.lbl_valValve3ModText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_valValve3ModText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_valValve3ModText.Location = new System.Drawing.Point(14, 265);
            this.lbl_valValve3ModText.Name = "lbl_valValve3ModText";
            this.lbl_valValve3ModText.Size = new System.Drawing.Size(90, 29);
            this.lbl_valValve3ModText.TabIndex = 289;
            this.lbl_valValve3ModText.Text = "Gun air";
            this.lbl_valValve3ModText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_valValve2ModText
            // 
            this.lbl_valValve2ModText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_valValve2ModText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_valValve2ModText.Location = new System.Drawing.Point(14, 209);
            this.lbl_valValve2ModText.Name = "lbl_valValve2ModText";
            this.lbl_valValve2ModText.Size = new System.Drawing.Size(90, 29);
            this.lbl_valValve2ModText.TabIndex = 287;
            this.lbl_valValve2ModText.Text = "Dosage air";
            this.lbl_valValve2ModText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_valValve1ModText
            // 
            this.lbl_valValve1ModText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_valValve1ModText.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_valValve1ModText.Location = new System.Drawing.Point(14, 153);
            this.lbl_valValve1ModText.Name = "lbl_valValve1ModText";
            this.lbl_valValve1ModText.Size = new System.Drawing.Size(90, 29);
            this.lbl_valValve1ModText.TabIndex = 285;
            this.lbl_valValve1ModText.Text = "Feed air";
            this.lbl_valValve1ModText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pb_separator
            // 
            this.pb_separator.BackColor = System.Drawing.Color.Transparent;
            this.pb_separator.BackgroundImage = global::RM.Properties.Resources.line;
            this.pb_separator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_separator.Location = new System.Drawing.Point(0, 71);
            this.pb_separator.Name = "pb_separator";
            this.pb_separator.Size = new System.Drawing.Size(395, 18);
            this.pb_separator.TabIndex = 272;
            this.pb_separator.TabStop = false;
            // 
            // pb_icon
            // 
            this.pb_icon.BackgroundImage = global::RM.Properties.Resources.airbrush;
            this.pb_icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pb_icon.Location = new System.Drawing.Point(5, 2);
            this.pb_icon.Name = "pb_icon";
            this.pb_icon.Size = new System.Drawing.Size(32, 32);
            this.pb_icon.TabIndex = 258;
            this.pb_icon.TabStop = false;
            this.pb_icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownEvent_startMove);
            this.pb_icon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveEvent_moving);
            this.pb_icon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseUpEvent_stopMove);
            // 
            // lbl_selectedFrom
            // 
            this.lbl_selectedFrom.BackColor = System.Drawing.Color.White;
            this.lbl_selectedFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbl_selectedFrom.Location = new System.Drawing.Point(127, 37);
            this.lbl_selectedFrom.Name = "lbl_selectedFrom";
            this.lbl_selectedFrom.Size = new System.Drawing.Size(49, 29);
            this.lbl_selectedFrom.TabIndex = 305;
            this.lbl_selectedFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_selectedFrom.Click += new System.EventHandler(this.ClickEvent_selectFrom);
            // 
            // lbl_selectedTo
            // 
            this.lbl_selectedTo.BackColor = System.Drawing.Color.White;
            this.lbl_selectedTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbl_selectedTo.Location = new System.Drawing.Point(250, 37);
            this.lbl_selectedTo.Name = "lbl_selectedTo";
            this.lbl_selectedTo.Size = new System.Drawing.Size(49, 29);
            this.lbl_selectedTo.TabIndex = 306;
            this.lbl_selectedTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_selectedTo.Click += new System.EventHandler(this.ClickEvent_selectTo);
            // 
            // lbl_valValve1Mod
            // 
            this.lbl_valValve1Mod.BackColor = System.Drawing.Color.White;
            this.lbl_valValve1Mod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbl_valValve1Mod.Location = new System.Drawing.Point(127, 153);
            this.lbl_valValve1Mod.Name = "lbl_valValve1Mod";
            this.lbl_valValve1Mod.Size = new System.Drawing.Size(49, 29);
            this.lbl_valValve1Mod.TabIndex = 307;
            this.lbl_valValve1Mod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_valValve1Mod.Click += new System.EventHandler(this.ClickEvent_valveFeedValue);
            // 
            // lbl_valValve2Mod
            // 
            this.lbl_valValve2Mod.BackColor = System.Drawing.Color.White;
            this.lbl_valValve2Mod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbl_valValve2Mod.Location = new System.Drawing.Point(127, 209);
            this.lbl_valValve2Mod.Name = "lbl_valValve2Mod";
            this.lbl_valValve2Mod.Size = new System.Drawing.Size(49, 29);
            this.lbl_valValve2Mod.TabIndex = 308;
            this.lbl_valValve2Mod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_valValve2Mod.Click += new System.EventHandler(this.ClickEvent_valveDosageValue);
            // 
            // lbl_valValve3Mod
            // 
            this.lbl_valValve3Mod.BackColor = System.Drawing.Color.White;
            this.lbl_valValve3Mod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbl_valValve3Mod.Location = new System.Drawing.Point(127, 265);
            this.lbl_valValve3Mod.Name = "lbl_valValve3Mod";
            this.lbl_valValve3Mod.Size = new System.Drawing.Size(49, 29);
            this.lbl_valValve3Mod.TabIndex = 309;
            this.lbl_valValve3Mod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_valValve3Mod.Click += new System.EventHandler(this.ClickEvent_valveGunAirValue);
            // 
            // lbl_valAMod
            // 
            this.lbl_valAMod.BackColor = System.Drawing.Color.White;
            this.lbl_valAMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbl_valAMod.Location = new System.Drawing.Point(323, 151);
            this.lbl_valAMod.Name = "lbl_valAMod";
            this.lbl_valAMod.Size = new System.Drawing.Size(49, 29);
            this.lbl_valAMod.TabIndex = 310;
            this.lbl_valAMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_valAMod.Click += new System.EventHandler(this.ClickEvent_microAmpereValue);
            // 
            // lbl_valVMod
            // 
            this.lbl_valVMod.BackColor = System.Drawing.Color.White;
            this.lbl_valVMod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.lbl_valVMod.Location = new System.Drawing.Point(323, 207);
            this.lbl_valVMod.Name = "lbl_valVMod";
            this.lbl_valVMod.Size = new System.Drawing.Size(49, 29);
            this.lbl_valVMod.TabIndex = 311;
            this.lbl_valVMod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_valVMod.Click += new System.EventHandler(this.ClickEvent_kiloVoltValue);
            // 
            // FormGunSettigs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(397, 417);
            this.Controls.Add(this.pb_icon);
            this.Controls.Add(this.lbl_header);
            this.Controls.Add(this.pnl_modifySettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormGunSettigs";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.pnl_modifySettings.ResumeLayout(false);
            this.pnl_modifySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_currentIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_pressureIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_separator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_icon;
        private System.Windows.Forms.Label lbl_header;
        private System.Windows.Forms.PictureBox pb_separator;
        private System.Windows.Forms.Label lbl_selectedPoints;
        private System.Windows.Forms.Label lbl_selectedFromText;
        private System.Windows.Forms.Label lbl_selectedToText;
        private System.Windows.Forms.Panel pnl_modifySettings;
        private RMLib.View.CustomButton btn_disableStatus;
        private System.Windows.Forms.Label lbl_valStatusModText;
        private System.Windows.Forms.Label lbl_valVModText;
        private System.Windows.Forms.Label lbl_valAModText;
        private System.Windows.Forms.Label lbl_valValve3ModText;
        private System.Windows.Forms.Label lbl_valValve2ModText;
        private System.Windows.Forms.Label lbl_valValve1ModText;
        private System.Windows.Forms.Label lbl_pressureText;
        private System.Windows.Forms.PictureBox pb_currentIcon;
        private System.Windows.Forms.Label lbl_currentText;
        private System.Windows.Forms.PictureBox pb_pressureIcon;
        private RMLib.View.CustomButton btn_enableStatus;
        private System.Windows.Forms.Label lbl_valVMod;
        private System.Windows.Forms.Label lbl_valAMod;
        private System.Windows.Forms.Label lbl_valValve3Mod;
        private System.Windows.Forms.Label lbl_valValve2Mod;
        private System.Windows.Forms.Label lbl_valValve1Mod;
        private System.Windows.Forms.Label lbl_selectedTo;
        private System.Windows.Forms.Label lbl_selectedFrom;
    }
}