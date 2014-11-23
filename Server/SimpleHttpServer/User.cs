﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

/*
    User:
 * класс представляющий абстракцию пользователь. 
 * позже должен быть заменен или дополнен таблицей в соответсвующей БД.
 * должен быть аналогичен классу Notification
    
    Использует:
 * пока ничего
 
    Используется:
 * AnswerServer
 * HttpServer
    
    Атрибуты:
 * private string email
 * private string password
 * private int id
  
    Методы:
 * == - логин и пароль совпали? 1 : 0
 * public User(int idIn, string emailIn, string passwordIn) - инициализация
 * public int  getId()
 * public string getEmail()
 * public string getPassword()
 * public void setId(int idIn)
 * public void setEmail(string emailIn)
 * public void setPassword(string passwordIn)
 * public override string ToString() - на выходе строка в формате хранения в файле и оправки клиенту.
 * 
 */

namespace myServer
{
    class User
    {

        private int id;
        private string email;
        private string password;

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

    }
}