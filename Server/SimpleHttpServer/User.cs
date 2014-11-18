﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

// АТД Пользователь
// хранит id, email, password ,а также методы SET-GET
// public override bool Equals(Object obj) переопределили метод сравнения
// public override string ToString() переопределили метод преобразование в строку, для записи в файл.

namespace myServer
{
    class User
    {

        // коструктор
        public User(int idIn, string emailIn, string passwordIn)
        {
            id = idIn;
            email = emailIn;
            password = passwordIn;
        }
        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is User))
                return false;
            else
                return email == ((User)obj).email && password == ((User)obj).password;
        }
        public override string ToString()
        {
            string result = "";
            result += "id=";
            result += id.ToString();
            result += ";";
            result += "email=";
            result += email;
            result += ";";
            result += "password=";
            result += password;
            result += ";";
            return result;
        }
        // печать всех данных в консоль
        public void showData()
        {
            Console.WriteLine("id = {0}\nemail = {1}\npassword = {2}", id, email, password);
        }
        // set - методы
        public void setId(int idIn)
        {
            id = idIn;
        }
        public void setEmail(string emailIn)
        {
            email = emailIn;
        }
        public void setPassword(string passwordIn)
        {
            password = passwordIn;
        }
        // get - методы
        public int getId()
        {
            return id;
        }
        public string getEmail()
        {
            return email;
        }
        public string getPassword()
        {
            return password;
        }

        // переменные, хранящие данные
        private int id;
        private string email;
        private string password;
    }
}