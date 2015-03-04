using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace trustPilotCodeChal
{
    public class Variations
    {
        public static List<List<T>> GetVariations<T>(int k, List<T> elements)
        {
            List<List<T>> result = new List<List<T>>();
            if (k == 1)
            {
                result.AddRange(elements.Select(element => new List<T>() { element }));
            }
            else
            {
                foreach (T element in elements)
                {
                    List<T> subelements = elements.Where(e => !e.Equals(element)).ToList();
                    List<List<T>> subvariations = GetVariations(k - 1, subelements);

                    foreach (List<T> subvariation in subvariations)
                    {
                        subvariation.Add(element);
                        result.Add(subvariation);
                        string s = string.Join(" ", subvariation);
                        if (Helper.stringMatchAnagram(s))
                        {
                            Console.WriteLine(s);
                        }
                        //GC.Collect();
                    }
                }
            }
            GC.Collect();
            return result;
        }

        public static string FindPhase(List<string> elements)
        {
            string phase = "";
            int count = 0;
            //ailnprwy oossttttuu 
            Parallel.ForEach(elements, (e1, loopstate1) => 
            {
                List<string> ele2 = Helper.RemoveSingleChar(e1, elements);

                Parallel.ForEach(ele2, (e2, loopstate2) => 
                {
                    List<string> ele3 = Helper.Remove2Chars(e1, e2, Helper.RemoveSingleChar(e2, ele2));                    

                    Parallel.ForEach(ele3, (e3, loopstate3) => 
                    {
                        string s = string.Format("{0} {1} {2}", e1, e2, e3);
                        //Console.Write("\r" + count); //performance impact...alot
                        if (Helper.stringMatchAnagram(s))
                        {
                            phase = s;
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
