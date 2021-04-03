using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_семестр_2_Лабораторная
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool check_flag1 = false;
        bool check_flag2 = false;
        int m; //число строк
        int k; // число столбцов
        int now_rown = -1; //переменная - счетчик для while
        double italon = double.MinValue;
        Random rnd = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(check_flag1 && check_flag2)
                {
                    dataGridView1.RowCount = m;
                    dataGridView1.ColumnCount = k;
                    while (now_rown++ != m-1)
                    {
                        for( int now_colums = 0 ; now_colums != k  ; now_colums++)
                        {
                            dataGridView1[now_colums, now_rown].Value =  $"{-50+(rnd.NextDouble() * 150):f2}";
                        }
                    }
                 
                }
                else
                {
                    MessageBox.Show("В ВВОДЕ ОШИБКА");
                }
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < m; i++)        //i - строки таблицы
                {
                    for (int j = 0; j < k; j++)   //j- колонки таблицы
                    {
                        if (j % 2 == 1 && double.Parse(dataGridView1[j, i].Value.ToString()) > italon)
                        {
                            italon = double.Parse(dataGridView1[j, i].Value.ToString());
                            dataGridView1[0, i].Value = italon.ToString() + "  " + $"Строка:{i} Колонка:{j}";
                        }
 
                    }
                    italon = double.MinValue;
                }
                dataGridView1.ColumnCount = 1;
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length ==0 || int.TryParse(textBox1.Text, out m) && m < 10 && m > 0)
            {
                check_flag1 = true;
                label3.Visible = false;
            }
            else
            {
                label3.Visible = true;
                check_flag1 = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(textBox2.Text.Length == 0 || int.TryParse(textBox2.Text, out k) && k < 10 && k > 0 )
            {
                check_flag2 = true;
                label4.Visible = false;
            }
            else
            {
                check_flag2 = false;
                label4.Visible = true;
            }
        }


    }
}
