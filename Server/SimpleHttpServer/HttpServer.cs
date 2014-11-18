using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

// HttpServer - основной класс для работы с клиентами
// int port - порт, с которым мы работаем
// TcpListener listener - средвтво поиска клиентов
// ConsoleManager consoleManager -  используем класс для работы с консолью
// HttpServer(int port) - конструктор ициализирует порт и consoleManager
// void listen() - слушаем клинтов и оправляем их в класс HttpProcessor
// handleGETRequest(HttpProcessor p) - обработка GET-запроса



namespace myServer
{
    public class HttpServer
    {
        protected int port;
        TcpListener listener;
        bool is_active = true;
        ConsoleManager consoleManager;

        public HttpServer(int port)
        {
            this.port = port;
            consoleManager = new ConsoleManager();
        }

        public void listen()
        {
            listener = new TcpListener(port);
            listener.Start();
            while (is_active)
            {
                TcpClient s = listener.AcceptTcpClient();
                consoleManager.playSoynds();
                consoleManager.printDate();
                HttpProcessor processor = new HttpProcessor(s, this);
                Thread thread = new Thread(new ThreadStart(processor.process));
                thread.Start();
                Thread.Sleep(1);
            }
        }
        
        public  void handleGETRequest(HttpProcessor p)
        {
            //Console.WriteLine("request: {0}", p.http_url);
            string query = (p.http_url).Trim(new Char[] { '/' });
            Console.WriteLine("q = {0}", query);
            AnswerServer ans = new AnswerServer();
            Console.WriteLine("correct query={0}", ans.isValidQuery(query));
            p.writeSuccess();
            p.outputStream.WriteLine(ans.getAnswer(query));
        }
    }
}
