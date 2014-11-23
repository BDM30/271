using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Text.RegularExpressions;

/*
    EmailValidator:
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
 * public IsValidEmail(string strIn) - главный метод который будет проверять валидность
*/

namespace myServer
{
    class EmailValidator
    {
        private bool invalid;

        public EmailValidator()
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
