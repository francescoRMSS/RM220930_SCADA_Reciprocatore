namespace RM.src.RM220930.Forms.Plant.Axis
{
    partial class UC_axePosition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_axePosition));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.pnl_delay = new RMLib.View.CustomPanel();
            this.lbl_delay = new System.Windows.Forms.Label();
            this.btn_delayUp = new RMLib.View.CustomButton();
            this.btn_delayDown = new RMLib.View.CustomButton();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl_advance = new RMLib.View.CustomPanel();
            this.lbl_advance = new System.Windows.Forms.Label();
            this.btn_advanceUp = new RMLib.View.CustomButton();
            this.btn_advanceDown = new RMLib.View.CustomButton();
            this.label4 = new System.Windows.Forms.Label();
            this.pnl_distance = new RMLib.View.CustomPanel();
            this.lbl_distance = new System.Windows.Forms.Label();
            this.btn_distanceUp = new RMLib.View.CustomButton();
            this.btn_distanceDown = new RMLib.View.CustomButton();
            this.pnl_delay.SuspendLayout();
            this.pnl_advance.SuspendLayout();
            this.pnl_distance.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::RM.Properties.Resources.barrier_vertical_left;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel3.Location = new System.Drawing.Point(252, 95);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(44, 273);
            this.panel3.TabIndex = 388;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::RM.Properties.Resources.axe;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(14, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 273);
            this.panel1.TabIndex = 387;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::RM.Properties.Resources.barrier_vertical_right;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Location = new System.Drawing.Point(302, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(44, 273);
            this.panel2.TabIndex = 388;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(376, 138);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(190, 15);
            this.label16.TabIndex = 410;
            this.label16.Text = "Ritardo";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_delay
            // 
            this.pnl_delay.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pnl_delay.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.pnl_delay.BorderColor = System.Drawing.Color.DimGray;
            this.pnl_delay.BorderRadius = 15;
            this.pnl_delay.BorderSize = 2;
            this.pnl_delay.Controls.Add(this.lbl_delay);
            this.pnl_delay.ForeColor = System.Drawing.Color.White;
            this.pnl_delay.Location = new System.Drawing.Point(441, 95);
            this.pnl_delay.Name = "pnl_delay";
            this.pnl_delay.Size = new System.Drawing.Size(60, 40);
            this.pnl_delay.TabIndex = 413;
            this.pnl_delay.TextColor = System.Drawing.Color.White;
            // 
            // lbl_delay
            // 
            this.lbl_delay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lbl_delay.ForeColor = System.Drawing.Color.Black;
            this.lbl_delay.Location = new System.Drawing.Point(10, 7);
            this.lbl_delay.Name = "lbl_delay";
            this.lbl_delay.Size = new System.Drawing.Size(42, 27);
            this.lbl_delay.TabIndex = 3;
            this.lbl_delay.Text = "0";
            this.lbl_delay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_delay.Click += new System.EventHandler(this.ClickEvent_updateDelay);
            // 
            // btn_delayUp
            // 
            this.btn_delayUp.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_delayUp.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_delayUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_delayUp.BackgroundImage")));
            this.btn_delayUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_delayUp.BorderColor = System.Drawing.Color.DimGray;
            this.btn_delayUp.BorderRadius = 15;
            this.btn_delayUp.BorderSize = 2;
            this.btn_delayUp.FlatAppearance.BorderSize = 0;
            this.btn_delayUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delayUp.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btn_delayUp.ForeColor = System.Drawing.Color.White;
            this.btn_delayUp.Location = new System.Drawing.Point(506, 95);
            this.btn_delayUp.Name = "btn_delayUp";
            this.btn_delayUp.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btn_delayUp.Size = new System.Drawing.Size(60, 40);
            this.btn_delayUp.TabIndex = 412;
            this.btn_delayUp.TextColor = System.Drawing.Color.White;
            this.btn_delayUp.UseVisualStyleBackColor = false;
            this.btn_delayUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_delayUp_MouseDown);
            this.btn_delayUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_delayUp_MouseUp);
            // 
            // btn_delayDown
            // 
            this.btn_delayDown.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_delayDown.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_delayDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_delayDown.BackgroundImage")));
            this.btn_delayDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_delayDown.BorderColor = System.Drawing.Color.DimGray;
            this.btn_delayDown.BorderRadius = 15;
            this.btn_delayDown.BorderSize = 2;
            this.btn_delayDown.FlatAppearance.BorderSize = 0;
            this.btn_delayDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delayDown.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btn_delayDown.ForeColor = System.Drawing.Color.White;
            this.btn_delayDown.Location = new System.Drawing.Point(376, 95);
            this.btn_delayDown.Name = "btn_delayDown";
            this.btn_delayDown.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btn_delayDown.Size = new System.Drawing.Size(60, 40);
            this.btn_delayDown.TabIndex = 411;
            this.btn_delayDown.TextColor = System.Drawing.Color.White;
            this.btn_delayDown.UseVisualStyleBackColor = false;
            this.btn_delayDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_delayDown_MouseDown);
            this.btn_delayDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_delayDown_MouseUp);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(376, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 15);
            this.label2.TabIndex = 414;
            this.label2.Text = "Anticipo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_advance
            // 
            this.pnl_advance.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pnl_advance.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.pnl_advance.BorderColor = System.Drawing.Color.DimGray;
            this.pnl_advance.BorderRadius = 15;
            this.pnl_advance.BorderSize = 2;
            this.pnl_advance.Controls.Add(this.lbl_advance);
            this.pnl_advance.ForeColor = System.Drawing.Color.White;
            this.pnl_advance.Location = new System.Drawing.Point(441, 205);
            this.pnl_advance.Name = "pnl_advance";
            this.pnl_advance.Size = new System.Drawing.Size(60, 40);
            this.pnl_advance.TabIndex = 417;
            this.pnl_advance.TextColor = System.Drawing.Color.White;
            // 
            // lbl_advance
            // 
            this.lbl_advance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lbl_advance.ForeColor = System.Drawing.Color.Black;
            this.lbl_advance.Location = new System.Drawing.Point(10, 7);
            this.lbl_advance.Name = "lbl_advance";
            this.lbl_advance.Size = new System.Drawing.Size(42, 27);
            this.lbl_advance.TabIndex = 3;
            this.lbl_advance.Text = "0";
            this.lbl_advance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_advance.Click += new System.EventHandler(this.ClickEvent_updateAdvance);
            // 
            // btn_advanceUp
            // 
            this.btn_advanceUp.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_advanceUp.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_advanceUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_advanceUp.BackgroundImage")));
            this.btn_advanceUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_advanceUp.BorderColor = System.Drawing.Color.DimGray;
            this.btn_advanceUp.BorderRadius = 15;
            this.btn_advanceUp.BorderSize = 2;
            this.btn_advanceUp.FlatAppearance.BorderSize = 0;
            this.btn_advanceUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_advanceUp.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btn_advanceUp.ForeColor = System.Drawing.Color.White;
            this.btn_advanceUp.Location = new System.Drawing.Point(506, 205);
            this.btn_advanceUp.Name = "btn_advanceUp";
            this.btn_advanceUp.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btn_advanceUp.Size = new System.Drawing.Size(60, 40);
            this.btn_advanceUp.TabIndex = 416;
            this.btn_advanceUp.TextColor = System.Drawing.Color.White;
            this.btn_advanceUp.UseVisualStyleBackColor = false;
            this.btn_advanceUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_advanceUp_MouseDown);
            this.btn_advanceUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_advanceUp_MouseUp);
            // 
            // btn_advanceDown
            // 
            this.btn_advanceDown.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_advanceDown.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_advanceDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_advanceDown.BackgroundImage")));
            this.btn_advanceDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_advanceDown.BorderColor = System.Drawing.Color.DimGray;
            this.btn_advanceDown.BorderRadius = 15;
            this.btn_advanceDown.BorderSize = 2;
            this.btn_advanceDown.FlatAppearance.BorderSize = 0;
            this.btn_advanceDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_advanceDown.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btn_advanceDown.ForeColor = System.Drawing.Color.White;
            this.btn_advanceDown.Location = new System.Drawing.Point(376, 205);
            this.btn_advanceDown.Name = "btn_advanceDown";
            this.btn_advanceDown.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btn_advanceDown.Size = new System.Drawing.Size(60, 40);
            this.btn_advanceDown.TabIndex = 415;
            this.btn_advanceDown.TextColor = System.Drawing.Color.White;
            this.btn_advanceDown.UseVisualStyleBackColor = false;
            this.btn_advanceDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_advanceDown_MouseDown);
            this.btn_advanceDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_advanceDown_MouseUp);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(376, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 15);
            this.label4.TabIndex = 418;
            this.label4.Text = "Distanza";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_distance
            // 
            this.pnl_distance.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pnl_distance.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.pnl_distance.BorderColor = System.Drawing.Color.DimGray;
            this.pnl_distance.BorderRadius = 15;
            this.pnl_distance.BorderSize = 2;
            this.pnl_distance.Controls.Add(this.lbl_distance);
            this.pnl_distance.ForeColor = System.Drawing.Color.White;
            this.pnl_distance.Location = new System.Drawing.Point(441, 311);
            this.pnl_distance.Name = "pnl_distance";
            this.pnl_distance.Size = new System.Drawing.Size(60, 40);
            this.pnl_distance.TabIndex = 421;
            this.pnl_distance.TextColor = System.Drawing.Color.White;
            // 
            // lbl_distance
            // 
            this.lbl_distance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lbl_distance.ForeColor = System.Drawing.Color.Black;
            this.lbl_distance.Location = new System.Drawing.Point(10, 7);
            this.lbl_distance.Name = "lbl_distance";
            this.lbl_distance.Size = new System.Drawing.Size(42, 27);
            this.lbl_distance.TabIndex = 3;
            this.lbl_distance.Text = "0";
            this.lbl_distance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_distance.Click += new System.EventHandler(this.ClickEvent_updateDistance);
            // 
            // btn_distanceUp
            // 
            this.btn_distanceUp.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_distanceUp.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_distanceUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_distanceUp.BackgroundImage")));
            this.btn_distanceUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_distanceUp.BorderColor = System.Drawing.Color.DimGray;
            this.btn_distanceUp.BorderRadius = 15;
            this.btn_distanceUp.BorderSize = 2;
            this.btn_distanceUp.FlatAppearance.BorderSize = 0;
            this.btn_distanceUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_distanceUp.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btn_distanceUp.ForeColor = System.Drawing.Color.White;
            this.btn_distanceUp.Location = new System.Drawing.Point(506, 311);
            this.btn_distanceUp.Name = "btn_distanceUp";
            this.btn_distanceUp.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btn_distanceUp.Size = new System.Drawing.Size(60, 40);
            this.btn_distanceUp.TabIndex = 420;
            this.btn_distanceUp.TextColor = System.Drawing.Color.White;
            this.btn_distanceUp.UseVisualStyleBackColor = false;
            this.btn_distanceUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_distanceUp_MouseDown);
            this.btn_distanceUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_distanceUp_MouseUp);
            // 
            // btn_distanceDown
            // 
            this.btn_distanceDown.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_distanceDown.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_distanceDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_distanceDown.BackgroundImage")));
            this.btn_distanceDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_distanceDown.BorderColor = System.Drawing.Color.DimGray;
            this.btn_distanceDown.BorderRadius = 15;
            this.btn_distanceDown.BorderSize = 2;
            this.btn_distanceDown.FlatAppearance.BorderSize = 0;
            this.btn_distanceDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_distanceDown.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btn_distanceDown.ForeColor = System.Drawing.Color.White;
            this.btn_distanceDown.Location = new System.Drawing.Point(376, 311);
            this.btn_distanceDown.Name = "btn_distanceDown";
            this.btn_distanceDown.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btn_distanceDown.Size = new System.Drawing.Size(60, 40);
            this.btn_distanceDown.TabIndex = 419;
            this.btn_distanceDown.TextColor = System.Drawing.Color.White;
            this.btn_distanceDown.UseVisualStyleBackColor = false;
            this.btn_distanceDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_distanceDown_MouseDown);
            this.btn_distanceDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_distanceDown_MouseUp);
            // 
            // UC_axePosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnl_distance);
            this.Controls.Add(this.btn_distanceUp);
            this.Controls.Add(this.btn_distanceDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnl_advance);
            this.Controls.Add(this.btn_advanceUp);
            this.Controls.Add(this.btn_advanceDown);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.pnl_delay);
            this.Controls.Add(this.btn_delayUp);
            this.Controls.Add(this.btn_delayDown);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "UC_axePosition";
            this.Size = new System.Drawing.Size(590, 510);
            this.pnl_delay.ResumeLayout(false);
            this.pnl_advance.ResumeLayout(false);
            this.pnl_distance.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label16;
        private RMLib.View.CustomPanel pnl_delay;
        private System.Windows.Forms.Label lbl_delay;
        private RMLib.View.CustomButton btn_delayUp;
        private RMLib.View.CustomButton btn_delayDown;
        private System.Windows.Forms.Label label2;
        private RMLib.View.CustomPanel pnl_advance;
        private System.Windows.Forms.Label lbl_advance;
        private RMLib.View.CustomButton btn_advanceUp;
        private RMLib.View.CustomButton btn_advanceDown;
        private System.Windows.Forms.Label label4;
        private RMLib.View.CustomPanel pnl_distance;
        private System.Windows.Forms.Label lbl_distance;
        private RMLib.View.CustomButton btn_distanceUp;
        private RMLib.View.CustomButton btn_distanceDown;
    }
}
