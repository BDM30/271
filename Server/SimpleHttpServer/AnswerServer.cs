using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Web.Helpers;



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
            Console.WriteLine("called getAsnwer!");

            string answer = "default value";


            // Времено уберем
            /*
            if (!validator.isValidQuery(query))
            {
                return "wrong query";
            }
            */

            //определили метод
            string method = Json.Decode<Query>(query).function;


          

            switch (method)
            {
                
            case "add_note":
                Console.WriteLine("add_notification!");
                    
                answer = "func=add_notification;result=" + api.add_notification(Json.Decode<AddNoteQuery>(query).name,
                    Json.Decode<AddNoteQuery>(query).user,
                    Json.Decode<AddNoteQuery>(query).x,
                    Json.Decode<AddNoteQuery>(query).y) + ";";
                     
                break;
            
                case "registration":
                    Console.WriteLine("registration!");
                    answer = "func=registration;result=" + api.registration(Json.Decode<RegistrationQuery>(query).email,
                                                                            Json.Decode<RegistrationQuery>(query).password) + ";";
                    break;
                
                case "entrance":
                    Console.WriteLine("entrance!");
                    answer = "func=entrance;result=" + api.entrance(Json.Decode<EntranceQuery>(query).email,
                                                                    Json.Decode<EntranceQuery>(query).password) + ";";
                    break;
                
                case "remind":
                    Console.WriteLine("remind!");
                    answer = "func=remind;result=" + api.remind(Json.Decode<RemindQuery>(query).email) + ";";
                    break;
                
                case "get_notes":
                    Console.WriteLine("get_notification!");
                    answer = "func=get_notification;" + api.get_notification(Json.Decode<GetNotesQuery>(query).email);
                    break;
                 
            }

            return answer;
        }

       
    }
}
