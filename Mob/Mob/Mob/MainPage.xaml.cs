using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mob
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        string num;
        int chis, sum = 0, i = 0;
        int[] arrayNum = new int[5];
        int[] arraySim = new int[5];
        private void button1_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "1";
            tb1.Text = tb1.Text + "1";
            num = tBOut.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "2";
            tb1.Text = tb1.Text + "2";
            num = tBOut.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "3";
            tb1.Text = tb1.Text + "3";
            num = tBOut.Text;
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "4";
            tb1.Text = tb1.Text + "4";
            num = tBOut.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "5";
            tb1.Text = tb1.Text + "5";
            num = tBOut.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "6";
            tb1.Text = tb1.Text + "6";
            num = tBOut.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "7";
            tb1.Text = tb1.Text + "7";
            num = tBOut.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "8";
            tb1.Text = tb1.Text + "8";
            num = tBOut.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "0";
            tb1.Text = tb1.Text + "0";
            num = tBOut.Text;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            tBOut.Text = tBOut.Text + "9";
            tb1.Text = tb1.Text + "9";
            num = tBOut.Text;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (tBOut.Text == string.Empty)
            {

            } else
            {
                tb1.Text = tb1.Text + "*";
                chis = int.Parse(num);
                arrayNum[i] = chis;
                arraySim[i] = 3;
                i++;
                tBOut.Text = "";
                num = "";
            }
        }

        private void btnPl_Click(object sender, EventArgs e)
        {
            if (tBOut.Text == string.Empty)
            {

            }
            else
            {
                tb1.Text = tb1.Text + "+";
                chis = int.Parse(num);
                arrayNum[i] = chis;
                arraySim[i] = 1;
                i++;
                tBOut.Text = "";
                num = "";
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            if (tBOut.Text == string.Empty)
            {

            }
            else
            {
                tb1.Text = tb1.Text + "-";
                chis = int.Parse(num);
                arrayNum[i] = chis;
                arraySim[i] = 2;
                i++;
                tBOut.Text = "";
                num = "";
            }
        }
        private void btnSls_Click(object sender, EventArgs e)
        {
            if (tBOut.Text == string.Empty || tBOut.Text == "0")
            {

            }
            else
            {
                tb1.Text = tb1.Text + "/";
                chis = int.Parse(num);
                arrayNum[i] = chis;
                arraySim[i] = 4;
                i++;
                tBOut.Text = "";
                num = "";
            }
        }

        private void btnRav_Click(object sender, EventArgs e)
        {

            chis = int.Parse(num);
            arrayNum[i] = chis;
            switch (arraySim[0])
            {
                case 1:
                    sum = arrayNum[0] + arrayNum[1];
                    break;
                case 2:
                    sum = arrayNum[0] - arrayNum[1];
                    break;
                case 3:
                    sum = arrayNum[0] * arrayNum[1];
                    break;
                case 4:
                    sum = arrayNum[0] / arrayNum[1];
                    break;
            }
            for (int j = 1; j < arraySim.Length + 1; j++)
            {
                switch (arraySim[j])
                {
                    case 1:
                        sum += arrayNum[j + 1];
                        break;
                    case 2:
                        sum -= arrayNum[j + 1];
                        break;
                    case 3:
                        sum *= arrayNum[j + 1];
                        break;
                    case 4:
                        sum /= arrayNum[j + 1];
                        break;
                }
                if (j == i)
                {
                    break;
                }
            }
            tBOut.Text = "";
            tb1.Text = sum.ToString();
        }
        private void btnClr_Click(object sender, EventArgs e)
        {
            tBOut.Text = string.Empty;
            tb1.Text = string.Empty;
            num = string.Empty;
        }
    }
}
