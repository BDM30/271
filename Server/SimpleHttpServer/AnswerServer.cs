using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Net.Mail;

/*
    AnswerServer:
 * класс для проверки запроса на корректность, а также его выполнения. Работает с API.
 * слишком большой нужно разделить его функциональность. Варинт : вынести проверку синтаксиса запроса в EmailValidator. Сам валидатор
 переименовать. Выполнение вынести в новый класс QueryExecuter.
 * Описание API в отдельном txt файле проекта
    
    Использует:
 * User
 * Notification
 * EmailValidator
    
    Используется:
 * HttpServer
    
    Атрибуты: 
 * private Hashtable allNotes - все напоминалки. Key = Owner's(User's) email | Value = List<Notification>
 * private Hashtable allUsers - все пользователи. Key = User's email | Value = User
 * private EmailValidator emailValidator
    
    Методы:
 ** более подробно см. описание API
 * public AnswerServer(Hashtable one, Hashtable two) - ициализация
 * public bool validAccountEntrance(string log, string pas) - пользователь с такой связкой существует в allUsers? 1 : 0
 * public bool remindPassword(string email) - отправить письмо, если есть кому.
 * public void saveInFileUsers() - заносим содержимое allUsers в allUsers.txt
 * public void saveInFileNotes() - заносим содержимое allNotes в allNotes.txt
 * private int getAmountNotes() - количество Notification в allNotes
 * public string getAnswer (string query) - главный метод. Который определяем запрос пользователя и исполняет его.
 * public bool isValidQuery(string query) - Проверка синтаксиса запроса
*/

namespace myServer
{
    class AnswerServer
    {
        private Hashtable allNotes;
        private Hashtable allUsers;

        private EmailValidator emailValidator;

        public AnswerServer(Hashtable one, Hashtable two)
        {
            allNotes = one;
            allUsers = two;
            emailValidator = new EmailValidator();
        }

        public bool validAccountEntrance(string log, string pas)
        {
            if (allUsers.ContainsKey(log))
            {
                User suspect = (User)allUsers[log];
                return (suspect.getEmail() == log && suspect.getPassword() == pas) ? true : false;
            }
            return false;
        }

