using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_семестр_2_Лабораторная__2вариант_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool chek_flag1 = false; 
        bool chek_flag2 = false;
        int m; // число строк табицы
        int k; // число столбцов таблицы
        Random rnd = new Random(); 

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if( int.TryParse(textBox1.Text, out m) && m < 10 && m > 1 )
            {
                chek_flag1 = true;
                label3.Visible = false;
                label3.Text = "В ВВОДЕ ОШИБКА";
            }
            else if( textBox1.Text.Length == 0)
            {
                chek_flag1 = false;
                label3.Visible = true;
                label3.Text = "ПУСТОЕ ПОЛЕ";

            }
            else
            {
                chek_flag1 = false;
                label3.Visible = true;
                label3.Text = "В ВВОДЕ ОШИБКА";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox2.Text, out k) && k < 10 && k > 1)
            {
                chek_flag2 = true;
                label4.Visible = false;
                label4.Text = "В ВВОДЕ ОШИБКА";
            }
            else if (textBox2.Text.Length == 0)
            {
                chek_flag2 = false;
                label4.Visible = true;
                label4.Text = "ПУСТОЕ ПОЛЕ";
            }
            else
            {
                chek_flag2 = false;
                label4.Visible = true;
                label4.Text = "В ВВОДЕ ОШИБКА";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (chek_flag1 && chek_flag2)
                {
                    int i = 0; // i- число строк в таблице(m)
                    int j = 0; // j - число столбцов (k)
                    dataGridView1.RowCount = m;
                    dataGridView1.ColumnCount = k;

                    do
                    {
                        do
                        {
                            dataGridView1[j, i].Value = $"{(rnd.NextDouble() * -3900 + 1700):f2}";
                            j++;
                        }
                        while (j < k);
                        i++;
                        j = 0;
                    }
                    while (i < m);
                }
                else
                {
                    MessageBox.Show("В ВВОДЕ ОШИБКА!!!");
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (chek_flag1 && chek_flag2)
                {
                    for (int j = 0; j < k; j++) // i - строка j - столбец 
                    {
                        if (j <= (k - 1) / 2)
                        {
                            double temp_max_result = double.MaxValue;
                            double swap_variable;
                            int swap_row_index = 0;
                            int swap_collums_index = 0;
                            int row_for_index =m-1;
                            int i = 0;
                            while (row_for_index != 0)
                            {
                                for (i = row_for_index; i != -1; i--)
                                {
                                    if (Math.Abs(double.Parse(dataGridView1[j, i].Value.ToString())) < Math.Abs(temp_max_result))
                                    {
                                        temp_max_result = double.Parse(dataGridView1[j, i].Value.ToString());
                                        swap_collums_index = j;
                                        swap_row_index = i;
                                    }
                                }
                                swap_variable = double.Parse(dataGridView1[j, row_for_index].Value.ToString());
                                dataGridView1[j, swap_row_index].Value = swap_variable;
                                dataGridView1[j, row_for_index].Value = temp_max_result;
                                row_for_index--;
                                temp_max_result = double.MaxValue;

                        }   }
                        else
                        {
                            double temp_min_result = double.MaxValue;
                            double swap_variable;
                            int swap_row_index = 0;
                            int swap_collums_index = 0;
                            int row_for_index = 0;
                            int i = 0;
                            while (row_for_index != m)
                            {
                                for (i = row_for_index; i < m; i++)
                                {
                                    if (Math.Abs(double.Parse(dataGridView1[j, i].Value.ToString())) < Math.Abs(temp_min_result))
                                    {
                                        temp_min_result = double.Parse(dataGridView1[j, i].Value.ToString());
                                        swap_collums_index = j;
                                        swap_row_index = i;
                                    }
                                }
                                swap_variable = double.Parse(dataGridView1[j, row_for_index].Value.ToString());
                                dataGridView1[swap_collums_index, swap_row_index].Value = swap_variable;
                                dataGridView1[j,row_for_index].Value = temp_min_result;
                                row_for_index++;
                                temp_min_result = double.MaxValue;
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("В ВВОДЕ ОШИБКА!!!");
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    } 
}
