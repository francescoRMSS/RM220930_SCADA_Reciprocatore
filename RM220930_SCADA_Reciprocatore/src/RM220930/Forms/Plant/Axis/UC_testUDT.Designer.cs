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
            this.btn_Cmd_On_Axe = new System.Windows.Forms.Button();
            this.btn_Cmd_En_Axe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Cmd_On_Axe
            // 
            this.btn_Cmd_On_Axe.Location = new System.Drawing.Point(21, 13);
            this.btn_Cmd_On_Axe.Name = "btn_Cmd_On_Axe";
            this.btn_Cmd_On_Axe.Size = new System.Drawing.Size(85, 23);
            this.btn_Cmd_On_Axe.TabIndex = 0;
            this.btn_Cmd_On_Axe.Text = "Cmd_On_Axe";
            this.btn_Cmd_On_Axe.UseVisualStyleBackColor = true;
            this.btn_Cmd_On_Axe.Click += new System.EventHandler(this.btn_Cmd_On_Axe_Click);
            // 
            // btn_Cmd_En_Axe
            // 
            this.btn_Cmd_En_Axe.Location = new System.Drawing.Point(21, 42);
            this.btn_Cmd_En_Axe.Name = "btn_Cmd_En_Axe";
            this.btn_Cmd_En_Axe.Size = new System.Drawing.Size(85, 23);
            this.btn_Cmd_En_Axe.TabIndex = 1;
            this.btn_Cmd_En_Axe.Text = "Cmd_En_Axe";
            this.btn_Cmd_En_Axe.UseVisualStyleBackColor = true;
            this.btn_Cmd_En_Axe.Click += new System.EventHandler(this.btn_Cmd_En_Axe_Click);
            // 
            // UC_testUDT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Cmd_En_Axe);
            this.Controls.Add(this.btn_Cmd_On_Axe);
            this.Name = "UC_testUDT";
            this.Size = new System.Drawing.Size(1024, 557);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cmd_On_Axe;
        private System.Windows.Forms.Button btn_Cmd_En_Axe;
    }
}
