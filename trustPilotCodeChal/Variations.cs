using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace trustPilotCodeChal
{
    public static class Variations
    {
        //find phase with up to k words.
        public static string FindPhase(int k, List<string> words, List<string> elements)
        {
            string phase = string.Empty;
            string temp = string.Empty;
            if (words.Count > 1)
            {
                temp = string.Join(" ", words);
                //Console.WriteLine(temp);
                if (Helper.StringMatchHash(temp))
                {
                    return temp;
                }
            }
            if (k > 0)
            {
                Parallel.ForEach<string>(elements, (string word, ParallelLoopState loopstate) =>
                {
                    List<string> tempWords = new List<string>();
                    tempWords.AddRange(words);
                    tempWords.Add(word);
                    List<string> newList = Helper.RemoveSingleChar(word, elements);
                    temp = Variations.FindPhase(k - 1, tempWords, newList);
                    if (!string.IsNullOrWhiteSpace(temp))
                    {
                        phase = temp;
                        loopstate.Stop();
                    }
                });
            }
            return phase;
        }

        //find phase with 2 or 3 words
        public static string FindPhase(List<string> elements)
        {
            string phase = "";

            Parallel.ForEach(elements, (word1, loopstate1) => 
            {
                List<string> elems2 = Helper.RemoveSingleChar(word1, elements);
                Parallel.ForEach(elems2, (word2, loopstate2) => 
                {
                    List<string> elems3 = Helper.RemoveSingleChar(word2, elems2);
                    string s2 = string.Format("{0}", word1, word2);
                    //Console.Write("\r" + count); 
                    if (Helper.StringMatchHash(s2))
                    {
                        phase = s2;
                        loopstate2.Stop();
                        loopstate1.Stop();
                    }

                    Parallel.ForEach(elems3, (word3, loopstate3) => 
                    {
                        string s3 = string.Format("{0} {1} {2}", word1, word2, word3);
                        //Console.Write("\r" + count); 
                        if (Helper.StringMatchHash(s3))
                        {
                            phase = s3;
                            loopstate3.Stop();
                            loopstate2.Stop();
                            loopstate1.Stop();
                        }
                    });
                });
            });
            return phase;
        }
    }
}
