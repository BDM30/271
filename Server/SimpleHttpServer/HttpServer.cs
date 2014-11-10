using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace myServer
{
    public abstract class HttpServer
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

        public abstract void handleGETRequest(HttpProcessor p);
    }

    public class MyHttpServer : HttpServer
    {
        public MyHttpServer(int port)
            : base(port)
        {
        }
        public override void handleGETRequest(HttpProcessor p)
        {
            Console.WriteLine("request: {0}", p.http_url);
            string query = (p.http_url).Trim(new Char[] { '/' });
            Console.WriteLine("q = {0}", query);
            string answer = "defaut value";

            //
            // Вызвали базу пользователей
            UserBase userBase = new UserBase();
            userBase.loadFile(); // Загрузили имеющихся пользователей

            // убрал точку с запятой с запроса. Это необходимо для дальнейшей обрабоки
            query = query.Substring(0, query.Length - 1);



            // завели разделители, которыми будем парсить
            char[] charSeparatorsBlocks = new char[] { ';' };
            char[] charSeparatorsNameValue = new char[] { '=' };

            // получили массив блоков. То, что находится между ';'.
            string[] arrayBlocks = query.Split(charSeparatorsBlocks, StringSplitOptions.None);


            // Парсим запрос
            switch (arrayBlocks.Length)
            {
                // случай регистрации и входа
                case 3:
                    // Получили массив из 2х элементов. В виде ключ = значение.
                    string[] arrayNameValueMethod = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None);
                    string[] arrayNameValueEmail = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None);
                    string[] arrayNameValuePassword = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None);

                    // управление фукциями
                    switch (arrayNameValueMethod[1])
                    {
                        case "registration":
                            answer = "func=registration;";
                            User found = userBase.existUser(arrayNameValueEmail[1]);
                            if (found == null)
                            {
                                userBase.addUser(arrayNameValueEmail[1], arrayNameValuePassword[1]);
                                userBase.saveFile();
                                answer += "result=1;";
                            }
                            else
                                answer += "result=0;";
                            break;
                        case "entrace":
                            Console.WriteLine("entrace!");
                            answer = "func=entace;";
                            if (userBase.existUser(arrayNameValueEmail[1], arrayNameValuePassword[1]))
                                answer += "result=1;";
                            else
                                answer += "result=0;";
                            break;
                        default:
                            answer = "wrong query";
                            break;

                    }
                    break;

                case 2:
                    string[] arrayNameValueMethod2 = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None);
                    string[] arrayNameValueEmail2 = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None);
                    switch (arrayNameValueMethod2[1])
                    {
                        case "remind":
                            Console.WriteLine("remind!");
                            answer = "func=remind;";
                            if (userBase.remindPassword(arrayNameValueEmail2[1]))
                                answer += "result=1;";
                            else
                                answer += "result=0;";
                            break;
                        default:
                            answer = "wrong query";
                            break;
                    }
                    break;
// Только для нужд Егор. Потом нахер удалить
//////////////////////////////////////////////////////////////////////////////////////////////////////////
                case 1:
                    switch (arrayBlocks[0])
                    {
                        case "test1" :
                            answer = "devtest2;";
                            break;
                        case "test2" :
                            answer = "devtest2;";
                            break;
                        default :
                            answer = "wrong query";
                            break;
                    }
                    Console.WriteLine("test query:={0}", arrayBlocks[0]);
                    break;
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                default:
                    answer = "wrong query";
                    break;

            }
            p.writeSuccess();
            p.outputStream.WriteLine(answer);
        }
    }
}
