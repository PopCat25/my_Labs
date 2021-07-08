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

namespace _2_Семестр_8_лабораторная
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
       
        bool flag = false;

        public  static (string, string,bool) Get_user_insert(out string login, out string password,out bool returned_flag)
        {
            Window1 window = new Window1();
            window.ShowDialog();
            returned_flag = window.flag;
            login = window.user_insert1.Text;
            password = window.user_insert2.Text;
            return (login, password,returned_flag);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            flag = true;
            this.Close();
        }
    }
}
