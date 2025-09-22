namespace RM.src.RM240513.Forms
{
    partial class FormTrackerCalibration
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
            this.gbCalibration = new System.Windows.Forms.GroupBox();
            this.btnConfirmCalibration = new System.Windows.Forms.Button();
            this.btnRecordX = new System.Windows.Forms.Button();
            this.btnRecordZ = new System.Windows.Forms.Button();
            this.btnRecordOrigin = new System.Windows.Forms.Button();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.progressCalibration = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnTestPoint = new System.Windows.Forms.Button();
            this.btnViewNewFrame = new System.Windows.Forms.Button();
            this.lblX = new System.Windows.Forms.Label();
            this.lblZ = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblRX = new System.Windows.Forms.Label();
            this.lblRY = new System.Windows.Forms.Label();
            this.lblRZ = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_effettueràre = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGoToOrigin = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbl_autoCalib = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.PnlClose = new System.Windows.Forms.Panel();
            this.gbCalibration.SuspendLayout();
            this.gbStatus.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCalibration
            // 
            this.gbCalibration.Controls.Add(this.btnConfirmCalibration);
            this.gbCalibration.Controls.Add(this.btnRecordX);
            this.gbCalibration.Controls.Add(this.btnRecordZ);
            this.gbCalibration.Controls.Add(this.btnRecordOrigin);
            this.gbCalibration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCalibration.Location = new System.Drawing.Point(12, 39);
            this.gbCalibration.Name = "gbCalibration";
            this.gbCalibration.Size = new System.Drawing.Size(114, 138);
            this.gbCalibration.TabIndex = 20;
            this.gbCalibration.TabStop = false;
            this.gbCalibration.Text = "Calibration";
            // 
            // btnConfirmCalibration
            // 
            this.btnConfirmCalibration.Location = new System.Drawing.Point(6, 106);
            this.btnConfirmCalibration.Name = "btnConfirmCalibration";
            this.btnConfirmCalibration.Size = new System.Drawing.Size(101, 23);
            this.btnConfirmCalibration.TabIndex = 2;
            this.btnConfirmCalibration.Text = "Confirm calibration";
            this.btnConfirmCalibration.UseVisualStyleBackColor = true;
            this.btnConfirmCalibration.Click += new System.EventHandler(this.ClickEvent_confirmCalibration);
            // 
            // btnRecordX
            // 
            this.btnRecordX.Location = new System.Drawing.Point(6, 48);
            this.btnRecordX.Name = "btnRecordX";
            this.btnRecordX.Size = new System.Drawing.Size(101, 23);
            this.btnRecordX.TabIndex = 1;
            this.btnRecordX.Text = "Record X";
            this.btnRecordX.UseVisualStyleBackColor = true;
            this.btnRecordX.Click += new System.EventHandler(this.ClickEvent_recordAxisX);
            // 
            // btnRecordZ
            // 
            this.btnRecordZ.Location = new System.Drawing.Point(6, 77);
            this.btnRecordZ.Name = "btnRecordZ";
            this.btnRecordZ.Size = new System.Drawing.Size(101, 23);
            this.btnRecordZ.TabIndex = 3;
            this.btnRecordZ.Text = "Record Z";
            this.btnRecordZ.UseVisualStyleBackColor = true;
            this.btnRecordZ.Click += new System.EventHandler(this.ClickEvent_recordAxisZ);
            // 
            // btnRecordOrigin
            // 
            this.btnRecordOrigin.Location = new System.Drawing.Point(6, 19);
            this.btnRecordOrigin.Name = "btnRecordOrigin";
            this.btnRecordOrigin.Size = new System.Drawing.Size(101, 23);
            this.btnRecordOrigin.TabIndex = 0;
            this.btnRecordOrigin.Text = "Record Origin";
            this.btnRecordOrigin.UseVisualStyleBackColor = true;
            this.btnRecordOrigin.Click += new System.EventHandler(this.ClickEvent_recordOrigin);
            // 
            // gbStatus
            // 
            this.gbStatus.Controls.Add(this.progressCalibration);
            this.gbStatus.Controls.Add(this.lblStatus);
            this.gbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbStatus.Location = new System.Drawing.Point(132, 39);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new System.Drawing.Size(359, 138);
            this.gbStatus.TabIndex = 21;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "Status";
            // 
            // progressCalibration
            // 
            this.progressCalibration.Location = new System.Drawing.Point(8, 57);
            this.progressCalibration.Name = "progressCalibration";
            this.progressCalibration.Size = new System.Drawing.Size(343, 23);
            this.progressCalibration.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(8, 87);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(343, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "lblStatus";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.txtLog);
            this.gbLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLog.Location = new System.Drawing.Point(12, 257);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(479, 311);
            this.gbLog.TabIndex = 22;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 18);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(473, 290);
            this.txtLog.TabIndex = 0;
            // 
            // btnTestPoint
            // 
            this.btnTestPoint.Location = new System.Drawing.Point(239, 21);
            this.btnTestPoint.Name = "btnTestPoint";
            this.btnTestPoint.Size = new System.Drawing.Size(108, 23);
            this.btnTestPoint.TabIndex = 24;
            this.btnTestPoint.Text = "Test point";
            this.btnTestPoint.UseVisualStyleBackColor = true;
            this.btnTestPoint.Click += new System.EventHandler(this.ClickEvent_testPoint);
            // 
            // btnViewNewFrame
            // 
            this.btnViewNewFrame.Location = new System.Drawing.Point(363, 21);
            this.btnViewNewFrame.Name = "btnViewNewFrame";
            this.btnViewNewFrame.Size = new System.Drawing.Size(108, 23);
            this.btnViewNewFrame.TabIndex = 25;
            this.btnViewNewFrame.Text = "View new frame";
            this.btnViewNewFrame.UseVisualStyleBackColor = true;
            this.btnViewNewFrame.Click += new System.EventHandler(this.ClickEvent_viewNewFrame);
            // 
            // lblX
            // 
            this.lblX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblX.Location = new System.Drawing.Point(105, 43);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(80, 30);
            this.lblX.TabIndex = 26;
            this.lblX.Text = "x";
            this.lblX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZ
            // 
            this.lblZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZ.Location = new System.Drawing.Point(288, 43);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(80, 30);
            this.lblZ.TabIndex = 27;
            this.lblZ.Text = "z";
            this.lblZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblY
            // 
            this.lblY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY.Location = new System.Drawing.Point(197, 43);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(80, 30);
            this.lblY.TabIndex = 28;
            this.lblY.Text = "y";
            this.lblY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRX
            // 
            this.lblRX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRX.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRX.Location = new System.Drawing.Point(105, 111);
            this.lblRX.Name = "lblRX";
            this.lblRX.Size = new System.Drawing.Size(80, 30);
            this.lblRX.TabIndex = 29;
            this.lblRX.Text = "rx";
            this.lblRX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRY
            // 
            this.lblRY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRY.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRY.Location = new System.Drawing.Point(197, 111);
            this.lblRY.Name = "lblRY";
            this.lblRY.Size = new System.Drawing.Size(80, 30);
            this.lblRY.TabIndex = 30;
            this.lblRY.Text = "ry";
            this.lblRY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRZ
            // 
            this.lblRZ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRZ.Location = new System.Drawing.Point(288, 111);
            this.lblRZ.Name = "lblRZ";
            this.lblRZ.Size = new System.Drawing.Size(80, 30);
            this.lblRZ.TabIndex = 31;
            this.lblRZ.Text = "rz";
            this.lblRZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblY);
            this.groupBox1.Controls.Add(this.lblRZ);
            this.groupBox1.Controls.Add(this.lblX);
            this.groupBox1.Controls.Add(this.lblRY);
            this.groupBox1.Controls.Add(this.lblZ);
            this.groupBox1.Controls.Add(this.lblRX);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 574);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 153);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Calculated pose";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(288, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 37;
            this.label6.Text = "rz";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(197, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "ry";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(105, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 35;
            this.label4.Text = "rx";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(288, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 34;
            this.label3.Text = "z";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(197, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 33;
            this.label2.Text = "y";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(105, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "x";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_effettueràre
            // 
            this.btn_effettueràre.Location = new System.Drawing.Point(6, 21);
            this.btn_effettueràre.Name = "btn_effettueràre";
            this.btn_effettueràre.Size = new System.Drawing.Size(104, 23);
            this.btn_effettueràre.TabIndex = 34;
            this.btn_effettueràre.Text = "Use last frame";
            this.btn_effettueràre.UseVisualStyleBackColor = true;
            this.btn_effettueràre.Click += new System.EventHandler(this.ClickEvent_getLastFrame);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGoToOrigin);
            this.groupBox2.Controls.Add(this.btnViewNewFrame);
            this.groupBox2.Controls.Add(this.btn_effettueràre);
            this.groupBox2.Controls.Add(this.btnTestPoint);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 58);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Functions";
            // 
            // btnGoToOrigin
            // 
            this.btnGoToOrigin.Location = new System.Drawing.Point(121, 22);
            this.btnGoToOrigin.Name = "btnGoToOrigin";
            this.btnGoToOrigin.Size = new System.Drawing.Size(108, 23);
            this.btnGoToOrigin.TabIndex = 35;
            this.btnGoToOrigin.Text = "Go to origin";
            this.btnGoToOrigin.UseVisualStyleBackColor = true;
            this.btnGoToOrigin.Click += new System.EventHandler(this.ClickEvent_goToOrigin);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.button4);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.progressBar1);
            this.groupBox3.Controls.Add(this.lbl_autoCalib);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(503, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(133, 488);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 57);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(117, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // lbl_autoCalib
            // 
            this.lbl_autoCalib.Location = new System.Drawing.Point(8, 87);
            this.lbl_autoCalib.Name = "lbl_autoCalib";
            this.lbl_autoCalib.Size = new System.Drawing.Size(117, 23);
            this.lbl_autoCalib.TabIndex = 0;
            this.lbl_autoCalib.Text = "label7";
            this.lbl_autoCalib.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(14, 410);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(104, 23);
            this.button6.TabIndex = 93;
            this.button6.Text = "Torna a home";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Location = new System.Drawing.Point(14, 363);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 23);
            this.button5.TabIndex = 92;
            this.button5.Text = "Vai a Z";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(14, 310);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 23);
            this.button4.TabIndex = 91;
            this.button4.Text = "Torna a origine";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(14, 257);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 23);
            this.button3.TabIndex = 90;
            this.button3.Text = "Vai a X";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(14, 207);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 89;
            this.button2.Text = "Vai a origine";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 88;
            this.button1.Text = "Vai a home ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // PnlClose
            // 
            this.PnlClose.BackColor = System.Drawing.Color.Red;
            this.PnlClose.BackgroundImage = global::RM.Properties.Resources.close_filled;
            this.PnlClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PnlClose.Location = new System.Drawing.Point(609, 0);
            this.PnlClose.Name = "PnlClose";
            this.PnlClose.Size = new System.Drawing.Size(34, 25);
            this.PnlClose.TabIndex = 82;
            this.PnlClose.Click += new System.EventHandler(this.PnlClose_Click);
            // 
            // FormTrackerCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 742);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.PnlClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.gbStatus);
            this.Controls.Add(this.gbCalibration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTrackerCalibration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calibrazione HTC VIVE Tracker 3.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingEvent);
            this.gbCalibration.ResumeLayout(false);
            this.gbStatus.ResumeLayout(false);
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbCalibration;
        private System.Windows.Forms.Button btnRecordOrigin;
        private System.Windows.Forms.Button btnRecordX;
        private System.Windows.Forms.Button btnConfirmCalibration;
        private System.Windows.Forms.Button btnRecordZ;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.ProgressBar progressCalibration;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnTestPoint;
        private System.Windows.Forms.Button btnViewNewFrame;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblRX;
        private System.Windows.Forms.Label lblRY;
        private System.Windows.Forms.Label lblRZ;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_effettueràre;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGoToOrigin;
        private System.Windows.Forms.Panel PnlClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbl_autoCalib;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}