using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Text.RegularExpressions;

/*
   Validator:
 * класс для проверки email на корректность.
 * Код взят с mdsn с небольшим дополнением. (http://msdn.microsoft.com/ru-ru/library/01escwtf(v=vs.110).aspx)
 
    Использует:
 * никого пока что

    Используется:
 * AnswerServer
   
    Атрибуты:
 * private bool invalid - флажок для последовательности проверок
    
    Методы:
 * public EmailValidator() - инициализация
 * private DomainMapper(Match match) - метод преобразует UTF url в ASKII код
 * public IsValidEmail(string strIn) - главный метод который будет проверять валидность email
 * public bool isValidQuery(string query) - проверка запроса на ситаксичесекую корректность. Все ли поля есть и тд..
*/

namespace myServer
{
    class Validator
    {
        private bool invalid;

        public Validator()
        {
            invalid = false;
        }

        public bool IsValidEmail(string strIn)
        {
            // проверка на пустоту
            if (String.IsNullOrEmpty(strIn))
                return false;

            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(2000));
            }
            catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+([a-z0-9][-\w]*[0-9a-z]*){2,24}))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(2500));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public bool isValidQuery(string query)
        {
            // завели разделители, которыми будем парсить
            char[] charSeparatorsBlocks = new char[] { ';' };
            char[] charSeparatorsNameValue = new char[] { '=' };

            // получили массив блоков. То, что находится между ';'.
            string[] arrayBlocks = query.Split(charSeparatorsBlocks, StringSplitOptions.None);
            switch (arrayBlocks.Length)
            {
                // добавление уведомления
                case 5:
                    // параметры
                    string func = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string name = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string user = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string x = arrayBlocks[3].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string y = arrayBlocks[4].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];

                    // значения
                    string add_notification = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];

                    // проверка параметров и значений
                    if (!(func == "func" &&
                                name == "name" &&
                                user == "user" &&
                                x == "x" &&
                                y == "y"))
                        return false;

                    if (!(add_notification == "add_notification"))
                        return false;
                    break;

                // вход и регистрация
                case 3:
                    // параметры
                    string func_3 = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string email = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string password = arrayBlocks[2].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    // значения
                    string registrationEntrance = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    // проверка параметров и значений
                    if (!(func_3 == "func" &&
                                email == "email" &&
                                password == "password"))
                    {
                        return false;
                    }
                    if (!(registrationEntrance == "registration" || registrationEntrance == "entrance"))
                    {
                        return false;
                    }
                    break;
                case 2:
                    string func_2 = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string email_2 = arrayBlocks[1].Split(charSeparatorsNameValue, StringSplitOptions.None)[0];
                    string remindGetNote = arrayBlocks[0].Split(charSeparatorsNameValue, StringSplitOptions.None)[1];
                    if (!(func_2 == "func" &&
                                email_2 == "email"))
                        return false;
                    if (!(remindGetNote == "remind" || remindGetNote == "get_notification"))
                        return false;
                    break;
                default:
                    return false;
                    break;
            }

            return true;
        }

        private string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();


            string domainName = match.Groups[2].Value;
            Console.WriteLine("domain={0}", domainName);
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
