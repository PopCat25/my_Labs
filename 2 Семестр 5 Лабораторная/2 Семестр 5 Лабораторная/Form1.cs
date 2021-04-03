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

namespace _2_Семестр_5_Лабораторная
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog opf = new OpenFileDialog();


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"Выбранный путь: {opf.FileName}");
                    Work_with_data.Refresh_tab(in opf, ref dataGridView1);
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                Work_with_data.Add_rec(in opf);
                Work_with_data.Refresh_tab(in opf, ref dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
                Work_with_data.Change_rec(in opf);
                Work_with_data.Refresh_tab(in opf, ref dataGridView1);        }

        private void button4_Click(object sender, EventArgs e)
        {
            Work_with_data.Find_rec(in opf, dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Work_with_data.Refresh_tab(in opf,ref dataGridView1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Work_with_data.Delete_rec(in opf);
            Work_with_data.Refresh_tab(in opf,ref dataGridView1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Work_with_data.Year_calc(ref dataGridView1);
        }

        private void MyFunc(object sender, DataGridViewCellEventArgs e)
        {
            
            MessageBox.Show($"Строка: {e.RowIndex} Колонка: {e.ColumnIndex}");
        }
    }

    public static class Work_with_data
    {

        static public void Refresh_tab(in OpenFileDialog opf, ref DataGridView dataGridView1)
        {
            try
            {
                dataGridView1.Rows.Clear();
                string[] mass_for_pars;

                using (StreamReader sr = new StreamReader(opf.FileName))
                {
                    for (int i = 0; !sr.EndOfStream; i++)
                    {
                        dataGridView1.RowCount += 2;
                        mass_for_pars = sr.ReadLine().Split('`');
                        dataGridView1[0, i].Value = mass_for_pars[0];
                        dataGridView1[1, i].Value = mass_for_pars[1];
                        dataGridView1[2, i].Value = mass_for_pars[2];
                        dataGridView1[3, i].Value = mass_for_pars[3];
                        dataGridView1.RowCount -= 1;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        static public void Add_rec(in OpenFileDialog opf)
        {
            try
            {
                Rec_info ri = new Rec_info();
                Form2 form = new Form2();
                form.Ask(ref ri, form);
                if (ri.return_without_sep(ri).Length != 0)
                {
                    MessageBox.Show($"Добавлена запись: {ri.return_without_sep(in ri)}");
                    File.AppendAllText(opf.FileName, ri.return_with_sep(in ri) + Environment.NewLine);
                }
                else
                {
                    MessageBox.Show("Ввод пустой, запись не произведена");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        public static void Delete_rec(in OpenFileDialog opf)
        {
            try
            {
                Rec_info ri = new Rec_info();
                Form2 form2 = new Form2();
                form2.label6.Visible = true;
                form2.Ask(ref ri, form2);

                string result = "";
                using (StreamReader sr = new StreamReader(opf.FileName))
                {

                    string[] str;
                    string strs;

                    while (!sr.EndOfStream)
                    {
                        strs = sr.ReadLine();
                        str = strs.Split('`');
                        if (!((str[0].Equals(ri.marka) || ri.marka.Equals("*")) && (str[1].Equals(ri.gov_number) || ri.gov_number.Equals("*")) && (str[2].Equals(ri.buy_date) || ri.buy_date.Equals("*")) && (str[3].Equals(ri.fio_owner) || ri.fio_owner.Equals("*"))))
                        {
                            result += strs + Environment.NewLine;
                        }
                    }
                }
                File.WriteAllText(opf.FileName, result);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        static public void Change_rec(in OpenFileDialog opf)
        {
            try
            {
                Rec_info ri = new Rec_info();
                Rec_info ri_2 = new Rec_info();
                Form2 form2 = new Form2();
                Form2 form2_2 = new Form2();
                form2_2.Text = "Введите изменение";
                form2.Ask(ref ri, form2);
                form2_2.Ask(ref ri_2, form2_2);

                string result = "";
                using (StreamReader sr = new StreamReader(opf.FileName))
                {

                    string[] str;
                    string strs;

                    while (!sr.EndOfStream)
                    {
                        strs = sr.ReadLine();
                        str = strs.Split('`');
                        if (str[0].Equals(ri.marka) && str[1].Equals(ri.gov_number) && str[2].Equals(ri.buy_date) && str[3].Equals(ri.fio_owner))
                        {
                            result += ri_2.return_with_sep(ri_2) + Environment.NewLine;
                        }
                        else
                        {
                            result += strs + Environment.NewLine;
                        }
                    }
                }
                File.WriteAllText(opf.FileName, result);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        static public void Find_rec(in OpenFileDialog opf, DataGridView dataGridView1)
        {
            try
            {
                Rec_info ri = new Rec_info();
                Form2 form2 = new Form2();
                form2.Ask(ref ri, form2);

                if (ri.return_without_sep(ri).Length != 0)
                {
                    dataGridView1.Rows.Clear();
                    using (StreamReader sr = new StreamReader(opf.FileName))
                    {

                        string[] str = new string[4];

                        for (int i = 0; !sr.EndOfStream;)
                        {
                            str = sr.ReadLine().Split('`');
                            if ((str[0].Contains(ri.marka) && ri.marka.Length != 0) || (str[1].Contains(ri.gov_number) && ri.gov_number.Length != 0) || (str[2].Contains(ri.buy_date) && ri.buy_date.Length != 0) || (str[3].Contains(ri.fio_owner) && ri.fio_owner.Length != 0))
                            {
                                dataGridView1.RowCount += 2;
                                dataGridView1[0, i].Value = str[0];
                                dataGridView1[1, i].Value = str[1];
                                dataGridView1[2, i].Value = str[2];
                                dataGridView1[3, i].Value = str[3];
                                dataGridView1.RowCount -= 1;
                                i++;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Задан пустой запрос");
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public static void Year_calc(ref DataGridView dataGridView)
        {   
            try
            {
                DateTime date = new DateTime();
                for(int i = 0 ; i < dataGridView.Rows.Count - 1  ;i++)
                {
                    if (DateTime.TryParse(dataGridView[2, i].Value.ToString(), out date))
                    {
                        dataGridView[4, i].Value =  $"{(DateTime.Now - date).Days} Дней" ;
                    }

                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public struct Rec_info
        {
            public string marka;
            public string gov_number;
            public string buy_date;
            public string fio_owner;

            public  string return_with_sep(in Rec_info info)
            {
                return info.marka + '`' + info.gov_number + '`' + info.buy_date + '`' + info.fio_owner;
            }

            public  string return_without_sep(in Rec_info info)
            {
                return info.marka + info.gov_number  + info.buy_date  + info.fio_owner;
            }
        }
    }
}
