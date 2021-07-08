using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace SocketTcpServer
{
    class Program
    {

        static List<string> name_list = new List<string> { };

        static int port = 8005; // порт для приема входящих запросов
        static string log = "";
        static void Main(string[] args)
        {

            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    string[] mes = builder.ToString().Split('$');

                    if (mes[0] == "message")
                    {
                        log += DateTime.Now.ToShortTimeString() + ": " + mes[1] + ": " + mes[2] + "\n";
                        Console.Clear();
                        Console.WriteLine(log);
                    }
                    if (mes[0] == "name")
                    {
                        if (!name_list.Contains(mes[1]))
                        {
                            name_list.Add(mes[1]);
                            log += DateTime.Now.ToShortTimeString() + ": К нам подключился: " + mes[1] + "\n";
                            Console.Clear();
                            Console.WriteLine(log);
                        }
                        else
                        {
                            // отправляем ответ
                            data = Encoding.Unicode.GetBytes("ERROR");
                            handler.Send(data);
                            // закрываем сокет
                            handler.Shutdown(SocketShutdown.Both);
                            handler.Close();
                            continue;
                        }
                    }

                    // отправляем ответ
                    data = Encoding.Unicode.GetBytes(log);
                    handler.Send(data);
                    // закрываем сокет
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}