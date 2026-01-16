namespace RM.src.RM220930.Forms.Plant
{
    partial class UC_MotorPanel
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
            this.lb_motorTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_motorTitle
            // 
            this.lb_motorTitle.BackColor = System.Drawing.Color.Gray;
            this.lb_motorTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_motorTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lb_motorTitle.ForeColor = System.Drawing.Color.White;
            this.lb_motorTitle.Location = new System.Drawing.Point(0, 0);
            this.lb_motorTitle.Name = "lb_motorTitle";
            this.lb_motorTitle.Size = new System.Drawing.Size(319, 39);
            this.lb_motorTitle.TabIndex = 362;
            this.lb_motorTitle.Text = "MOTORE REC";
            this.lb_motorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UC_MotorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.lb_motorTitle);
            this.Name = "UC_MotorPanel";
            this.Size = new System.Drawing.Size(319, 177);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_motorTitle;
    }
}
