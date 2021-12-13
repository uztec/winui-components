using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls
{
    public partial class ThemeDataGridView : DataGridView, IThemeControl
    {

        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();


        public ThemeDataGridView()
        {
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.AutoGenerateColumns = false;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.BorderStyle = BorderStyle.None;
            InitializeComponent();

            //DataGridView Scheme
            this.BackgroundColor = this.ThemeScheme.FormBackgroundDarkColor; //BackgroundDarkColor
            this.GridColor = this.ThemeScheme.FormBackgroundDarkColor; //BackgroundDarkColor
            this.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11.25F);
            this.ColumnHeadersDefaultCellStyle.BackColor = this.ThemeScheme.PrimaryColor; //PrimaryColor
            this.ColumnHeadersDefaultCellStyle.ForeColor = this.ThemeScheme.ControlTextLightColor; //TextLightColor 
            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = this.ThemeScheme.PrimaryColor; //PrimaryColor
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = this.ThemeScheme.ControlTextLightColor; //TextLightColor
            this.ColumnHeadersDefaultCellStyle.Padding = new Padding(2, 7, 0, 7);
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            this.DefaultCellStyle.BackColor = this.ThemeScheme.FormBackgroundDarkColor; //BackgroundDarkColor
            this.DefaultCellStyle.ForeColor = this.ThemeScheme.ControlTextColorDark; //TextDarkDefaultColor
            this.DefaultCellStyle.SelectionBackColor = this.ThemeScheme.PrimaryLightColor; //LightPrimaryColor
            this.DefaultCellStyle.SelectionForeColor = this.ThemeScheme.ControlTextColorDark; //TextDarkDefaultColor
            this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            this.AlternatingRowsDefaultCellStyle.BackColor = this.ThemeScheme.FormBackgroundDarkColor; //BackgroundDarkColor
        }

        public ThemeDataGridView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.EnableHeadersVisualStyles = false;
        }
    }
}
