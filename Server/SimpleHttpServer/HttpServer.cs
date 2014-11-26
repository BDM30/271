using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using System.Web.Helpers;

/*
    HttpServer:
 * основной класс для работы с клиентами

    Используется:
 * MyMain
 * HttpProcessor
 
    Использует: 
 * AnswerServer
 * ConsoleManager
 * User
 * Note


    Атрибуты:
 * private int port - порт, с которым мы работаем
 * private TcpListener listener - средство поиска клиентов
 * private HashTable allUsers - хранит всех пользователей. Key = User's email | Value = User
 * private HashTable allNotes - хранит все напоминалки. Key = Owner's(User's) email | Value = List<Note>
 * private int amountNotes - счетчик имеющихся напоминалок
 * private int amountUsers - счетчик имеющихся пользователей
 * private ConsoleManager consoleManager -  используем класс для работы с консолью
 * private AnswerServer answerFromServer - используем класс для получения ответа


    Методы:
 * public HttpServer(int port) - конструктор ициализирует порт и consoleManager
 * public void listen() - слушаем клинтов и оправляем их в класс HttpProcessor
 * public handleGETRequest(HttpProcessor p) - первчиная бработка GET-запроса, чтобы получить текст запроса . Эти первые методы необходимо переделать.

 * private getAmountNotes() - получаем кол-во напоминалок в allNotes
 * private readNotes() - считывает напоминалки из AllNotes.txt . И добавляет их в allNotes. Обнуляет allNotes и amountNotes.
 * private readUsers() - считывает пользователей из AllUsers.txt . И добавляет их в allUsers. Обнуляет allUsers и amountUsers.
 */



namespace myServer
{
    public class HttpServer
    {
        private int port;
        private TcpListener listener;
        private Hashtable allUsers;
        private Hashtable allNotes;
        private int amountUsers;
        private int amountNotes;

        private AnswerServer answerFromServer;
        private ConsoleManager consoleManager;

        // убрать потом.
        private bool is_active = true;


        public HttpServer(int port)
        {
            this.port = port;
            consoleManager = new ConsoleManager();
            amountNotes = 0;
            amountUsers = 0;
            allUsers = new Hashtable();
            allNotes = new Hashtable();
        }

        private  void readUsers() {
            allUsers = new Hashtable();
            amountUsers = 0;

            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"allUsers.txt");

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    User new_one = Json.Decode<User>(line);
                    new_one.id = amountUsers++; 
                    allUsers.Add(new_one.email, new_one);
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
                        Note new_one = Json.Decode<Note>(line);
                        new_one.id = amountNotes++;
                        // если есть какие-то данные в HT
                        if (allNotes.ContainsKey(new_one.owner))
                        {
                            List<Note> list_notes = (List<Note>)allNotes[new_one.owner]; 
                            list_notes.Add(new_one);
                            allNotes.Remove(new_one.owner);
                            allNotes.Add(new_one.owner, list_notes);
                        }
                        else
                        {
                            List<Note> list_notes = new List<Note>();
                            list_notes.Add(new_one);
                            allNotes.Add(new_one.owner, list_notes);
                            
                        }
                    }       
                }
                file.Close();
                Console.WriteLine("after read {0} notes", getAmountNotes());

        }

        private int getAmountNotes()
        {
            int res = 0;
            foreach (List<Note> list_notes in allNotes.Values)
            {
                foreach (Note x in list_notes)
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

            answerFromServer = new AnswerServer(allNotes, allUsers);

            p.writeSuccess();
            p.outputStream.WriteLine(answerFromServer.getAnswer(query));
        }
    }
}
