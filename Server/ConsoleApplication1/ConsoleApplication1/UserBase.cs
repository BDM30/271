using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

// Класс для работы с пользователями. 
// userList - список всех пользователей в нашей базе
// amountUser - количество пользователей в базе
// loadFile() - загружает базу пользователей. ВАЖНО перед запуском нужно указать корректный адрес файла
// existUser(email, pass) - проверяет есть пользователь в базе. Возвращает bool
// existUser(email) - ищет пользователя по email и возращает его либо null
// saveFile() - сохраняет всех пользователей в файл. Перезаписывает. ВАЖНО перед запуском нужно указать корректный адрес файла
// addUser(log,pas) и addUser(id,log,pas) - добавить пользователя в базу


namespace ConsoleApplication1
{
    class UserBase
    {
        private List<User> userList = new List<User>();
        public UserBase()
        {
            amountUsers = 0;
        }

        public bool remindPassword(string email)
        {
            User found = existUser(email);

            if (found == null)
                return false;

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

            //return false;

        }

        public void loadFile()
        {
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

                    addUser(arrayNameValueEmail[1], arrayNameValuePassword[1]);
                }
            }
            file.Close();
        }

        public User existUser(string log)
        {
            foreach (User one in userList)
            {
                if (one.getEmail() == log )
                    return one;
            }
            return null;
        }

        public bool existUser(string log, string pas)
        {
            foreach (User one in userList)
            {
                if (one.getEmail() == log && one.getPassword() == pas)
                    return true;
            }
            return false;
        }

        // сохраняем всех пользователей в файл allAcounts.txt (перезаписываем)
        public void saveFile()
        {

            List<string> lines = new List<string>();
            // добавлять в массив строк нужно
            foreach (User one in userList)
            {
                string result = "";
                result += "id=";
                result += (one.getId()).ToString();
                result += ";";
                result += "email=";
                result += (one.getEmail()).ToString();
                result += ";";
                result += "password=";
                result += (one.getPassword()).ToString();
                result += ";";
                //result += "\n";
                lines.Add(result);
            }
            string[] slot = lines.ToArray();
            //Console.WriteLine(slot[0]);
            //Console.WriteLine(slot[1]);
            // System.IO.File file = new File
            System.IO.File.WriteAllLines(@"allAcounts.txt", slot);
        }


        private int amountUsers;

        public void addUser(string log, string pas)
        {
            User newguy = new User(amountUsers++, log, pas);
            userList.Add(newguy);
            // for ex only
            //saveFile();
        }
        public void addUser(int id, string log, string pas)
        {
            User newguy = new User(id, log, pas);
            userList.Add(newguy);
            //saveFile();
        }
    }
}
