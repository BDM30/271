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
    
    Поля:
 * public static int amount_notes; - класс будет чекать кол-во созданных. И сам назначать Id при добавлении
 
    Свойства:
 *  public double X; - координата
 *  public double Y;
 *  public int Id;
 *  public string Name;
 *  public string Owner; - email владельца
    
    Методы:
 * == != - Owner Name X Y совпали? 1 : 0 (не используется)
 * < > - сравнения по Id 1 : 0 (не используется)
 * public Note(string name_, string owner_, double x_, double y_, int id_) - инициализация
 * public override string ToString() - на выходе строка в формате хранения в файле и оправки клиенту.
 * public Note() - без него не читается из JSON. Весьма любопытный факт
 * public override bool Equals(Object obj) - эквивалентны ли объекты? (не используется)
 * public string this[int index] = Мы можем по индексу вызывать свой-ства Note. 0 = Email 1 = Password (не используется)
*/


namespace myServer
{
    public class Note
    {

        public static int amount_notes;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Note()
        {
        }
        public Note(string name_, string owner_, double x_, double y_)
        {
            Id = amount_notes++;
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

        // операторы 
        public static bool operator ==(Note a, Note b)
        {
            return (a.Name == b.Name && a.Owner == b.Owner && a.X == b.X && a.Y == b.Y) ? true : false;
        }
        public static bool operator !=(Note a, Note b)
        {
            return (a.Name == b.Name && a.Owner == b.Owner && a.X == b.X && a.Y == b.Y) ? false : true;
        }
        public static bool operator <(Note a, Note b)
        {
            return (a.Id < b.Id) ? true : false;
        }
        public static bool operator >(Note a, Note b)
        {
            return (a.Id > b.Id) ? true : false;
        }

        // зафигачим индексацию целочисленную 
        // PS можно также индексировать стрингом
        // 0 = Name 1 = Owner
        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Name;
                    case 1: return Owner;
                    default: throw new ArgumentOutOfRangeException("index", "Only index 0-1 valid!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: Name = value; break;
                    case 1: Owner = value; break;
                    default: throw new ArgumentOutOfRangeException("index", "Only index 0-1 valid!");
                }
            }
        }
    }
}
