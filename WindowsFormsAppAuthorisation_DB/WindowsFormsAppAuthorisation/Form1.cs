using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace WindowsFormsAppAuthorisation
{
    public partial class Form1 : Form
    {
        public string defpath = "users.xml";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void enterbutton_Click(object sender, EventArgs e)
        {
            String loginUser = loginenbox.Text;
            String passwordUser = passenbox.Text;

            DB dB = new DB();

            DataTable table = new DataTable();
            
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `password` = @uP", dB.getConnection());

            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passwordUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("Error");
            } else
            {
                Form2 form2 = new Form2(loginUser);
                form2.Data = loginUser;
                form2.Data2 = passwordUser;
                form2.Show();
            }
        }

        private void loginenbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passenbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
