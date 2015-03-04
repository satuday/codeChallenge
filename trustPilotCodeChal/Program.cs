using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Collections.Concurrent;
using Combinatorics.Collections;

namespace trustPilotCodeChal
{
    class Program
    {
        static string[] wordlist;
        
        static string anagram = "poultry outwits ants".Replace(" ", "");
        static IEnumerable<char> anagramChar = anagram.ToCharArray().OrderBy(a => a);
        //check string and char counts to match all chars in "poultry outwits ants"
       
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            wordlist = File.ReadAllLines(System.Configuration.ConfigurationManager.AppSettings["wordFile"]);//J:\Documents\download\wordlist testList.txt
            //var result = File.CreateText(@"J:\Documents\download\result.txt");

            List<string> newWordList = Helper.CleanList(wordlist.ToList());

            var phase = Variations.FindPhase(newWordList);

            //var c = new Combinatorics.Collections.Variations<string>(newWordList, 3, GenerateOption.WithoutRepetition);

             
            //c.AsParallel().ForAll((a) =>
            //    {
            //        count++;
            //        string s = string.Join(" ", a);

            //        //System.Diagnostics.Debug.WriteLine(s);
            //        if (Helper.stringMatchAnagram(s))
            //        {
            //            Console.WriteLine(s);
            //            result.WriteLine(s);
            //        }
            //    });


            //result.Flush();
            //result.Close();

            Console.WriteLine("Done. " + phase);
            DateTime stop = DateTime.Now;

            Console.WriteLine(stop - start);
            Console.ReadKey();
        }

      

    }
    
}
