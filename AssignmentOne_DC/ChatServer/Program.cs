using ChatServerDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host;
            NetTcpBinding binding = new NetTcpBinding();
            host = new ServiceHost(typeof(Server));
            host.AddServiceEndpoint(typeof(ServerInterface), binding, "net.tcp://localhost:8100/Server");
            host.Open();

            Console.ReadLine();

            host.Close();


        }
    }
}
