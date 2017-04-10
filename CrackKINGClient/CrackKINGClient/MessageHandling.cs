using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrackKINGClient.Model;
using Newtonsoft.Json;

namespace CrackKINGClient
{
    public class MessageHandling
    {
        public List<UserInfo> GetPasswords(StreamReader reader)
        {
            string passwordui = reader.ReadLine();
            List<UserInfo> tempList = JsonConvert.DeserializeObject<List<UserInfo>>(passwordui);
            return tempList;
        }

        public List<string> GetWords(string json)
        {
           // string words = reader.ReadLine();
            List<string> tempwords = JsonConvert.DeserializeObject<List<string>>(json);
            return tempwords;
        }

    }
}
