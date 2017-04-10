using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using CracKINGServer.BulkHandler;
using CracKINGServer.Model;
using CracKINGServer.Util;
using Newtonsoft.Json;

namespace CracKINGServer
{
   public class Service
   {
       public static int NoOfClients;
        public static Stopwatch stopwatch = new Stopwatch();


        private TcpClient connectionSocket;
        static List<UserInfoClearText> resultList= new List<UserInfoClearText>();
        public Service(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }

        public void GetResults()
        {
            foreach (var userInfoClearText in resultList)
            {
                Console.WriteLine(userInfoClearText);
            }
        }
        internal void DoIt()
        {
            
            NetworkStream ns = connectionSocket.GetStream();

            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;


            List<UserInfo> userInfos =
                PasswordFileHandler.ReadPasswordFile("passwords.txt");
            Console.WriteLine("passwd  send");
            


            sw.WriteLine(JsonConvert.SerializeObject(userInfos));
            GetWordsHandler wordLock = new GetWordsHandler();
            while (true)
            {
                string ClientResponse = sr.ReadLine();
                if (ClientResponse == "Need Words")
                {
                    lock (wordLock)
                    {
                      
                      List<string> wordsForCrack = wordLock.GetWords();

                        if (wordsForCrack.Count==0)
                        {
                            sw.WriteLine("No Words");
                            string results = sr.ReadLine();
                            List<UserInfoClearText> tempResultList = JsonConvert.DeserializeObject<List<UserInfoClearText>>(results);
                            
                            foreach (var userInfoClearText in tempResultList)
                            {
                                resultList.Add(userInfoClearText);
                            }
                            NoOfClients--;
                            if (Service.NoOfClients == 0)
                            {
                                
                                GetResults();
                                stopwatch.Stop();
                                Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                                Console.ReadKey();
                            }

                        }
                        else
                        {
                            sw.WriteLine("New Words");
                            sw.WriteLine(JsonConvert.SerializeObject(wordsForCrack));
                        }
                       

                        
                    }

                }
            }
            
            //Console.WriteLine(JsonConvert.SerializeObject(userInfos));

            
               
                




        }

       
    }
}
