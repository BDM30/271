using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Web.Helpers;

/*
    APIexecuter:
 * класс для исполнения запросов пользователя
 
    Использует:
 * User
 * Note
 * Validator
 
    Используется
 * AnswerServer
    
    Атрибуты:
 * private Hashtable allNotes; - Key = Owner's(User's) email | Value = List<Note>
 * private Hashtable allUsers; - Key = User's email | Value = User
 * private Validator validator - использует метод проверки email
    
    Методы:
 * public APIexecuter(ref Hashtable all_notes, ref Hashtable all_users) - инициализация
 * public List<Notes> get_notification (string log) - вовзращает все напоминалки Пользователя из allNotes
 * public string remind(string email) - отравляет письмо с паролем
    1 - есть аккаунт с таким логином
    0 - такого аккаунта нет
 * public string add_notification(string name, string user, string x, string y) - добавить напоминалку в allNotes
    1 - есть аккаунт с таким логином
    0 - такого аккаунта нет
 * public string entrance(string log, string pas) - поиск пары логин/пароль в allUsers
    1 - есть аккаунт с таким логином
    0 - такого аккаунта нет
    * private bool validAccountEntrance(string log, string pas) - логика метода entrace(log, pas)
 * public string registration(string log, string pas) - создать и добавить пользователя в allUsers 
    1 - верный email и такого у нас еще нет
    0 - такой email уже есть
    2 - email введен неверно
 * private void saveInFileNotes() - сохранить allNotes в allNotes.txt , формат хранения определен классом Note
 * private void saveInFileUsers() - сохранить allUsers в allUsers.txt , формат хранения определен классом User
*/

namespace myServer
{
    class APIexecuter
    {
        private Hashtable allNotes;
        private Hashtable allUsers;
        private Validator validator;

        public APIexecuter(ref Hashtable all_notes, ref Hashtable all_users)
        {
            allNotes = all_notes;
            allUsers = all_users;
            validator = new Validator();
        }

        public List<Note> get_notification(string log)
        {
            if (allNotes.ContainsKey(log))
            {
                List<Note> list_notes = (List<Note>)allNotes[log];
                return list_notes;
            }
            else
            {
                return new List<Note>();
            }
        }

        public string remind(string email)
        {
            if (!allUsers.ContainsKey(email))
                return "0";
            User found = (User)allUsers[email];
            string smtpServer = "smtp.gmail.com";
            string from = "starson4587@gmail.com";
            string password = "djkoolherc";
            string mailto = email;
            string caption = "Восстановление пароля от " + email;
            string message = "Ваш пароль =" + found.Password;
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
                return "1";
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        public string add_notification(string name, string user, double x, double y)
        {
            string res = "";
            if (allUsers.ContainsKey(user))
            {
                Note newOne = new Note(name, user, x, y);
                List<Note> noteList;
                if (allNotes.ContainsKey(user))
                {
                    noteList = (List<Note>)allNotes[user];
                    allNotes.Remove(user);
                }
                else
                {
                    noteList = new List<Note>();
                }
                noteList.Add(newOne);
                allNotes.Add(user, noteList);
                saveInFileNotes();
                res = "1";
            }
            else
            {
                res = "0";
            }
            return res;
        }

        public string entrance(string log, string pas)
        {
            return validAccountEntrance(log, pas) ? "1" : "0";
        }

        public string registration(string log, string pas)
        {
            string res = "";
            if (!allUsers.ContainsKey(log))
            {
                // проверка на валидность емаила
                if (validator.IsValidEmail(log))
                {
                    User newGuy = new User(log, pas);
                    allUsers.Add(log, newGuy);
                    saveInFileUsers();
                    res = "1";
                }
                else
                {
                    res ="2";
                }
            }
            else
            {
                // ответ в случае если, такой же есть
                res = "0";
            }
            return res;
        }

        private bool validAccountEntrance(string log, string pas)
        {
            if (allUsers.ContainsKey(log))
            {
                User suspect = (User)allUsers[log];
                return (suspect.Email == log && suspect.Password == pas) ? true : false;
            }
            return false;
        }

        private void saveInFileNotes()
        {
            Console.WriteLine("AsnwerServer.saveInFileNotes()");
            List<string> lines = new List<string>();
            foreach (List<Note> list_notes in allNotes.Values)
            {
                foreach (var note in list_notes)
                {
                    lines.Add(Json.Encode(note));
                }


            }
            string[] slot = lines.ToArray();
            System.IO.File.WriteAllLines(@"allNotes.txt", slot);
            Console.WriteLine("saved {0} notes", Note.amount_notes);
        }

        private void saveInFileUsers()
        {
            Console.WriteLine("AsnwerServer.saveInFileUsers()");
            ICollection valueCollection = allUsers.Values;
            List<string> lines = new List<string>();
            // добавлять в массив строк нужно
            foreach (var one in valueCollection)
            {
                lines.Add(Json.Encode(one));
                // создать файл с именем id пользователя
            }
            string[] slot = lines.ToArray();
            System.IO.File.WriteAllLines(@"allUsers.txt", slot);
        }
    }
}
