namespace UzunTec.WinUI.TestApp
{
    partial class StyledFormTest
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
            MainPanel.Location = new Point(10, 25);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(882, 395);
            MainPanel.TabIndex = 0;
            // 
            // StyledFormTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Red;
            BorderColorDark = Color.DarkBlue;
            BorderColorLight = Color.RoyalBlue;
            BorderWidth = 10;
            ClientSize = new Size(902, 430);
            ControlBoxAlign = ContentAlignment.MiddleRight;
            Controls.Add(MainPanel);
            DoubleBuffered = true;
            HeaderColorDark = Color.DimGray;
            HeaderColorLight = Color.Black;
            Location = new Point(0, 0);
            Name = "StyledFormTest";
            Text = "Uzun Technology Styled Form";
            ResumeLayout(false);
        }

        #endregion

        private Panel MainPanel;
    }
}