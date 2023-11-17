namespace Raycasting.Engine
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBoxMain = new PictureBox();
            HUD = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HUD).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxMain
            // 
            pictureBoxMain.Location = new Point(0, 0);
            pictureBoxMain.Margin = new Padding(4, 3, 4, 3);
            pictureBoxMain.Name = "pictureBoxMain";
            pictureBoxMain.Size = new Size(117, 58);
            pictureBoxMain.TabIndex = 0;
            pictureBoxMain.TabStop = false;
            // 
            // HUD
            // 
            HUD.Location = new Point(17, 248);
            HUD.Name = "HUD";
            HUD.Size = new Size(100, 50);
            HUD.TabIndex = 1;
            HUD.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(331, 301);
            Controls.Add(HUD);
            Controls.Add(pictureBoxMain);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "Form1";
            FormClosing += MainWindow_FormClosing;
            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
            ((System.ComponentModel.ISupportInitialize)HUD).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxMain;
        private PictureBox HUD;
    }
}