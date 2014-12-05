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
 * APIexecuter
 * HttpServer
 
    Атрибуты:
 *  public double X; - координата
 *  public double Y;
 *  public int Id;
 *  public string Name;
 *  public string Owner; - email владельца
    
    Методы:
 * == - равенство без id? 1 : 0
 * public Note(string name_, string owner_, double x_, double y_, int id_) - инициализация
 * public override string ToString() - на выходе строка в формате хранения в файле и оправки клиенту.
 * public Note() - без него не читается из JSON. Весьма любопытный факт
*/


namespace myServer
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Note()
        {

        }
        public Note(string name_, string owner_, double x_, double y_, int id_)
        {
            Id = id_;
            X = x_;
            Y = y_;
            Name = name_;
            Owner = owner_;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Note))
                return false;
            else
                return X == ((Note)obj).X && Y == ((Note)obj).Y &&
                    Name == ((Note)obj).Name && Owner == ((Note)obj).Owner;
        }       
    }
}
