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
 * реализует инетерфейс IComparable<User>, а именно функцию сравнения 
    
    Использует:
 * пока ничего
 
    Используется:
 * APIexecuter
 * HttpServer

    Поля:
 * public static int amount_users; - класс будет чекать кол-во созданных. И сам назначать Id при добавлении
    
    Свойства:
 * public string email
 * public string password
 * public int id
  
    Методы:
 * == != - логин и пароль совпали? 1 : 0 (не используется)
 * < > - сравнения по Id 1 : 0 (не используется)
 * public User(int idIn, string emailIn, string passwordIn) - инициализация
 * public User() - без него не читается из JSON
 * public override bool Equals(Object obj) - эквивалентны ли объекты? (не используется)
 * public string this[int index] = Мы можем по индексу вызывать свой-ства User. 0 = Email 1 = Password (не используется)
 */

namespace myServer
{
    class User 
    {

        public static int amount_users;

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {
        }

        public User( string emailIn, string passwordIn)
        {
            Id = amount_users++;
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
        // операторы 
        public static bool operator ==(User a, User b)
        {
            return (a.Email == b.Email && a.Password == b.Password) ? true : false;
        }
        public static bool operator !=(User a, User b)
        {
            return (a.Email == b.Email && a.Password == b.Password) ? false : true;
        }
        public static bool operator <(User a, User b)
        {
            return (a.Id < b.Id) ? true : false;
        }
        public static bool operator >(User a, User b)
        {
            return (a.Id > b.Id) ? true : false;
        }

        // зафигачим индексацию целочисленную 
        // PS можно также индексировать стрингом
        // 0 = email 1 = password
        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Email;
                    case 1: return Password;
                    default: throw new ArgumentOutOfRangeException("index", "Only index 0-1 valid!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: Email = value; break;
                    case 1: Password = value; break;
                    default: throw new ArgumentOutOfRangeException("index", "Only index 0-1 valid!");
                }
            }
        }

    }
}