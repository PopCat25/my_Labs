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
using System.Net;
using System.Net.Mail;
using Microsoft.Win32;
using System.IO;

namespace _2_Семестр_7_Лабораторная
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }


        OpenFileDialog opf = new OpenFileDialog ();
        bool flag = false;
        bool html_flag = false ;

        public static void Send_letter()
        {
            Window1 window1 = new Window1();
            window1.ShowDialog();

            if (window1.flag == true)
            {
                MailAddress m_from = new MailAddress("alexeylinskijl@yandex.ru", window1.name.Text);
                MailAddress m_to = new MailAddress(window1.to.Text);
                MailMessage message = new MailMessage(m_from, m_to);
                message.Subject = window1.subject.Text;
                message.Body = window1.body.Text;
                if (File.Exists(window1.opf.FileName))
                    message.Attachments.Add(new Attachment(window1.opf.FileName));
                if (window1.html_flag == true)
                    message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
                client.Credentials = new NetworkCredential("alexeylinskijl@yandex.ru", "dkrdrjbcpasdzxmo");  //dkrdrjbcpasdzxmo - пароль для приложения яндекс
                client.EnableSsl = true;
                client.Send(message);
                Window2.add_rec(window1.name.Text,window1.to.Text,window1.subject.Text, client.EnableSsl,message.IsBodyHtml,DateTime.UtcNow);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(to.Text.Contains("@") && (to.Text.Contains(".ru") || to.Text.Contains(".com")) && subject.Text.Length != 0 && body.Text.Length != 0 )
            {
                flag = true;
                MessageBox.Show("done!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            opf.ShowDialog();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            html_flag = true;
        }
    }

}
