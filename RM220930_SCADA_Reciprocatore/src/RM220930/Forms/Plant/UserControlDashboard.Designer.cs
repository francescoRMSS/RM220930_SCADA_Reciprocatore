namespace RM.src.RM220930.Forms.Plant
{
    partial class UserControlDashboard
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btn_impostazioni = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(238, 160);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(35, 13);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "label1";
            // 
            // btn_impostazioni
            // 
            this.btn_impostazioni.Location = new System.Drawing.Point(345, 91);
            this.btn_impostazioni.Name = "btn_impostazioni";
            this.btn_impostazioni.Size = new System.Drawing.Size(75, 23);
            this.btn_impostazioni.TabIndex = 1;
            this.btn_impostazioni.Text = "Impostazioni";
            this.btn_impostazioni.UseVisualStyleBackColor = true;
            this.btn_impostazioni.Click += new System.EventHandler(this.btn_impostazioni_Click);
            // 
            // UserControlDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_impostazioni);
            this.Controls.Add(this.lblWelcome);
            this.Name = "UserControlDashboard";
            this.Size = new System.Drawing.Size(521, 384);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btn_impostazioni;
    }
}
