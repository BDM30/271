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
 * должен быть аналогичен классу Note
    
    Использует:
 * пока ничего
 
    Используется:
 * APIexecuter
 * HttpServer
    
    Атрибуты:
 * public string email
 * public string password
 * public int id
  
    Методы:
 * == - логин и пароль совпали? 1 : 0
 * public User(int idIn, string emailIn, string passwordIn) - инициализация
 * public User() - без него не читается из JSON
 */

namespace myServer
{
    class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(int idIn, string emailIn, string passwordIn)
        {
            Id = idIn;
            Email = emailIn;
            Password = passwordIn;
        }
        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is User))
                return false;
            else
                return Email == ((User)obj).Email && Password == ((User)obj).Password;
        }
    }
}