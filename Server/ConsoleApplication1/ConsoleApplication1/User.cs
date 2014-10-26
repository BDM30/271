using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// АТД Пользователь
// хранит id, email, password ,а также метод SET-GET

namespace ConsoleApplication1
{
    class User
    {

        // коструктор
        public User(int idIn, string emailIn, string passwordIn)
        {
            id = idIn;
            email = emailIn;
            password = passwordIn;
        }
        // печать всех данных в консоль
        public void showData()
        {
            Console.WriteLine("id = {0}\nemail = {1}\npassword = {2}", id, email, password);
        }
        // set - методы
        public void setId(int idIn)
        {
            id = idIn;
        }
        public void setEmail(string emailIn)
        {
            email = emailIn;
        }
        public void setPassword(string passwordIn)
        {
            password = passwordIn;
        }
        // get - методы
        public int getId()
        {
            return id;
        }
        public string getEmail()
        {
            return email;
        }
        public string getPassword()
        {
            return password;
        }

        // переменные, хранящие данные
        private int id;
        private string email;
        private string password;
    }
}
