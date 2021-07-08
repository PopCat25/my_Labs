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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Net;
using System.IO;


namespace _2_Семестр_8_лабораторная
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            browser.Navigate("http://elibrary.ru");
        }


        private void Button_Click_Aftorization(object sender, RoutedEventArgs e)
        {
            try
            {
                Window1.Get_user_insert(out string login, out string password, out bool flag);
                if (login.Length == 0 || password.Length == 0)
                {
                    MessageBox.Show("одно из полей пусто");
                }
                else if (flag == false)
                {
                    MessageBox.Show("Авторизация отменена");
                }
                else
                {
                    IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    driver.Navigate().GoToUrl("http://elibrary.ru");
                    driver.Manage().Window.Maximize();

                    IWebElement elem = driver.FindElement(By.CssSelector("#login"));
                    elem.Click();

                    elem = driver.FindElement(By.CssSelector("#login"));
                    elem.SendKeys(login);

                    elem = driver.FindElement(By.CssSelector("#password"));
                    elem.Click();
                    elem.SendKeys(password);

                    elem = driver.FindElement(By.CssSelector("#win_login > table:nth-child(2) > tbody > tr:nth-child(9) > td > div.butred"));
                    elem.Click();
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            
            try
            {
                browser.GoBack();
            }
            catch (Exception err)
            {

            }
        }


        private void Button_Click_Forward(object sender, RoutedEventArgs e)
        {
            try
            {
                browser.GoForward();
            }
            catch(Exception err)
            {
             
            }
        }
        

        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            try
            {
                browser.Navigate("http://elibrary.ru");
            }
            catch (Exception err)
            {
            
            }
        }

        private void Button_Click_download_page(object sender, RoutedEventArgs e)
        {
            try
            {
                 WebClient web = new WebClient();
                 Random rnd = new Random();
                 File.WriteAllText($@"Page{rnd.Next()}.html", web.DownloadString("http://elibrary.ru"));

            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
    }
}



