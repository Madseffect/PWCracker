using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrackKINGClient.Model;
using Newtonsoft.Json;
using PasswordCrackerCentralized;

namespace CrackKINGClient
{
     public class Program
    {
        public static void threadMethod()
        {
            MessageHandling MH = new MessageHandling();
            Cracking cracker = new Cracking();
            TcpClient clientSocket = new TcpClient("192.168.6.87", 6446);
            Stream ns = clientSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;
            string serverMessage;
            string clientResponce;

            cracker.ListUi = MH.GetPasswords(sr);
            List<UserInfoClearText> result = new List<UserInfoClearText>();
            clientResponce = "Need Words";
            sw.WriteLine(clientResponce);
            while (true)
            {
                serverMessage = sr.ReadLine();
                if (serverMessage.StartsWith("New Words"))
                {
                    string serverResponse = sr.ReadLine();
                    //string[] serverResponse = sr.ReadLine().Split('\n');

                    cracker.words = MH.GetWords(serverResponse);
                    List<UserInfoClearText> templist = cracker.RunCracking();
                    foreach (UserInfoClearText infoClearText in templist)
                    {
                        result.Add(infoClearText);
                    }
                    clientResponce = "Need Words";
                    sw.WriteLine(clientResponce);
                }
                else if (serverMessage.StartsWith("No Words"))
                {

                    sw.WriteLine(JsonConvert.SerializeObject(result));
                    foreach (var userInfoClearText in result)
                    {
                        Console.WriteLine(userInfoClearText);
                    }
                    clientSocket.Close();
                    break;
                }
            }
        }
      

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(threadMethod));
            Thread thread2 = new Thread(new ThreadStart(threadMethod));
            Thread thread3 = new Thread(new ThreadStart(threadMethod));
            Thread thread4 = new Thread(new ThreadStart(threadMethod));

            thread1.Start();
            thread2.Start();
            //thread3.Start();
            //thread4.Start();
        }

       
    }

}

