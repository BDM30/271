﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using System.Web.Helpers;
using System.Web;
using System.Threading.Tasks;

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

        private AnswerServer answerFromServer;
        private ConsoleManager consoleManager;

        // убрать потом.
        private bool is_active = true;


        public HttpServer(int port)
        {
            this.port = port;
            consoleManager = new ConsoleManager();
            allUsers = new Hashtable();
            allNotes = new Hashtable();
        }

        private  void readUsers() {
            Console.WriteLine("HttpServer.readUsers()");
            allUsers = new Hashtable();
            User.amount_users = 0;

            string line;
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"allUsers.txt");

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    User new_one = Json.Decode<User>(line);
                    new_one.Id = User.amount_users++; 
                    allUsers.Add(new_one.Email, new_one);
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
            Note.amount_notes = 0;

                allNotes = new Hashtable();
                Console.WriteLine("before read {0} notes", Note.amount_notes);
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(@"allNotes.txt");
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        Note new_one = Json.Decode<Note>(line);
                        new_one.Id = Note.amount_notes++;
                        // если есть какие-то данные в HT
                        if (allNotes.ContainsKey(new_one.Owner))
                        {
                            List<Note> list_notes = (List<Note>)allNotes[new_one.Owner]; 
                            list_notes.Add(new_one);
                            allNotes.Remove(new_one.Owner);
                            allNotes.Add(new_one.Owner, list_notes);
                        }
                        else
                        {
                            List<Note> list_notes = new List<Note>();
                            list_notes.Add(new_one);
                            allNotes.Add(new_one.Owner, list_notes);
                            
                        }
                    }       
                }
                file.Close();
                Console.WriteLine("after read {0} notes", Note.amount_notes);

        }
    
        public  void handleGETRequest(HttpProcessor p)
        {
            //Console.WriteLine("request: {0}", p.http_url);

            string query = (p.http_url).Trim(new Char[] { '/' });
            Console.WriteLine("q = {0}", HttpUtility.UrlDecode(query));

            Parallel.Invoke(
                () => readUsers(),
                () => readNotes()
            );
            

            answerFromServer = new AnswerServer(allNotes, allUsers);

            p.writeSuccess();
            p.outputStream.WriteLine(answerFromServer.getAnswer(query));
        }
    }
}
