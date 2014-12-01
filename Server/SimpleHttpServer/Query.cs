using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Query:
 * Все запросы к северу и ответы от него в формате JSON. Этот класс нужен для создания результата и парсинга запросов.
 * От него наследуются более конкретные классы.
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
 
    GetNotesAnswer : Query
 * класс для вывода всех напоминалок
    атрибуты:
 * public List<Note> notes - храним все напоминалки пользователя

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

    class GetNotesAnswer : Query
    {   
        public GetNotesAnswer()
        {
        }
        public List<Note> notes;
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
