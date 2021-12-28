using System;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls;

namespace UzunTec.WinUI.TestApp
{
    public partial class Form1 : ThemeBaseForm
    {
        public Form1()
        {
            this.ControlBox = false;
            //this.NonClientArea = new Padding(-5, 30, -5, 0);
            InitializeComponent();
            //this.themeComboBox1.DataSource = new[] { 1, 2, 3, 4, 5 };
            //this.themeComboBox2.DataSource = new[] { 1, 2, 3, 4, 5 };
            //this.comboBox1.DataSource = new[] { 1, 2, 3, 4, 5 };
            //this.comboBox2.DataSource = new[] { 1, 2, 3, 4, 5 };
            //this.comboBox3.DataSource = new[] { 1, 2, 3, 4, 5 };
        }

        //protected override void OnNcPaint(PaintEventArgs e)
        //{
        //    base.OnNcPaint(e);
        //    e.Graphics.Clear(Color.DarkCyan);
        //}
    }
}
