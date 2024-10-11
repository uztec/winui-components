namespace UzunTec.WinUI.TestApp
{
    partial class ThemeFormTest
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
            MainPanel = new Panel();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.BackColor = Color.Yellow;
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(5, 85);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(894, 364);
            MainPanel.TabIndex = 1;
            // 
            // ThemeFormTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 454);
            Controls.Add(MainPanel);
            DoubleBuffered = true;
            Location = new Point(0, 0);
            Name = "ThemeFormTest";
            Text = "Uzun Technology ";
            Title = "WinUI Components";
            Load += Form2_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel MainPanel;
    }
}