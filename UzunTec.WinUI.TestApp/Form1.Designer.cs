namespace UzunTec.WinUI.TestApp
{
    partial class Form1
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
            this.themeTextBox1 = new UzunTec.WinUI.Controls.ThemeTextBox();
            this.SuspendLayout();
            // 
            // themeTextBox1
            // 
            this.themeTextBox1.BackgroundColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(230)))));
            this.themeTextBox1.BackgroundColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(238)))));
            this.themeTextBox1.DisabledBackgroundColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.themeTextBox1.DisabledBackgroundColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.themeTextBox1.DisabledHintColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(143)))), ((int)(((byte)(154)))));
            this.themeTextBox1.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(143)))), ((int)(((byte)(154)))));
            this.themeTextBox1.FocusedBackgroundColorDark = System.Drawing.Color.Empty;
            this.themeTextBox1.FocusedBackgroundColorLight = System.Drawing.Color.Empty;
            this.themeTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.themeTextBox1.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(173)))), ((int)(((byte)(80)))));
            this.themeTextBox1.HintColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(143)))), ((int)(((byte)(154)))));
            this.themeTextBox1.HintFont = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.themeTextBox1.InternalPadding = new System.Windows.Forms.Padding(5);
            this.themeTextBox1.Location = new System.Drawing.Point(297, 134);
            this.themeTextBox1.Name = "themeTextBox1";
            this.themeTextBox1.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(143)))), ((int)(((byte)(154)))));
            this.themeTextBox1.PlaceholderFont = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.themeTextBox1.PlaceholderHintText = "12312312";
            this.themeTextBox1.Size = new System.Drawing.Size(200, 50);
            this.themeTextBox1.TabIndex = 0;
            this.themeTextBox1.Text = "23112312";
            this.themeTextBox1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(53)))), ((int)(((byte)(66)))));
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 272);
            this.Controls.Add(this.themeTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }


        #endregion

        private Controls.ThemeTextBox themeTextBox1;
    }
}

