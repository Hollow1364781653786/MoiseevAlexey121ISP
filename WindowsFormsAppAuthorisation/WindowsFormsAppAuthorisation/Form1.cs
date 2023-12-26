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
using System.Xml;


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
            if (!File.Exists(defpath)) CreateXMLDocument(defpath);
        }

        private void CreateXMLDocument(string filepath) //создание xml файла
        {
            XmlTextWriter xtw = new XmlTextWriter(filepath, Encoding.UTF32);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("users");
            xtw.WriteEndDocument();
            xtw.Close();
        }

        private string MaxID(string filepath) // вычисление ID, которое можно занять
        {
            List<int> idList = new List<int>();
            int id;
            XmlDocument xd = new XmlDocument();
            FileStream fs = new FileStream(filepath, FileMode.Open);
            xd.Load(fs);
            XmlNodeList list = xd.GetElementsByTagName("user");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement user = (XmlElement)xd.GetElementsByTagName("user")[i];
                    id = Convert.ToInt32(user.GetAttribute("id"));
                    idList.Add(id);
                }
                int result = 0;
                foreach (int j in idList)
                    if (j > result)
                        result = j;
                result++;
                fs.Close();
                return result.ToString();
            }
            else
            {
                fs.Close();
                return "1";
            }
        }

        private void WriteToXMLDocument(string filepath, string name, string pwd) //добавление записи
        {
            if (!File.Exists(defpath)) CreateXMLDocument(defpath);
            string id = MaxID(filepath);
            XmlDocument xd = new XmlDocument();
            xd.PreserveWhitespace = false;
            FileStream fs = new FileStream(filepath, FileMode.Open);
            xd.Load(fs);

            XmlElement user = xd.CreateElement("user");
            user.SetAttribute("id", id);

            XmlElement login = xd.CreateElement("login");
            XmlElement pass = xd.CreateElement("password");

            XmlText tLogin = xd.CreateTextNode(name);
            XmlText tPassword = xd.CreateTextNode(pwd);

            login.AppendChild(tLogin);
            pass.AppendChild(tPassword);

            user.AppendChild(login);
            user.AppendChild(pass);

            xd.DocumentElement.AppendChild(user);

            fs.Close();
            xd.Save(filepath);
        }

        private void registerbutton_Click_1(object sender, EventArgs e)
        {
            if (!(loginregbox.Text == "") & !(passregbox.Text == ""))
                WriteToXMLDocument(defpath, loginregbox.Text, passregbox.Text);
            else MessageBox.Show("Введите имя пользователя и пароль");
        }

        private void ReadXMLDocument(string filepath, string loget, string passget)
        {
            string name, pwd;
            XmlDocument xd = new XmlDocument();
            xd.PreserveWhitespace = false;
            FileStream fs = new FileStream(filepath, FileMode.Open);
            xd.Load(fs);
            XmlNodeList list = xd.GetElementsByTagName("user");
            for (int i = 0; i < list.Count; i++)
            {
                XmlElement user = (XmlElement)xd.GetElementsByTagName("login")[i];
                XmlElement pass = (XmlElement)xd.GetElementsByTagName("password")[i];
                name = user.InnerText;
                pwd = pass.InnerText;
                if ((loget == name) & (passget == pwd))
                {
                    Form2 form2 = new Form2(user.InnerText);
                    form2.Data = user.InnerText;
                    form2.Data2 = pass.InnerText;
                    form2.Show();
                    break;
                }
                else if (i == list.Count - 1) MessageBox.Show("Неверный логин или пароль");
            }
            fs.Close();
        }

        private void enterbutton_Click(object sender, EventArgs e)
        {
            if (!(loginenbox.Text == "") & !(passenbox.Text == ""))
            {
                ReadXMLDocument(defpath, loginenbox.Text, passenbox.Text);
            }
        }
    }
}
