using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Web.Helpers;
using System.Web;



/*
    AnswerServer:
 * Определяет какой метод, хочет пользователя и выдает результат запроса.
 * Описание API в отдельном txt файле проекта
    
    Использует:
 * Validator
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
        private APIexecuter api_executer;

        public AnswerServer(Hashtable one, Hashtable two)
        {
            validator = new Validator();
            api_executer = new APIexecuter(ref one, ref two);
        }
        
        public string getAnswer (string query) {
            // раскодируем url
            query = HttpUtility.UrlDecode(query);

            string answer = "default value";


            // Времено уберем
            /*
            if (!validator.isValidQuery(query))
            {
                return "wrong query";
            }
            */

            //определили метод
            dynamic json = Json.Decode(query);
            string method = json.function;



          

            switch (method)
            {
                
            case "add_note":
                Console.WriteLine("add_notification!");
                // Пишем ответ. Функция и результат. Ниже аналогично.
                answer = Json.Encode( new
                { Function = method,
                  Result = api_executer.add_notification(json.name, json.user, json.x, json.y)
                });      
                break;
            
                case "registration":
                    Console.WriteLine("registration!");
                    answer = Json.Encode(new
                    {
                        Function = method,
                        Result = api_executer.registration(json.email, json.password)
                    });  
                    break;
                
                case "entrance":
                    Console.WriteLine("entrance!");
                    answer = Json.Encode(new
                    {
                        Function = method,
                        Result = api_executer.entrance(json.email, json.password)
                    });
                    break;
                
                case "remind":
                    Console.WriteLine("remind!");
                    answer = Json.Encode(new
                    {
                        Function = method,
                        Result = api_executer.remind(json.email)
                    });
                    break;
                
                case "get_notes":
                    Console.WriteLine("get_notification!");
                    List<Note> user_notes = api_executer.get_notification(json.email);
                    if (user_notes.Count == 0)
                    {
                        answer = Json.Encode(new
                        {
                            Function = method,
                            Result = "0",
                            Notes = "null"
                        });
                    }
                    else
                    {
                        answer = Json.Encode(new
                        {
                            Function = method,
                            Result = "1",
                            Notes = user_notes
                        });
                    }
                    break;
                 
            }

            return answer;
        }

       
    }
}
