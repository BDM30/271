﻿using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

/*
 MyMain:
 * класс для запуска программы
    Использует:
 * ConsoleManager
 * HttpServer
 */ 

namespace myServer {

    // запускаем главный класс HttpServer. Слушает клиентов и оправляет их обрабочику = HttpProcessor. Обрабочик принимает
    // клиента, а также указатель на класс сервера, чтобы вызывать POST и GET методы.
    public class MyMain {
        public static int Main(String[] args) {
            // некая инфа о машине
            ConsoleManager man = new ConsoleManager();
            man.printInfo();

            Console.WriteLine("\nWelcome to Server!\n");
            Console.Title = "Welcome to Server!"; 
            HttpServer httpServer = new HttpServer(11000);
            Thread thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();
            return 0;
        }

    }

}



