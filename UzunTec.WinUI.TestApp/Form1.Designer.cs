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
            this.datePicker1 = new UzunTec.WinUI.Controls.DatePicker();
            this.SuspendLayout();
            // 
            // datePicker1
            // 
            this.datePicker1.BackgroundColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.datePicker1.BackgroundColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.datePicker1.CustomFormat = "dd-MMM-yyyy";
            this.datePicker1.DisabledBackgroundColorDark = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.datePicker1.DisabledBackgroundColorLight = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.datePicker1.DisabledHintColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(143)))), ((int)(((byte)(154)))));
            this.datePicker1.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(143)))), ((int)(((byte)(154)))));
            this.datePicker1.FocusedBackgroundColorDark = System.Drawing.Color.Empty;
            this.datePicker1.FocusedBackgroundColorLight = System.Drawing.Color.Empty;
            this.datePicker1.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.datePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePicker1.HighlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(173)))), ((int)(((byte)(80)))));
            this.datePicker1.HintColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(143)))), ((int)(((byte)(154)))));
            this.datePicker1.HintFont = new System.Drawing.Font("Segoe UI", 7F);
            this.datePicker1.InternalPadding = new System.Windows.Forms.Padding(5);
            this.datePicker1.Location = new System.Drawing.Point(340, 76);
            this.datePicker1.MinimumSize = new System.Drawing.Size(0, 0);
            this.datePicker1.Name = "datePicker1";
            this.datePicker1.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(143)))), ((int)(((byte)(154)))));
            this.datePicker1.PlaceholderFont = new System.Drawing.Font("Segoe UI", 15F);
            this.datePicker1.PlaceholderHintText = "Data de Entrega:";
            this.datePicker1.Size = new System.Drawing.Size(200, 50);
            this.datePicker1.TabIndex = 0;
            this.datePicker1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(53)))), ((int)(((byte)(66)))));
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 272);
            this.Controls.Add(this.datePicker1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DatePicker datePicker1;
    }
}

