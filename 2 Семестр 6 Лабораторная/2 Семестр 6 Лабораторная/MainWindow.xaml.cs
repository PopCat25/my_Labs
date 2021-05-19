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

        //Обработка нажатия кнопки "Поиск"
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                Work_witch_data.search_recs(in opf,ref myGrid);
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        //Обработка нажатия "Цена посылок"
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                Work_witch_data.calc_cost(in opf);
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
                        record.id = mass_for_split[5];
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
                Window1.return_data_insert(in opf ,out string result_str, out  bool  flag ); 
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

        //Из таблицы копируется значение выбранной строкии и приводится к экземпляру rec_form, файл удаляется и создаётся заново, варианты с совпадающим id не записываются
        public static void delete_rec (in OpenFileDialog opf, ref DataGrid myGrid)
        {
            try
            {
                var row_data = (rec_form)myGrid.SelectedItem;
                string[] file = File.ReadAllLines(opf.FileName);
                string[] parsed_row;
                File.Delete(opf.FileName);
                File.Create(opf.FileName).Close();
                for (int i = 0; file.Length != i; i++)
                {
                    parsed_row = file[i].Split('&');
                    if (row_data.id != parsed_row[5] )
                        File.AppendAllText(opf.FileName, file[i] + Environment.NewLine);
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        public static void search_recs (in OpenFileDialog opf, ref DataGrid myGrid)
        {
            string[] file = File.ReadAllLines(opf.FileName);
            string[] file_parsed;
            Window1.return_data_insert(in opf , out string result_string , out bool flag);
            string[] result_string_parsed = result_string.Split('&');
            List<rec_form> searh_list = new List<rec_form>();
            rec_form record = new rec_form();

            if(flag == true)
            {
                for (int i = 0; file.Length != i; i++)
                {
                    file_parsed = file[i].Split('&');
                    if (file_parsed[0].Contains(result_string_parsed[0]) || file_parsed[1].Contains(result_string_parsed[1]) || file_parsed[2].Contains(result_string_parsed[2]) || file_parsed[3].Contains(result_string_parsed[3]) || file_parsed[4].Contains(result_string_parsed[4]) )
                    {
                        record.send_number = file_parsed[0];
                        record.send_weight = file_parsed[1];
                        record.cost = file_parsed[2];
                        record.sending_date = file_parsed[3];
                        record.deliv_point = file_parsed[4];
                        record.id = file_parsed[5];
                        searh_list.Add(record);
                    }
                    myGrid.ItemsSource = null;
                    myGrid.ItemsSource = searh_list;
                }
            }
        }

        public static void calc_cost (in OpenFileDialog opf )
        {
            Window2.return_user_insert(out DateTime start, out DateTime finish, out bool flag);
            if (flag == true)
            {
                string[] file = File.ReadAllLines(opf.FileName);
                string[] split_row;
                int result_summ = 0 ;

                for (int i = 0; file.Length  != i; i++)
                {
                    split_row = file[i].Split('&');
                    if(DateTime.Compare(start,DateTime.Parse(split_row[3])) <= 0 && DateTime.Compare(finish,DateTime.Parse(split_row[3])) >= 0)
                    {
                        result_summ += int.Parse(split_row[2]);
                    }
                }
                MessageBox.Show($"В период с {start.ToShortDateString()} по {finish.ToShortDateString()} общая стоимость товаров равна:{result_summ}");
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
            public string id { get; set; }
        }
    }
}