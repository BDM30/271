using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Notification:
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
 * public Notification(string name_, string owner_, double x_, double y_, int id_) - инициализация
 * public override string ToString() - на выходе строка в формате хранения в файле и оправки клиенту.
*/


namespace myServer
{
    public class Notification
    {
        public double x;
        public double y;
        public int id;
        public string name;
        public string owner;
        public Notification(string name_, string owner_, double x_, double y_, int id_)
        {
            id = id_;
            x = x_;
            y = y_;
            name = name_;
            owner = owner_;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Notification))
                return false;
            else
                return x == ((Notification)obj).x && y == ((Notification)obj).y &&
                    name == ((Notification)obj).name && owner == ((Notification)obj).owner;
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
