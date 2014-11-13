using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Text.RegularExpressions;

// EmailValidator - класс для проверки корректности email
// bool valid - атрибут хранящий статус проверки
// IsValidEmail(string strIn) - главный метод который будет проверять валидность
// DomainMapper(Match match) - метод преобразует UTF url в ASKII код

namespace myServer
{
    class EmailValidator
    {
        public static bool invalid = false;

        public static bool IsValidEmail(string strIn)
        {
            Console.Write("strIn={0}\n", strIn);
            // проверка на пустоту
            if (String.IsNullOrEmpty(strIn))
                return false;


            // домменое имя превели в норм форму, в случае если оно не ASCII
            // На этом этапе отпадут те, в которых больше 1 точки
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(2000));
            }
            catch (System.Text.RegularExpressions.RegexMatchTimeoutException)
            {
                return false;
            }
            Console.Write("strIn={0}\n", strIn);

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {

                //((?(")("[^"]+?"@) =
                // Если первый символ является кавычкой, совпадение с открывающей кавычкой, после которой следует
                // как минимум одно вхождение любого символа, отличного от кавычки, с последующей закрывающей кавычкой.
                //Строка должна заканчиваться знаком @. 

                // |(([0-9a-zA-Z] =
                //Если первый символ не является кавычкой, имеется соответствие любой буквы с A до Z или любой цифре от 0 до 9.

                //(\.(?!\.)) = 
                // Если следующим символом является точка, имеется соответствие. Если этот символ не является точкой, 
                // выполняется поиск вперед к следующему символу и продолжается поиск соответствия. (?!\.) является утверждением
                // отрицательного поиска вперед нулевой ширины, предотвращающим отображение двух последовательных точек в локальной части 
                // адреса электронной почты.

                // |[-!#\$%&'\*\+/=\?\^`\{\}\|~\w] = 
                // Если следующий символ не является точкой, совпадение с любым символом слова или одним из следующих символов: -!#$%'*+=?^`{}|~.

                return Regex.IsMatch(strIn,
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+([a-z0-9][-\w]*[0-9a-z]*){2,24}))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(2500));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }



            //return true;
        }
        // Проверка домена, чтобы все его символы были из ASCII
        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();


            string domainName = match.Groups[2].Value;
            Console.WriteLine("domain={0}", domainName);
            try
            {
                // Метод IdnMapping.GetAscii нормализует имя домена, преобразует нормализованное имя в представление
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
