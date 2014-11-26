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
 */

namespace myServer
{
    class Query
    {
        public string function;
        public Query()
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
