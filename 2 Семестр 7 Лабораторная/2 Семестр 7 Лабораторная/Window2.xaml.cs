using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace _2_Семестр_7_Лабораторная
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        public static void Show_send_history ()
        {
            Window2 window2 = new Window2();

            List<record_form> record_s = new List<record_form>();
            record_form rf = new record_form();


            try
            {
                string[] file_read = File.ReadAllLines(@"send_history.txt");

                for (int i = 0; i != file_read.Length; i++)
                {
                    rf.Имя_отправителя = file_read[i].Split('`')[0];
                    rf.Кому = file_read[i].Split('`')[1];
                    rf.Тема = file_read[i].Split('`')[2];
                    rf.Ssh = file_read[i].Split('`')[3];
                    rf.Html = file_read[i].Split('`')[4];
                    rf.Время = file_read[i].Split('`')[5];
                    record_s.Add(rf);
                }

                window2.Data.ItemsSource = record_s;
                window2.ShowDialog();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public static void add_rec (string sender_name, string to, string subject,  bool ssh, bool html, DateTime time) //window1.name.Text,window1.to.Text,window1.subject.Text,window1.body.Text, client.EnableSsl,message.IsBodyHtml,DateTime.UtcNow
        {
            File.AppendAllText(@"send_history.txt",$"{ sender_name}`{to}`{subject}`{ssh}`{html}`{time}{Environment.NewLine}") ;
        }

        struct record_form
        {
            public  string Имя_отправителя { get; set; }
            public string Кому { get; set; }
            public string Тема { get; set; }
            public string Ssh { get; set; }
            public string Html { get; set; }
            public string Время { get; set; }
        }
            

    }
}
