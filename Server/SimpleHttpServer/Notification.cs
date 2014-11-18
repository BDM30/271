using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myServer
{
    public class Notification
    {
        public Notification(string name_, string owner_, double x_, double y_, int id_)
        {
            id = id_;
            x = x_;
            y = y_;
            name = name_;
            owner = owner_;
            Console.WriteLine("const success");
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
        public double x;
        public double y;
        public int id;
        public string name;
        public string owner;
    }
}
