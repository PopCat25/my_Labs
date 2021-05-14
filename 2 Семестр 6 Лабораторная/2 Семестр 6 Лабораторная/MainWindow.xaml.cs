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
using System.IO;
using Microsoft.Win32;

namespace _2_Семестр_6_Лабораторная
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

        OpenFileDialog opf = new OpenFileDialog();                      //Глобальная переменная пути к файлу с бд
        
        //Обработка нажатия кнопки "Выбор БД"
        private void Button_Click(object sender, RoutedEventArgs e)     
        {
            try
            {
                if (opf.ShowDialog() == true)
                {
                    MessageBox.Show($"Выбранный путь {opf.FileName}");
                    Work_witch_data.refresh_tab(in opf, ref myGrid);
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        //Обработка нажатия кнопки "Добавить запись"
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Work_witch_data.add_rec(in opf);
                Work_witch_data.refresh_tab(in opf, ref myGrid);
            }   
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        //обработка нажатия кнопки " Обновить "
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Work_witch_data.refresh_tab(in opf, ref myGrid);
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        //Обработка нажатия в контектсном меню "Удалить"
        private void Delete_DataGrid(object sender, RoutedEventArgs e)
        {
            if (myGrid.SelectedIndex != -1)
            {
                Work_witch_data.delete_rec(in opf, ref myGrid);
                Work_witch_data.refresh_tab(in opf, ref myGrid);
            }
            else
            {
                MessageBox.Show("Строка не выбрана");
            }
        }

        ////Обработка нажатия в контектсном меню "Изменить"
        private void Change_DataGrid(object sender, RoutedEventArgs e)
        {
            if (myGrid.SelectedIndex != -1)
            {
                if (Work_witch_data.add_rec(in opf) == true && myGrid.SelectedIndex != -1)
                {
                    Work_witch_data.delete_rec(in opf, ref myGrid);
                    Work_witch_data.refresh_tab(in opf, ref myGrid);
                }
                else
                {
                    MessageBox.Show("Изменения не совершены");
                }
            }
            else
            {
                MessageBox.Show("Строка не выбрана");
            }

        }

    }

    public static class Work_witch_data
    {
        //Считывание всей бд и обновление dataGrid
        public static void refresh_tab(in OpenFileDialog opf, ref DataGrid myGrid)
        {
            try
            {
                using (StreamReader sr = new StreamReader(opf.FileName))
                {
                    string str;
                    string[] mass_for_split;
                    rec_form record = new rec_form();
                    List<rec_form> rec_s = new List<rec_form>();
                    while (!sr.EndOfStream)
                    {
                        str = sr.ReadLine();
                        mass_for_split = str.Split('&');
                        record.send_number = mass_for_split[0];
                        record.send_weight = mass_for_split[1];
                        record.cost = mass_for_split[2];
                        record.sending_date = mass_for_split[3];
                        record.deliv_point = mass_for_split[4];
                        rec_s.Add(record);
                    }
                    myGrid.ItemsSource = null;
                    myGrid.ItemsSource = rec_s;
                }
            }
            catch(Exception err)
            {
               MessageBox.Show(err.Message);
            }
        }

        //создание и вызов окна ввода пользователя(window1), и запись ввода в бд, flag отвечает за предупреждение о том что окно ввода пользователь закрыл крестиком
        public static bool add_rec (in OpenFileDialog opf)
        {
            try
            {
                Window1.return_data_insert(out string result_str, out  bool  flag );
                if ( flag == true)
                {
                    File.AppendAllText(opf.FileName, result_str);
                    return flag;
                }
                return flag;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return false;
            }
        }

        public static void delete_rec (in OpenFileDialog opf, ref DataGrid myGrid)
        {
            try
            {
                int selected_Row = myGrid.SelectedIndex;
                string[] file = File.ReadAllLines(opf.FileName);
                File.Delete(opf.FileName);
                File.Create(opf.FileName).Close();
                for (int i = 0; file.Length != i; i++)
                {
                    if (selected_Row != i)
                        File.AppendAllText(opf.FileName, file[i] + Environment.NewLine);
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        // описание структуры одной записи в бд
        public struct rec_form
        {
            public string send_number { get; set; }
            public string send_weight { get; set; }
            public string cost { get; set; }
            public string sending_date { get; set; }
            public string deliv_point { get; set; }
        }
    }
}