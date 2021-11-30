
namespace UzunTec.WinUI.Controls
{
    partial class ThemeBaseForm
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
            this.panelTitle = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCloseIcon = new System.Windows.Forms.Button();
            this.btnMaximizeIcon = new System.Windows.Forms.Button();
            this.btnMinimizeIcon = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelTitle.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(78)))), ((int)(((byte)(96)))));
            this.panelTitle.Controls.Add(this.flowLayoutPanel1);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(751, 29);
            this.panelTitle.TabIndex = 0;
            this.panelTitle.DoubleClick += new System.EventHandler(this.btnMaximize_Click);
            this.panelTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelTitle_MouseDoubleClick);
            this.panelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMovable_MouseDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.btnCloseIcon);
            this.flowLayoutPanel1.Controls.Add(this.btnMaximizeIcon);
            this.flowLayoutPanel1.Controls.Add(this.btnMinimizeIcon);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(660, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(91, 29);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMovable_MouseDown);
            // 
            // btnCloseIcon
            // 
            this.btnCloseIcon.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseIcon.FlatAppearance.BorderSize = 0;
            this.btnCloseIcon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(76)))), ((int)(((byte)(99)))));
            this.btnCloseIcon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(76)))), ((int)(((byte)(99)))));
            this.btnCloseIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseIcon.ForeColor = System.Drawing.Color.White;
            this.btnCloseIcon.Location = new System.Drawing.Point(65, 0);
            this.btnCloseIcon.Margin = new System.Windows.Forms.Padding(0);
            this.btnCloseIcon.Name = "btnCloseIcon";
            this.btnCloseIcon.Size = new System.Drawing.Size(26, 26);
            this.btnCloseIcon.TabIndex = 2;
            this.btnCloseIcon.UseVisualStyleBackColor = false;
            this.btnCloseIcon.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaximizeIcon
            // 
            this.btnMaximizeIcon.BackColor = System.Drawing.Color.Transparent;
            this.btnMaximizeIcon.FlatAppearance.BorderSize = 0;
            this.btnMaximizeIcon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(76)))), ((int)(((byte)(99)))));
            this.btnMaximizeIcon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(76)))), ((int)(((byte)(99)))));
            this.btnMaximizeIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximizeIcon.ForeColor = System.Drawing.Color.Transparent;
            this.btnMaximizeIcon.Location = new System.Drawing.Point(39, 0);
            this.btnMaximizeIcon.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaximizeIcon.Name = "btnMaximizeIcon";
            this.btnMaximizeIcon.Size = new System.Drawing.Size(26, 26);
            this.btnMaximizeIcon.TabIndex = 3;
            this.btnMaximizeIcon.UseVisualStyleBackColor = false;
            this.btnMaximizeIcon.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnMinimizeIcon
            // 
            this.btnMinimizeIcon.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizeIcon.FlatAppearance.BorderSize = 0;
            this.btnMinimizeIcon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(76)))), ((int)(((byte)(99)))));
            this.btnMinimizeIcon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(76)))), ((int)(((byte)(99)))));
            this.btnMinimizeIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizeIcon.ForeColor = System.Drawing.Color.Transparent;
            this.btnMinimizeIcon.Location = new System.Drawing.Point(13, 0);
            this.btnMinimizeIcon.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimizeIcon.Name = "btnMinimizeIcon";
            this.btnMinimizeIcon.Size = new System.Drawing.Size(26, 26);
            this.btnMinimizeIcon.TabIndex = 4;
            this.btnMinimizeIcon.UseVisualStyleBackColor = false;
            this.btnMinimizeIcon.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 29);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(751, 47);
            this.panelHeader.TabIndex = 1;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMovable_MouseDown);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblTitle.Location = new System.Drawing.Point(18, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(59, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMovable_MouseDown);
            // 
            // ThemeBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(751, 400);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThemeBaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormBase";
            this.panelTitle.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitle;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCloseIcon;
        private System.Windows.Forms.Button btnMaximizeIcon;
        private System.Windows.Forms.Button btnMinimizeIcon;
        private System.Windows.Forms.Label lblTitle;
    }
}