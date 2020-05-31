namespace PsoWindowSize
{
    partial class frmPSO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPSO));
            this.pnlPSO = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlPSO
            // 
            this.pnlPSO.BackColor = System.Drawing.Color.Black;
            this.pnlPSO.Location = new System.Drawing.Point(0, 0);
            this.pnlPSO.Name = "pnlPSO";
            this.pnlPSO.Size = new System.Drawing.Size(1280, 960);
            this.pnlPSO.TabIndex = 0;
            // 
            // frmPSO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1086, 597);
            this.Controls.Add(this.pnlPSO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPSO";
            this.Text = "Phantasy Star Online - FullScreen";
            this.Activated += new System.EventHandler(this.frmPSO_Activated);
            this.Resize += new System.EventHandler(this.frmPSO_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlPSO;

    }
}