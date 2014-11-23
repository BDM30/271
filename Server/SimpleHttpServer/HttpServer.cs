using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;

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

        Hashtable allUsers;
        Hashtable allNotes;
        // тут будем хранить кол-во пользователей и уведомлений
        // для выставления id
        private int amountUsers;
        private int amountNotes;


        public HttpServer(int port)
        {
            this.port = port;
            consoleManager = new ConsoleManager();
            amountNotes = 0;
            amountUsers = 0;
            allUsers = new Hashtable();
            allNotes = new Hashtable();
        }

        public void readUsers() {
            allUsers = new Hashtable();
            amountUsers = 0;

            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"allAcounts.txt");

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    // line - строка где 1 учетная запись

                    // убрали лишнюю точку с запятой
                    line = line.Substring(0, line.Length - 1);

                    // завели разделители
                    char[] charSeparatorsBlocks = new char[] { ';' };
                    char[] charSeparatorsNameValue = new char[] { '=' };
                    // получили массив блоков
                    string[] arrayBlocks = line.Split(charSeparatorsBlocks, StringSplitOptions.None);


                    string[] arrayNameValueId = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None);
                    string[] arrayNameValueEmail = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None);
                    string[] arrayNameValuePassword = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None);

                    User newGuy = new User(amountUsers++, arrayNameValueEmail[1], arrayNameValuePassword[1]);
                    allUsers.Add(arrayNameValueEmail[1], newGuy);
                    amountUsers++;
                }
            }
            file.Close();
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

        private void readNotes()
        {
                    allNotes = new Hashtable();
                    amountNotes = 0;

                Console.WriteLine("before read {0} notes", getAmountNotes());
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(@"allNotes.txt");
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        
                        // убрали лишнюю точку с запятой
                        line = line.Substring(0, line.Length - 1);
                        // завели разделители
                        char[] charSeparatorsBlocks = new char[] { ';' };
                        char[] charSeparatorsNameValue = new char[] { '=' };
                        // получили массив блоков
                        string[] arrayBlocks = line.Split(charSeparatorsBlocks, StringSplitOptions.None);

                        string id = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                        string name = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                        string owner = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                        string x = arrayBlocks[3].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                        string y = arrayBlocks[4].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];

                        Notification new_note = new Notification(name, owner, Convert.ToDouble(x), Convert.ToDouble(y), Convert.ToInt32(id));
                        amountNotes++;
                        // если есть какие-то данные в HT
                        if (allNotes.ContainsKey(owner))
                        {
                            List<Notification> list_notes = (List<Notification>)allNotes[owner]; 
                            list_notes.Add(new_note);
                            allNotes.Remove(owner);
                            allNotes.Add(owner, list_notes);
                        }
                        else
                        {
                            List<Notification> list_notes = new List<Notification>();
                            list_notes.Add(new_note);
                            allNotes.Add(owner, list_notes);
                            
                        }
                    }       
                }
                file.Close();
                Console.WriteLine("after read {0} notes", getAmountNotes());

        }

        private int getAmountNotes()
        {
            int res = 0;
            foreach (List<Notification> list_notes in allNotes.Values)
            {
                foreach (Notification x in list_notes)
                {
                    res++;
                }
            }
            return res;
        }
        
        public  void handleGETRequest(HttpProcessor p)
        {
            //Console.WriteLine("request: {0}", p.http_url);

            string query = (p.http_url).Trim(new Char[] { '/' });
            Console.WriteLine("q = {0}", query);
     
            readUsers();
            readNotes();

            AnswerServer ans = new AnswerServer(allNotes, allUsers);

            p.writeSuccess();
            p.outputStream.WriteLine(ans.getAnswer(query));
        }
    }
}
