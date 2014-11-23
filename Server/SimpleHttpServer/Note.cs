using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Note:
 * класс представляющий абстракцию Напоминание. 
 * позже должен быть заменен или дополнен таблицей в соответсвующей БД.
 * должен быть аналогичен классу User
 * в перспективе будет дополнен полем Description
 
    Использует:
 * пока ничего
 
    Используется:
 * AnswerServer
 * HttpServer
 
    Атрибуты:
 *  public double x; - координата
 *  public double y;
 *  public int id;
 *  public string name;
 *  public string owner; - email владельца
    
    Методы:
 * == - полное равенство включая id? 1 : 0
 * public Note(string name_, string owner_, double x_, double y_, int id_) - инициализация
 * public override string ToString() - на выходе строка в формате хранения в файле и оправки клиенту.
*/


namespace myServer
{
    public class Note
    {
        public double x;
        public double y;
        public int id;
        public string name;
        public string owner;
        public Note(string name_, string owner_, double x_, double y_, int id_)
        {
            id = id_;
            x = x_;
            y = y_;
            name = name_;
            owner = owner_;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Note))
                return false;
            else
                return x == ((Note)obj).x && y == ((Note)obj).y &&
                    name == ((Note)obj).name && owner == ((Note)obj).owner;
        }

        public override string ToString()
        {
            string result = "";
            result += "id=";
            result += id.ToString();
            result += ";";
            result += "name=";
            result += name;
            result += ";";
            result += "owner=";
            result += owner;
            result += ";";
            result += "x=";
            result += x.ToString();
            result += ";";
            result += "y=";
            result += y.ToString();
            result += ";";
            return result;
        }
        
    }
}
