using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Query:
 * класс для парсинга запросов к серверу. От него будут наследоваться конкретные запросы из API
    атрибуты:
 * public string function - название функции
 * public string result - возвращаемый результат
 
    EntranceQuery : Query
 * класс запроса входа
    атрибуты:
 * public string email;
 * public string password;
    
    RegistrationQuery : Query
 * класс запроса регистрации
    атрибуты:
 * public string email;
 * public string password;

    RemindQuery : Query
 * класс запроса восстановления пароля
    атрибуты:
 * public string email;
    
    AddNoteQuery : Query
 * класс запроса добавление напоминалки
    атрибуты:
 * public string name;
 * public string user;
 * public double x;
 * public double y;
    
    GetNotesQuery : Query 
 * класс запроса на получение всех напоминалок
    атрибуты:
 * public string email;

 */

namespace myServer
{
    class Query
    {
        public string function;
        public string result;
        public Query()
        {

        }
    }

    class GetNotesQuery : Query
    {
        public string email;
        public GetNotesQuery()
        {

        }
    }

    class AddNoteQuery : Query
    {
        public string name;
        public string user;
        public double x;
        public double y;
        public AddNoteQuery()
        {

        }
    }

    class RemindQuery : Query
    {
        public string email;
        public RemindQuery()
        {

        }  
    }

    class EntranceQuery : Query
    {
        public string email;
        public string password;
        public EntranceQuery()
        {

        }
    }

    class RegistrationQuery : Query
    {
        public string email;
        public string password;
        public RegistrationQuery()
        {

        }
    }
}
