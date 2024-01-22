using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppAuthorisation
{
    public partial class Form2 : Form
    {
        public string Data { get; set; }
        public string Data2 { get; set; }
        public Form2(string data)
        {
            InitializeComponent();
        }
        public Form2(Form1 f)
        {
            InitializeComponent();
            f.BackColor = Color.Yellow;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = "Ваш логин: " + Data;
            label2.Text = "Ваш пароль: " + Data2;
        }
    }
}
