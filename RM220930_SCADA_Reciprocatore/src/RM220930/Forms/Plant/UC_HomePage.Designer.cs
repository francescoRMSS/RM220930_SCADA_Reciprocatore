namespace RM.src.RM220930.Forms.Plant
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
            this.lbl_title = new System.Windows.Forms.Label();
            this.pnl_axis = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pnl_options = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_hub = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pnl_axis.SuspendLayout();
            this.pnl_options.SuspendLayout();
            this.pnl_hub.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.BackColor = System.Drawing.Color.DimGray;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(1024, 50);
            this.lbl_title.TabIndex = 267;
            this.lbl_title.Text = "LAVORO";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_axis
            // 
            this.pnl_axis.BackColor = System.Drawing.Color.Gray;
            this.pnl_axis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_axis.Controls.Add(this.label6);
            this.pnl_axis.Location = new System.Drawing.Point(319, 50);
            this.pnl_axis.Name = "pnl_axis";
            this.pnl_axis.Size = new System.Drawing.Size(382, 459);
            this.pnl_axis.TabIndex = 358;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(-1, -1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(382, 39);
            this.label6.TabIndex = 364;
            this.label6.Text = "ASSI";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_options
            // 
            this.pnl_options.BackColor = System.Drawing.Color.Gray;
            this.pnl_options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_options.Controls.Add(this.label7);
            this.pnl_options.Location = new System.Drawing.Point(733, 50);
            this.pnl_options.Name = "pnl_options";
            this.pnl_options.Size = new System.Drawing.Size(260, 459);
            this.pnl_options.TabIndex = 359;
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
            this.label7.TabIndex = 365;
            this.label7.Text = "OPZIONI";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 362;
            // 
            // pnl_hub
            // 
            this.pnl_hub.BackColor = System.Drawing.Color.Gray;
            this.pnl_hub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_hub.Controls.Add(this.label5);
            this.pnl_hub.Controls.Add(this.label1);
            this.pnl_hub.Location = new System.Drawing.Point(23, 50);
            this.pnl_hub.Name = "pnl_hub";
            this.pnl_hub.Size = new System.Drawing.Size(260, 459);
            this.pnl_hub.TabIndex = 360;
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
            this.label5.Text = "HUB";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.pnl_hub);
            this.Controls.Add(this.pnl_options);
            this.Controls.Add(this.pnl_axis);
            this.Controls.Add(this.lbl_title);
            this.Name = "UC_HomePage";
            this.Size = new System.Drawing.Size(1024, 557);
            this.Load += new System.EventHandler(this.UC_HomePage_Load);
            this.pnl_axis.ResumeLayout(false);
            this.pnl_options.ResumeLayout(false);
            this.pnl_hub.ResumeLayout(false);
            this.pnl_hub.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Panel pnl_axis;
        private System.Windows.Forms.Panel pnl_options;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnl_hub;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}
