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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        public void Make_rec(ref Form1.subscribe_information si, ref Form2 form2, in OpenFileDialog opf)
        {
            form2.ShowDialog();
            
            si.surname = textBox1.Text;
            si.name = textBox2.Text;
            si.middle_name = textBox3.Text;
            si.phone_num = textBox4.Text;

            if ((si.Return_subs(si)).Length == 0)
            {
                MessageBox.Show("Все поля пусты, запись не произведена");
            }
            else
            {
                File.AppendAllText(opf.FileName, (si.Return_subs_with_sep(in si) + "\r\n"));
            }
        }

        public Form1.subscribe_information Ask(ref Form1.subscribe_information si, ref Form2 form2)
        {
            form2.ShowDialog();

            si.surname = textBox1.Text;
            si.name = textBox2.Text;
            si.middle_name = textBox3.Text;
            si.phone_num = textBox4.Text;

            return si;
        }
    }
}
