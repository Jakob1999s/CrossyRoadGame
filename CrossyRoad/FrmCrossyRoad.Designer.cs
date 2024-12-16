namespace CrossyRoad
{
    partial class FrmCrossyRoad
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCrossyRoad));
            this.tmrCrossyRoad = new System.Windows.Forms.Timer(this.components);
            this.btnTryAgain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tmrCrossyRoad
            // 
            this.tmrCrossyRoad.Interval = 1;
            this.tmrCrossyRoad.Tick += new System.EventHandler(this.tmrCrossyRoad_Tick);
            // 
            // btnTryAgain
            // 
            this.btnTryAgain.Font = new System.Drawing.Font("Cooper Black", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTryAgain.Location = new System.Drawing.Point(407, 411);
            this.btnTryAgain.Name = "btnTryAgain";
            this.btnTryAgain.Size = new System.Drawing.Size(284, 67);
            this.btnTryAgain.TabIndex = 0;
            this.btnTryAgain.Text = "Try again?";
            this.btnTryAgain.UseVisualStyleBackColor = true;
            this.btnTryAgain.Visible = false;
            this.btnTryAgain.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // FrmCrossyRoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1722, 915);
            this.Controls.Add(this.btnTryAgain);
            this.Font = new System.Drawing.Font("Algerian", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1164, 649);
            this.Name = "FrmCrossyRoad";
            this.Text = "FrmCrossyRoad";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCrossyRoad_Load);
            this.SizeChanged += new System.EventHandler(this.FrmCrossyRoad_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmCrossyRoad_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCrossyRoad_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmCrossyRoad_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmCrossyRoad_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrCrossyRoad;
        private System.Windows.Forms.Button btnTryAgain;
    }
}