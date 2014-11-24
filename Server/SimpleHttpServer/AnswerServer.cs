using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;



/*
    AnswerServer:
 * Определяет какой метод, хочет пользователя и выдает результат запроса.
 * Описание API в отдельном txt файле проекта
    
    Использует:
 * EmailValidator
 * APIexecutor
    
    Используется:
 * HttpServer
    
    Атрибуты: 
 * private Validator validator - используем для проверки запросов на корректность
 * private API api - используем для выполнения запросов
    
    Методы:
 ** более подробно см. описание API
 * public AnswerServer(Hashtable one, Hashtable two) - ициализация
 * public string getAnswer (string query) - главный метод. Который определяем запрос пользователя и исполняет его.
*/

namespace myServer
{
    class AnswerServer
    {
        private Validator validator;
        private APIexecuter api;

        public AnswerServer(Hashtable one, Hashtable two)
        {
            validator = new Validator();
            api = new APIexecuter(ref one, ref two);
        }
        
        public string getAnswer (string query) {
            // убрал точку с запятой с запроса. Это необходимо для дальнейшей обрабоки
            query = query.Substring(0, query.Length - 1);
            string answer = "default value";

            if (!validator.isValidQuery(query))
            {
                return "wrong query";
            }
            // завели разделители, которыми будем парсить
            char[] charSeparatorsBlocks = new char[] { ';' };
            char[] charSeparatorsNameValue = new char[] { '=' };
            string[] arrayBlocks = query.Split(charSeparatorsBlocks, StringSplitOptions.None);

            // На этом этапе мы уверены, что наш запрос корректен, и теперь мы начинаем его обрабатывать
            string method = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];

            switch (method)
            {
                case "add_notification":
                    Console.WriteLine("add_notification!");
                    string in_name = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string in_user = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string in_x = arrayBlocks[3].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string in_y = arrayBlocks[4].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=add_notification;result=" + api.add_notification(in_name,in_user,in_x, in_y) + ";";
                    break;
                case "registration":
                    Console.WriteLine("registration!");
                    string log_r = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string pas_r = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=registration;result=" + api.registration(log_r, pas_r) + ";";
                    break;

                case "entrance":
                    Console.WriteLine("entrance!");
                    string log_e = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    string pas_e = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=entrance;result=" + api.entrance(log_e, pas_e) + ";";
                    break;

                case "remind":
                    Console.WriteLine("remind!");
                    string log_re = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=remind;result=" + api.remind(log_re) + ";";
                    break;
                case "get_notification":
                    Console.WriteLine("get_notification!");
                    string log_g = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    answer = "func=get_notification;" + api.get_notification(log_g);
                    break;
            }

            return answer;
        }

       
    }
}
