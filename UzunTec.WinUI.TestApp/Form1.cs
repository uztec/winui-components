using System;
using System.Windows.Forms;
using UzunTec.WinUI.Controls;

namespace UzunTec.WinUI.TestApp
{
    public partial class Form1 : ThemeBaseForm
    {
        public Form1()
        {
            InitializeComponent();
            //this.themeComboBox1.DataSource = new[] { 1, 2, 3, 4, 5 };
            //this.themeComboBox2.DataSource = new[] { 1, 2, 3, 4, 5 };
            //this.comboBox1.DataSource = new[] { 1, 2, 3, 4, 5 };
            //this.comboBox2.DataSource = new[] { 1, 2, 3, 4, 5 };
            //this.comboBox3.DataSource = new[] { 1, 2, 3, 4, 5 };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = this.RectClient;
            var a = this.RectClient2;
            var b = this.RectNewWindow;
            var c = this.RectOldWindow;



        }
    }
}
