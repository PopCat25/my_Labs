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

namespace _2_Семестр_6_Лабораторная
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(CheckGood_PO.Visibility == Visibility.Visible && CheckGood_S.Visibility == Visibility.Visible )
            {
                this.Close();
            }
           else
            {
                MessageBox.Show("Некорректный ввод");
            }
        }

        private void S_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dt;
            if(DateTime.TryParse(S.Text,out dt))
            {
                CheckGood_S.Visibility = Visibility.Visible;
                CheckBad_S.Visibility = Visibility.Hidden;
            }
            else
            {
                CheckGood_S.Visibility = Visibility.Hidden;
                CheckBad_S.Visibility = Visibility.Visible;
            }
        }

        private void PO_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParse(PO.Text,out dt))
            {
                CheckGood_PO.Visibility = Visibility.Visible;
                CheckBad_PO.Visibility = Visibility.Hidden;
            }
            else
            {
                CheckGood_PO.Visibility = Visibility.Hidden;
                CheckBad_PO.Visibility = Visibility.Visible;
            }
        }

        static public (DateTime,DateTime,bool) return_user_insert (out DateTime start, out DateTime finish, out bool flag)
        {
            Window2 window2 = new Window2();
            window2.ShowDialog();
            flag = false;
            if(window2.CheckGood_PO.Visibility == Visibility.Visible && window2.CheckGood_S.Visibility == Visibility.Visible)
            {
                flag = true;
            }
            start = DateTime.Parse(window2.S.Text);
            finish = DateTime.Parse(window2.PO.Text);
            return (start,finish,flag);
        }
    }
}
