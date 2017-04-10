using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CracKINGServer.BulkHandler
{
    public class GetWordsHandler
    {
        private static int index = 0;
        private static int counter = 0;

        internal List<string> GetWords()
        {
            List<string> noWordsList = new List<string>();
            List<string> bulkList = new List<string>();
            List<string> wordlist = new List<string>();
            using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))

            using (StreamReader dictionary = new StreamReader(fs))
            {

                while (!dictionary.EndOfStream)
                {
                    String dictionaryEntry = dictionary.ReadLine();
                    wordlist.Add(dictionaryEntry);
                }
            }


            int wordcount = 0;
            if (index == wordlist.Count - 1)
            {
                return noWordsList;
            }
            else
            {
                while (wordcount <= 1999)
                {
                    try
                    {
                        bulkList.Add(wordlist[index]);

                        wordcount = bulkList.Count;
                        index++;
                    }
                    catch (Exception)
                    {

                        break;
                    }
                

                }

                return bulkList;
            }
           
        }
    }
}
