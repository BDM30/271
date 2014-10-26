using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Introduction
            Console.WriteLine("Welcome to Server!\n");
            Console.Title = "Welcome to Server!";
            
            


            ConsoleManager consoleManager = new ConsoleManager();
            // setColor(int number) , установить 1 из 16 цветов по номеру
            // printInfo() = вывести информацию об испольняющей системе
            // playSound() = издать несколько звуков
            // printData() = вывести h:m:s
  
            TcpListener server = null; // TcpListener
            int MaxThreadsCount = Environment.ProcessorCount * 4; // Количество потоков
            Console.WriteLine(MaxThreadsCount.ToString()); // вывели его
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount); // установили
            ThreadPool.SetMinThreads(2, 2); // установили минимальное количество
            Int32 port = 11000; // выбрали порт
            int amountConnections = 0;
            Parser parser = new Parser(); // класс который буд

            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                while (true)
                {
                    Console.Write("\nWaiting for a connection... ");
                    ThreadPool.QueueUserWorkItem(parser.showText, server.AcceptTcpClient());
                    amountConnections++;
                    Console.Write("\nConnection " + amountConnections.ToString() + "!");
                    

                }

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
