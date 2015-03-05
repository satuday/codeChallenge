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
        
        
       
        static void Main(string[] args)
        {
            string[] wordlist;
            var logFile = File.AppendText(@"log.txt");

            DateTime start = DateTime.Now;
            logFile.WriteLine("Start: " + start.ToString());
            logFile.AutoFlush = true;
            wordlist = File.ReadAllLines(System.Configuration.ConfigurationManager.AppSettings["wordFile"]);//J:\Documents\download\wordlist testList.txt
            

            List<string> newWordList = Helper.CleanList(wordlist.ToList());

            var phase = Variations.FindPhase(3, new List<string>(), newWordList);

            DateTime stop = DateTime.Now;
            logFile.WriteLine("Stop. " + stop.ToString());
            logFile.WriteLine(stop - start);
            logFile.WriteLine("Done. " + (string.IsNullOrWhiteSpace(phase) ? "Fail" : phase));
            logFile.Flush();
            logFile.Close();

            Console.WriteLine("Done. " + (string.IsNullOrWhiteSpace(phase) ? "Fail" : phase));
            Console.ReadKey();
        }

      

    }
    
}
