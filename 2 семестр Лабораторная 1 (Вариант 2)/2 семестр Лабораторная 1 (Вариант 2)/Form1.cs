using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_семестр_Лабораторная_1__Вариант_2_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int value;
        bool chek = false;
        int result;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out value) || textBox1.Text.Length == 0 )
            {
                value = Math.Abs(value);
                chek = true;
                label2.Visible = false;
            }
            else
            {
                chek = false;
                label2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(chek)
            {
                for(int i = 0 ; value.ToString().Length > i  ; i++ )
                {
                   if( int.Parse( value.ToString()[i].ToString()  ) > 5)
                    {
                        result +=  int.Parse(value.ToString()[i].ToString());
                    }
                }
                MessageBox.Show(result.ToString());
                result = 0;
            }
            else
            {
                MessageBox.Show("В ВВОДЕ ОШИБОЧКА ТО !!!!");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ЗАЧЕМ ТРОГАТЬ ОШИБКУ???");
        }
    }
}
