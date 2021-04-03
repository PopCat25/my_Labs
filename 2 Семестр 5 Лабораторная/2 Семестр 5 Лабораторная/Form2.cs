using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2_Семестр_5_Лабораторная
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        bool flag = false ;

        private void button1_Click(object sender, EventArgs e)
        {
            if (flag == true || textBox3.Text.Length == 0)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректная дата покупки");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DateTime dt = new DateTime();
            if( DateTime.TryParse(textBox3.Text,out dt) || textBox3.Text.Equals("*"))
            {
                label5.Visible = false;
                flag = true;
            }
            else
            {
                label5.Visible = true ;
                flag = false ;
            }
        }

        public _2_Семестр_5_Лабораторная.Work_with_data.Rec_info Ask (ref _2_Семестр_5_Лабораторная.Work_with_data.Rec_info ri, Form2 form)
         {
            form.ShowDialog();
            ri.marka = textBox1.Text;
            ri.gov_number = textBox2.Text;
            ri.buy_date = textBox3.Text;
            ri.fio_owner = textBox4.Text;
            return ri;
         }
    }
}
