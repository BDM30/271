using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApplication1
{
    class Parser
    {
        public void showText(object clientO)
        {
            
            Byte[] bytes = new Byte[256];
            String data = null;
            TcpClient client = clientO as TcpClient;
            NetworkStream stream = client.GetStream();

            int i;
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("\nmessage:{0}",data );
                string ipClient = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                Console.WriteLine("Client Ip Address is: {0}", ipClient);
                
                Socket soc = new Socket(SocketType.Stream, ProtocolType.Tcp);
                soc.Connect(ipClient, 12000);
                byte[] msg = Encoding.UTF8.GetBytes("This is a test");
                soc.Send(msg);
                soc.Shutdown(SocketShutdown.Both);
                soc.Close();
                 
                 
            }
            client.Close();
        
        }
    }
}