        public bool remindPassword(string email)
        {
            if (!allUsers.ContainsKey(email))
                return false;
            User found = (User)allUsers[email];
            string smtpServer = "smtp.gmail.com";
            string from = "starson4587@gmail.com";
            string password = "djkoolherc";
            string mailto = email;
            string caption = "Восстановление пароля от " + email;
            string message = "Ваш пароль =" + found.getPassword();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        public void saveInFileUsers()
        {
            Console.WriteLine("AsnwerServer.saveInFileUsers()");
            ICollection valueCollection = allUsers.Values;
            List<string> lines = new List<string>();
            // добавлять в массив строк нужно
            foreach (User one in valueCollection)
            {
                lines.Add(one.ToString());
                // создать файл с именем id пользователя
            }
            string[] slot = lines.ToArray();
            System.IO.File.WriteAllLines(@"allAcounts.txt", slot);
            //System.IO.File.Create("notifications/"+".txt");
        }

        public void saveInFileNotes()
        {
            Console.WriteLine("AsnwerServer.saveInFileNotes()");
            List<string> lines = new List<string>();
            foreach (List<Notification> list_notes in allNotes.Values)
            {     
                foreach (Notification note in list_notes)
                {
                    lines.Add(note.ToString());
                }
               
              
            }
            string[] slot = lines.ToArray();
            System.IO.File.WriteAllLines(@"allNotes.txt", slot);
            Console.WriteLine("saved {0} notes",getAmountNotes());
        }

        private int getAmountNotes()
        {
            int res = 0;
            foreach(List<Notification> list_notes in allNotes.Values)
            {
                foreach (Notification x in list_notes)
                {
                    res++;
                }
            }
            return res;
        }

        public string getAnswer (string query) {
            // убрал точку с запятой с запроса. Это необходимо для дальнейшей обрабоки
            query = query.Substring(0, query.Length - 1);
            string answer = "default value";

            if (!isValidQuery(query))
            {
                return "wrong query";
            }
            // завели разделители, которыми будем парсить
            char[] charSeparatorsBlocks = new char[] { ';' };
            char[] charSeparatorsNameValue = new char[] { '=' };
            string[] arrayBlocks = query.Split(charSeparatorsBlocks, StringSplitOptions.None);

            // На этом этапе мы уверены, что наш запрос корректен, и теперь мы начинаем его обрабатывать
            string method = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];

            switch (method)
            {
                case "add_notification":
                    Console.WriteLine("add_notification!");
                    Console.WriteLine("amount before adding = {0}", getAmountNotes());
                    string in_name = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string in_user = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string in_x = arrayBlocks[3].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string in_y = arrayBlocks[4].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];

                    answer = "func=add_notification;";
                    if (allUsers.ContainsKey(in_user))
                    {
                        Notification newOne = new Notification(in_name, in_user, Convert.ToDouble(in_x), Convert.ToDouble(in_y), getAmountNotes());
                        List<Notification> noteList;
                        if (allNotes.ContainsKey(in_user))
                        {
                            noteList = (List<Notification>)allNotes[in_user];
                            allNotes.Remove(in_user);
                        }
                        else
                        {
                            noteList = new List<Notification>();
                        }
                        noteList.Add(newOne);
                        allNotes.Add(in_user, noteList);
                        saveInFileNotes();
                        answer += "result=1";
                    }
                    else
                    {
                        answer += "result=0";
                    }
                    break;
                case "registration":
                    Console.WriteLine("registration!");
                    string log_r = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string pas_r = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=registration;";

                    if ( !allUsers.ContainsKey(log_r) )
                    {
                        // проверка на валидность емаила
                        if (emailValidator.IsValidEmail(log_r))
                        {
                            User newGuy = new User(allUsers.Count, log_r, pas_r);
                            allUsers.Add(log_r, newGuy);
                            saveInFileUsers();
                            answer += "result=1;";
                        }
                        else
                        {
                            answer += "result=2;";
                        }
                    }
                    else
                    {
                        // ответ в случае если, такой же есть
                        answer += "result=0;";
                    }
                    break;

                case "entrance":
                    Console.WriteLine("entrance!");
                    string log_e = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string pas_e = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=entrance;";
                    if (validAccountEntrance(log_e, pas_e))
                                answer += "result=1;";
                            else
                                answer += "result=0;";
                    break;

                case "remind":
                    Console.WriteLine("remind!");
                    string log_re = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=remind;";
                    if (remindPassword(log_re))
                        answer += "result=1;";
                    else
                        answer += "result=0;";
                    break;
                case "get_notification":
                    Console.WriteLine("get_notification!");
                    string log_g = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=get_notification;";
                    if (allNotes.ContainsKey(log_g))
                    {
                        answer += "result=1;;;";
                        List<Notification> list_notes = (List<Notification>)allNotes[log_g];
                        foreach (Notification one in list_notes)
                        {
                            Console.WriteLine("new one");
                            answer += one.ToString()+";;";
                        }
                    }
                    else
                    {
                        answer += "result=0;";
                    }
                    
                    break;
            }

            return answer;
        }

        public bool isValidQuery(string query)
        {
            // завели разделители, которыми будем парсить
            char[] charSeparatorsBlocks = new char[] { ';' };
            char[] charSeparatorsNameValue = new char[] { '=' };

            // получили массив блоков. То, что находится между ';'.
            string[] arrayBlocks = query.Split(charSeparatorsBlocks, StringSplitOptions.None);
            switch (arrayBlocks.Length)
            {
                // добавление уведомления
                case 5:
                    // параметры
                    string func = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string name = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string user = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string x = arrayBlocks[3].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string y = arrayBlocks[4].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];

                    // значения
                    string add_notification = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];

                    // проверка параметров и значений
                    if ( !(     func == "func" &&
                                name == "name" &&
                                user == "user" &&
                                x == "x" &&
                                y == "y" ))
                    return false;

                    if ( !(add_notification == "add_notification") )
                        return false;
                    break;

                // вход и регистрация
                case 3:
                    // параметры
                    string func_3 = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string email = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string password = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    // значения
                    string registrationEntrance = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    // проверка параметров и значений
                    if (!(func_3 == "func" &&
                                email == "email" &&
                                password == "password"))
                    {
                        return false;
                    }
                    if (!(registrationEntrance == "registration" || registrationEntrance == "entrance"))
                    {
                        return false;
                    }
                    break;
                case 2:
                    string func_2 = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string email_2 = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string remindGetNote = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    if (!(func_2 == "func" &&
                                email_2 == "email"))
                        return false;
                    if (!(remindGetNote == "remind" || remindGetNote == "get_notification"))
                        return false;
                    break;
                default :
                    return false;
                   break;
            }

            return true;
        }
    }
}
