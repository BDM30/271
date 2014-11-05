﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// класс для управления консолью
// setColor - меняет цвет текста консоли. Принимает на вход число от 0 до 15. ВРЕМЕННО НЕ РАБОТАЕТ!
// playSounds - проигрывает системные звуки
// printData - выводит текущее время на сервере
// printInfo - выводит технические характеристики машины, на который запущен сервер

namespace myServer
{
    class ConsoleManager
    {
        public void setColor(int number)
        {
            String[] colorNames = ConsoleColor.GetNames(typeof(ConsoleColor));
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorNames[number]);
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