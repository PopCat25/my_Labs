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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public  partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckGood_send_number.Visibility == Visibility.Visible && CheckGood_send_weight.Visibility ==  Visibility.Visible && CheckGood_cost.Visibility == Visibility.Visible && CheckGood_sending_date.Visibility == Visibility.Visible && CheckGood_deliv_point.Visibility == Visibility.Visible) 
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректный ввод");
            }
        }


        private void send_number_textChanged(object sender, TextChangedEventArgs e)
        {
            if(send_number.Text.Length > 0)
            {
                CheckGood_send_number.Visibility = Visibility.Visible;
                CheckBad_send_number.Visibility = Visibility.Hidden;

            }
            else
            {
                CheckGood_send_number.Visibility = Visibility.Hidden;
                CheckBad_send_number.Visibility = Visibility.Visible;   
            }
        }

        private void send_weight_TextChanged(object sender, TextChangedEventArgs e)
        {
            double i;
            if(double.TryParse(send_weight.Text ,out i) && i > 0 )
            {
                CheckGood_send_weight.Visibility = Visibility.Visible;
                CheckBad_send_weight.Visibility = Visibility.Hidden;
            }
            else
            {
                CheckGood_send_weight.Visibility = Visibility.Hidden;
                CheckBad_send_weight.Visibility = Visibility.Visible;
            }
        }

        private void cost_TextChanged(object sender, TextChangedEventArgs e)
        {
            double i;
            if(double.TryParse(cost.Text, out i)&& i > 0 )
            {
                CheckGood_cost.Visibility = Visibility.Visible;
                CheckBad_cost.Visibility = Visibility.Hidden;
            }
            else
            {
                CheckGood_cost.Visibility = Visibility.Hidden;
                CheckBad_cost.Visibility = Visibility.Visible;
            }
        }


        private void sending_date_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dt = new DateTime();
            if(DateTime.TryParse(sending_date.Text ,out dt ))
            {
                CheckGood_sending_date.Visibility = Visibility.Visible;
                CheckBad_sending_date.Visibility = Visibility.Hidden;
            }
            else
            {
                CheckGood_sending_date.Visibility = Visibility.Hidden;
                CheckBad_sending_date.Visibility = Visibility.Visible;
            }
        }



        private void deliv_point_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (deliv_point.Text.Length > 0)
            {
                CheckGood_deliv_point.Visibility = Visibility.Visible;
                CheckBad_deliv_point.Visibility = Visibility.Hidden;
            }
            else
            {
                CheckGood_deliv_point.Visibility = Visibility.Hidden;
                CheckBad_deliv_point.Visibility = Visibility.Visible;
            }
        } 

        public static (string, bool) return_data_insert(out string result_str, out bool flag )
        {
            Window1 window = new Window1();
            window.ShowDialog();
            flag = false;
            if (window.CheckGood_send_number.Visibility == Visibility.Visible && window.CheckGood_send_weight.Visibility == Visibility.Visible && window.CheckGood_cost.Visibility == Visibility.Visible && window.CheckGood_sending_date.Visibility == Visibility.Visible && window.CheckGood_deliv_point.Visibility == Visibility.Visible)
            { 
                flag = true;
                window.sending_date.Text = DateTime.Parse(window.sending_date.Text).ToShortDateString(); //приводит ввод пользователя к стандарту облегчая поиск
            }
            result_str = $"{window.send_number.Text}&{window.send_weight.Text}&{window.cost.Text}&{window.sending_date.Text}&{window.deliv_point.Text}{Environment.NewLine}";
            return (result_str,flag);
        }
    }
}
