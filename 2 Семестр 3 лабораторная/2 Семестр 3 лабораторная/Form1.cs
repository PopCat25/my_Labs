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
namespace _2_Семестр_3_лабораторная
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Подсказка: для работы программы необходимо указать путь к файлу");
        }

        OpenFileDialog opf = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(opf.FileName))
                {
                    string cheked_line = "";
                    string result_line = "";
                    while (!sr.EndOfStream)
                    {
                        cheked_line = sr.ReadLine();
                        if (cheked_line.Contains(" "))
                        {
                            result_line += cheked_line + "\n";
                        }

                    }
                    textBox2.Text = result_line;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(opf.ShowDialog() == DialogResult.OK)
            {
                string str = "";
                using (StreamReader sr = new StreamReader(opf.FileName))
                {
                    str = sr.ReadToEnd();
                }
                textBox1.Text = str;
                MessageBox.Show($"Выбраный путь: {opf.FileName}");
            }
            else
            {
                MessageBox.Show("Путь к текствовому файлу не указан");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(opf.FileName) == true)
                {
                    using (StreamWriter sw = new StreamWriter(opf.FileName))
                    {
                        sw.Write(textBox1.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Файл не выбран или не существует");
                    textBox1.Clear();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
