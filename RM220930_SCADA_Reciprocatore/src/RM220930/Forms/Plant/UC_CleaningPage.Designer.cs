namespace RM.src.RM220930.Forms.Plant
{
    partial class UC_CleaningPage
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
            this.btn_homePage = new RMLib.View.CustomButton();
            this.SuspendLayout();
            // 
            // lbl_home
            // 
            this.lbl_home.BackColor = System.Drawing.Color.Transparent;
            this.lbl_home.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_home.ForeColor = System.Drawing.SystemColors.Control;
            this.lbl_home.Location = new System.Drawing.Point(16, 632);
            this.lbl_home.Name = "lbl_home";
            this.lbl_home.Size = new System.Drawing.Size(60, 20);
            this.lbl_home.TabIndex = 39;
            this.lbl_home.Text = "Home";
            this.lbl_home.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_homePage
            // 
            this.btn_homePage.BackColor = System.Drawing.SystemColors.Control;
            this.btn_homePage.BackgroundColor = System.Drawing.SystemColors.Control;
            this.btn_homePage.BackgroundImage = global::RM.Properties.Resources.back32;
            this.btn_homePage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_homePage.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.btn_homePage.BorderRadius = 20;
            this.btn_homePage.BorderSize = 1;
            this.btn_homePage.FlatAppearance.BorderSize = 0;
            this.btn_homePage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_homePage.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_homePage.ForeColor = System.Drawing.Color.Black;
            this.btn_homePage.Location = new System.Drawing.Point(15, 569);
            this.btn_homePage.Name = "btn_homePage";
            this.btn_homePage.Size = new System.Drawing.Size(60, 60);
            this.btn_homePage.TabIndex = 40;
            this.btn_homePage.TextColor = System.Drawing.Color.Black;
            this.btn_homePage.UseVisualStyleBackColor = false;
            this.btn_homePage.Click += new System.EventHandler(this.btn_homePage_Click);
            // 
            // UC_CleaningPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::RM.Properties.Resources.UC_HomePage;
            this.Controls.Add(this.lbl_home);
            this.Controls.Add(this.btn_homePage);
            this.Name = "UC_CleaningPage";
            this.Size = new System.Drawing.Size(1024, 658);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_home;
        private RMLib.View.CustomButton btn_homePage;
    }
}
