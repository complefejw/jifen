using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 消防积分获取
{
    public partial class Form2 : Form
    {
        public string st;
        public Form2()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show(dateTimePicker1.Value.ToString());

            MessageBox.Show(st);
        }
    }
}
