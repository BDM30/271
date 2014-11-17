﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Collections;

// Класс для работы с пользователями. 
// allUsers - список всех пользователей в нашей базе. Хэш таблица : ключ = email(string) значение = obect User.
// amountUser - количество пользователей в базе
// loadFile() - загружает базу пользователей. ВАЖНО перед запуском нужно указать корректный адрес файла
// existUser(email, pass) - проверяет есть пользователь в базе. Возвращает bool
// existUser(email) - ищет пользователя по email и возращает его либо null
// saveFile() - сохраняет всех пользователей в файл. Перезаписывает. ВАЖНО перед запуском нужно указать корректный адрес файла
// addUser(log,pas) и addUser(id,log,pas) - добавить пользователя в базу


namespace myServer
{
    class UserBase
    {
        private List<User> userList = new List<User>();
        private Hashtable allUsers = new Hashtable();
        public UserBase()
        {
            amountUsers = 0;
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

        public bool existUser(string log)
        {
                return allUsers.ContainsKey(log) ? true : false;
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

        // сохраняем всех пользователей в файл allAcounts.txt (перезаписываем)
        public void saveFile()
        {
            ICollection valueCollection = allUsers.Values;
            List<string> lines = new List<string>();
            // добавлять в массив строк нужно
            foreach (User one in valueCollection)
            {
                lines.Add(one.ToString());
            }
            string[] slot = lines.ToArray();
            System.IO.File.WriteAllLines(@"allAcounts.txt", slot);
        }


        private int amountUsers;

        public void addUser(string log, string pas)
        {
            User newguy = new User(amountUsers++, log, pas);
            allUsers.Add(log, newguy);
        }
        public void addUser(int id, string log, string pas)
        {
            User newguy = new User(id, log, pas);
            allUsers.Add(log, newguy);
        }
    }
}