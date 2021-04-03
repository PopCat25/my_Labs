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

namespace _2_Семестр_4_Лабораторная
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog opf = new OpenFileDialog();

        public struct subscribe_information
        {
            public string surname;
            public string name;
            public string middle_name;
            public string phone_num;

            public string Return_subs( subscribe_information si )
            {
                return si.surname + si.name + si.middle_name + si.phone_num;
            }

            public string Return_subs_with_sep(in subscribe_information si)
            {
                return si.surname + "$" + si.name + "$" + si.middle_name + "$" + si.phone_num;
            }
        }

        void Refresh_tab()
        {
            try
            {
                dataGridView1.Rows.Clear();
                using (StreamReader sr = new StreamReader(opf.FileName))
                {
                    string[] str;
                    for (int i = 0; !sr.EndOfStream; i++)
                    {
                        str = sr.ReadLine().Split('$');
                        dataGridView1.RowCount = i + 2;
                        dataGridView1[0, i].Value = str[0] + " " + str[1] + " " + str[2];
                        dataGridView1[1, i].Value = str[3];
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                opf.ShowDialog();
                if(File.Exists(opf.FileName))
                    MessageBox.Show($"Выбранный путь: {opf.FileName}");
                Refresh_tab();
            }
            catch(Exception)
            {
                MessageBox.Show("Файл не выбран");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                    subscribe_information si = new subscribe_information();
                    Form2 form2 = new Form2();
                    form2.Make_rec(ref si, ref form2,in opf);
                    Refresh_tab();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 form2 = new Form2();
                subscribe_information si = new subscribe_information();
                form2.Ask(ref si, ref form2);
                dataGridView1.Rows.Clear();

                using (StreamReader sr = new StreamReader(opf.FileName))
                {
                    int i = 0;
                    string[] str;
                    while ( !sr.EndOfStream )
                    {
                        str = sr.ReadLine().Split('$');
                        if ((si.surname.Length != 0 & str[0].Contains(si.surname)) || (si.name.Length != 0 & str[1].Contains(si.name)) || (si.middle_name.Length !=0 & str[2].Contains(si.middle_name)) ||( si.phone_num.Length !=0 & str[3].Contains(si.phone_num)))
                        {
                            dataGridView1.RowCount = i + 2;
                            dataGridView1[0, i].Value = str[0] + " " + str[1] + " " + str[2];
                            dataGridView1[1, i].Value = str[3];
                            i++;
                        }
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Refresh_tab();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           try
            {
                subscribe_information si = new subscribe_information();
                Form2 form2 = new Form2();
                form2.Ask(ref si,ref form2);

                string result = "";
                using (StreamReader sr = new StreamReader(opf.FileName))
                {
                    
                    string[] str;
                    string strs;
                    
                    while (!sr.EndOfStream)
                    {
                        strs = sr.ReadLine();
                        str = strs.Split('$');
                        if (!( str[0].Equals(si.surname) &&   str[1].Equals(si.name) &&   str[2].Equals(si.middle_name) &&  str[3].Equals(si.phone_num)))
                        {
                            result += strs +"\r\n";
                        }
                    }
                }
                File.WriteAllText(opf.FileName, result);
                Refresh_tab();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                subscribe_information si = new subscribe_information();
                subscribe_information si_2 = new subscribe_information();
                Form2 form2 = new Form2();
                Form2 form2_2 = new Form2();
                form2.Ask(ref si, ref form2);
                form2_2.Text = "Измените значения";
                form2_2.Ask(ref si_2, ref form2_2);
                
                string result = "";
                using (StreamReader sr = new StreamReader(opf.FileName))
                {

                    string[] str;
                    string strs;

                    while (!sr.EndOfStream)
                    {
                        strs = sr.ReadLine();
                        str = strs.Split('$');
                        if (str[0].Equals(si.surname) && str[1].Equals(si.name) && str[2].Equals(si.middle_name) && str[3].Equals(si.phone_num))
                        {
                            result += si_2.Return_subs_with_sep(si_2) + "\r\n";
                        }
                        else
                        {
                            result += strs + "\r\n";
                        }
                    }
                }
                File.WriteAllText(opf.FileName, result);
                Refresh_tab();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
