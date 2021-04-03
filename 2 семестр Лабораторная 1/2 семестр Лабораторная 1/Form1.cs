using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_семестр_Лабораторная_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int min_value;
        int max_value;
        int num_chek = 0;
        bool check_min_value = true;
        bool check_max_value = true;
        List<int> result = new List<int>();
        string showing_result;



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0 && int.TryParse(textBox1.Text, out min_value))
            {
                check_min_value = true;
                min_value = int.Parse(textBox1.Text);
            }
            else
            {
                check_min_value = false;
            }
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length != 0 && int.TryParse(textBox2.Text, out max_value))
            {
                check_max_value = true;
                max_value = int.Parse(textBox2.Text);
            }
            else
            {
                check_max_value = false;
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if(max_value >= min_value && check_max_value &&check_min_value )
            {
                
                for (int count = min_value; count <= max_value; count++)
                {
                    int modul = Math.Abs(count);
                    for (int i = 0; i < modul.ToString().Length; i++)
                    {
                        num_chek += int.Parse((modul.ToString()[i]).ToString());
                    }

                    if (num_chek % 7 == 0 && count % 7 == 0)
                    {
                        result.Add(count);
                    }
                    num_chek = 0;
                }

                for (int i = 0; i < result.Count; i++)
                {
                    showing_result += result[i].ToString() + ' ';
                }

                MessageBox.Show(showing_result);
                result.Clear();
                showing_result = "";
                
            }
            else if(!check_max_value)
            {
                MessageBox.Show("В поле максимального сообщениия ошибка, введите число");
            }
            else if (!check_min_value)
            {
                MessageBox.Show("В поле минимального сообщениия ошибка, введите число");
            }


            else
            {
                MessageBox.Show("Максимальное значение меньше минимального");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
