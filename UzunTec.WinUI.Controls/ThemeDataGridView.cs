using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls
{
    public partial class ThemeDataGridView : DataGridView, IThemeControl
    {
        [Browsable(false), ReadOnly(true)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Browsable(false), ReadOnly(true)]
        public bool MouseHovered { get; private set; }

        [Browsable(false), ReadOnly(true)]
        public bool UpdatingTheme { get; set; }

        [Category("Theme"), DefaultValue(true)]
        public bool UseThemeColors { get => this.props.UseThemeColors; set => this.props.UseThemeColors = value; }

        [Category("Theme"), DefaultValue(typeof(Padding), "1; 1; 1; 1;")]
        public Padding InternalPadding { get => this.props.InternalPadding; set => this.props.InternalPadding = value; }

        private readonly ThemeControlProperties props;

        public ThemeDataGridView()
        {
            this.props = new ThemeControlProperties(this);
            InitializeComponent();
        }

        public void UpdateStylesFromTheme()
        {
            //DataGridView Scheme
            this.BackgroundColor = ThemeScheme.CellBackgroundColor; //BackgroundDarkColor
            this.GridColor = ThemeScheme.CellBackgroundColor; //BackgroundDarkColor
            this.ColumnHeadersDefaultCellStyle.Font = ThemeScheme.GridFont; //"Segoe UI Semibold", 11.25F
            this.ColumnHeadersDefaultCellStyle.BackColor = ThemeScheme.FormTitlePanelBackgroundColorDark; //PrimaryColor
            this.ColumnHeadersDefaultCellStyle.ForeColor = ThemeScheme.ControlTextColorLight; //TextLightColor 
            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = ThemeScheme.FormTitlePanelBackgroundColorDark; //PrimaryColor
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = ThemeScheme.ControlTextColorLight; //TextLightColor
            this.ColumnHeadersDefaultCellStyle.Padding = new Padding(2, 7, 0, 7);
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            this.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            this.DefaultCellStyle.BackColor = ThemeScheme.CellBackgroundColor; //BackgroundDarkColor
            this.DefaultCellStyle.ForeColor = ThemeScheme.ControlTextColorDark; //TextDarkDefaultColor
            this.DefaultCellStyle.SelectionBackColor = this.ThemeScheme.FormTitlePanelBackgroundColorDark; //LightPrimaryColor
            this.DefaultCellStyle.SelectionForeColor = this.ThemeScheme.ControlTextColorDark; //TextDarkDefaultColor
            this.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            this.AlternatingRowsDefaultCellStyle.BackColor = this.ThemeScheme.FormTitlePanelBackgroundColorDark; //BackgroundDarkColor
        }

        protected override void OnCreateControl()
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

            base.OnCreateControl();
            this.EnableHeadersVisualStyles = false;
        }

        public void UpdateRects()
        {

        }
    }
}
