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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2_Семестр_10_Лабораторная
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fon.Visibility == Visibility.Hidden)
            {
                fon.Visibility = Visibility.Visible;
                polyline1.Stroke = Brushes.White;
                polyline2.Stroke = Brushes.White;
                ellipse1.Stroke = Brushes.White;
                ellipse2.Stroke = Brushes.White;
                ellipse3.Stroke = Brushes.White;
                ellipse4.Stroke = Brushes.White;
                ellipse5.Stroke = Brushes.White;
                line1.Stroke = Brushes.White;
                line2.Stroke = Brushes.White;
                line3.Stroke = Brushes.White;
                line4.Stroke = Brushes.White;
                line5.Stroke = Brushes.White;
                line6.Stroke = Brushes.White;
                line7.Stroke = Brushes.White;
                line8.Stroke = Brushes.White;
                path1.Stroke = Brushes.White;
                path2.Stroke = Brushes.White;
                path3.Stroke = Brushes.White;
                path4.Stroke = Brushes.White;
            }
            else
            {
                fon.Visibility = Visibility.Hidden;
                polyline1.Stroke = Brushes.Black;
                polyline2.Stroke = Brushes.Black;
                ellipse1.Stroke = Brushes.Black;
                ellipse2.Stroke = Brushes.Black;
                ellipse3.Stroke = Brushes.Black;
                ellipse4.Stroke = Brushes.Black;
                ellipse5.Stroke = Brushes.Black;
                line1.Stroke = Brushes.Black;
                line2.Stroke = Brushes.Black;
                line3.Stroke = Brushes.Black;
                line4.Stroke = Brushes.Black;
                line5.Stroke = Brushes.Black;
                line6.Stroke = Brushes.Black;
                line7.Stroke = Brushes.Black;
                line8.Stroke = Brushes.Black;
                path1.Stroke = Brushes.Black;
                path2.Stroke = Brushes.Black;
                path3.Stroke = Brushes.Black;
                path4.Stroke = Brushes.Black;
            }
        }
    }
}
