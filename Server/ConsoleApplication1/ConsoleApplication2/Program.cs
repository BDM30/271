using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket soc = new Socket(SocketType.Stream, ProtocolType.IPv4);
            soc.Connect("localhost", 11000);
            byte[] msg = Encoding.UTF8.GetBytes("This is a test");
            soc.Send(msg);
        }
    }
}
