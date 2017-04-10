using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace CracKINGServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(6446);
            serverSocket.Start();
            Console.WriteLine("Server is started");
            
           
            

            while (true)
            {

                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Hej med dig cracker");
                Service service = new Service(connectionSocket);

                
                Task.Factory.StartNew(() => service.DoIt());
                Service.NoOfClients++;
                
                if (Service.NoOfClients == 1)
                {
                    Service.stopwatch.Start();
                }

               


            }
        }
    }
}
