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
 * Query (и его производные подклассы) - для парсинга запросов и создания ответов.
    
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
                AddNoteQuery addNoteIn = Json.Decode<AddNoteQuery>(query);
                Query addNoteOut = new Query();
                addNoteOut.function = method;
                addNoteOut.result = api_executer.add_notification(addNoteIn.name, addNoteIn.user, addNoteIn.x, addNoteIn.y);
                answer = Json.Encode(addNoteOut);      
                break;
            
                case "registration":
                    Console.WriteLine("registration!");
                    RegistrationQuery registrationIn = Json.Decode<RegistrationQuery>(query);
                    Query registrationOut = new Query();
                    registrationOut.function = method;
                    registrationOut.result = api_executer.registration(registrationIn.email, registrationIn.password);
                    answer = Json.Encode(registrationOut);
                    break;
                
                case "entrance":
                    Console.WriteLine("entrance!");
                    EntranceQuery entranceIn = Json.Decode<EntranceQuery>(query);
                    Query entranceOut = new Query();
                    entranceOut.function = method;
                    entranceOut.result = api_executer.entrance(entranceIn.email, entranceIn.password);
                    answer = Json.Encode(entranceOut);
                    break;
                
                case "remind":
                    Console.WriteLine("remind!");
                    RemindQuery remindIn = Json.Decode<RemindQuery>(query);
                    Query remindOut = new Query();
                    remindOut.function = method;
                    remindOut.result = api_executer.remind(remindIn.email);
                    answer = Json.Encode(remindOut);
                    break;
                
                case "get_notes":
                    Console.WriteLine("get_notification!");
                    GetNotesQuery getNotesIn = Json.Decode<GetNotesQuery>(query);
                    GetNotesAnswer getNotesOut = new GetNotesAnswer();
                    getNotesOut.function = method;
                    List<Note> user_notes = api_executer.get_notification(getNotesIn.email);
                    if (user_notes.Count == 0)
                    {
                        getNotesOut.result = "0";
                        getNotesOut.notes = null;
                    }
                    else
                    {
                        getNotesOut.result = "1";
                        getNotesOut.notes = user_notes;
                    }
                    answer = Json.Encode(getNotesOut);
                    //answer = "func=get_notification;" + api_executer.get_notification(Json.Decode<GetNotesQuery>(query).email);
                    break;
                 
            }

            return answer;
        }

       
    }
}
