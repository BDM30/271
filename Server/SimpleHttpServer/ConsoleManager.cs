﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    ConsoleManager:
 * класс для управления консолью 
 * нужно пересмотреть метод сменить шрифт
 
    Использует:
 * пока ничего
 
    Используется:
 * HttpServer
 * MyMain
  
    Атрибуты:
 * пока ничего
    
    Методы:
 * public ConsoleManager() - не делает ничего
 * public void playSounds() - проигрывает системные звуки
 * public void printData() - выводит текущее время на сервере
 * public void printInfo - выводит технические характеристики машины, на который запущен сервер()
*/

namespace myServer
{
    class ConsoleManager
    {
        public ConsoleManager()
        {

        }

        public void playSoynds()
        {
            for (int i = 100; i <= 1000; i += 100)
            {
                Console.Beep(i, 200);
            }
        }

        public void printDate()
        {
            DateTime time = new DateTime();
            time.ToLocalTime();
            Console.WriteLine("current time: {0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        public void printInfo()
        {
            Console.WriteLine("Information about your system");
            foreach (string drive in Environment.GetLogicalDrives())
                Console.WriteLine("Drive: {0}", drive); // диски 
            Console.WriteLine("OS: {0}", Environment.OSVersion); // ОС 
            Console.WriteLine("Number of processors: {0}",
            Environment.ProcessorCount); // количество процессоров 
            Console.WriteLine(".NET Version: {0}",
            Environment.Version);
            Console.WriteLine("Machine Name: {0}", Environment.MachineName);
            Console.WriteLine("User Name: {0}", Environment.UserName);
        }
    }
}