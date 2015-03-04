using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trustPilotCodeChal
{
    public static class test
    {
        static int MIN_LENGTH = 2;
        static int MAX_LENGTH;

        static IList<string> vals;
        static IList<string> results;
        static int maxWordCount = 4;
        public static void run(List<string> wordlist)
        {
            StringBuilder sb = new StringBuilder();
            string[] phase = new string[4];
            int wordIndex = 0;

            //var c = new Combinatorics.Collections.Combinations<string>(newWordList, 3);

            //foreach (var v in c)
            //{
            //    Console.WriteLine(string.Join(" ", v));
            //}


            //printSubset(wordlist.Distinct().ToArray());

            //PowerSetofWordList();


            //Variations.GetVariations<string>(3, newWordList);


            //System.Threading.Thread t = new System.Threading.Thread(checkQueue);
            //t.Start();

            //CreateSubsets(wordlist.Distinct().ToArray());

            var result = GetVariations<string>(wordlist.Distinct().ToList(), 3).ToList();

            while (wordIndex < 4)
            {
                for (int i = wordIndex; i < wordlist.Count; i++)
                {
                    phase[wordIndex] = wordlist[i];
                    string s = string.Join(" ", phase).Trim();
                    Console.Write(s + ",");
                }
                phase[wordIndex] = wordlist[wordIndex];
                wordIndex++;

            }

            vals = new List<string>();
            vals = wordlist;

            results = new List<string>();
            go("");
            results = results.OrderBy(x => x.Length).ToList();

            foreach (string r in results)
            {
                Console.WriteLine(r);
            }

            Console.ReadKey();
        }


        static IEnumerable<IList<T>> GetVariations<T>(IList<T> offers, int length)
        {
            var startIndices = new int[length];
            for (int i = 0; i < length; ++i)
                startIndices[i] = i;

            var indices = new HashSet<int>(); // for duplicate check

            while (startIndices[0] < offers.Count)
            {
                var variation = new List<T>(length);
                for (int i = 0; i < length; ++i)
                {
                    variation.Add(offers[startIndices[i]]);
                }
                yield return variation;

                //Count up the indices
                AddOne(startIndices, length - 1, offers.Count - 1);

                //duplicate check                
                var check = true;
                while (check)
                {
                    indices.Clear();
                    for (int i = 0; i <= length; ++i)
                    {
                        if (i == length)
                        {
                            check = false;
                            break;
                        }
                        if (indices.Contains(startIndices[i]))
                        {
                            var unchangedUpTo = AddOne(startIndices, i, offers.Count - 1);
                            indices.Clear();
                            for (int j = 0; j <= unchangedUpTo; ++j)
                            {
                                indices.Add(startIndices[j]);
                            }
                            int nextIndex = 0;
                            for (int j = unchangedUpTo + 1; j < length; ++j)
                            {
                                while (indices.Contains(nextIndex))
                                    nextIndex++;
                                startIndices[j] = nextIndex++;
                            }
                            break;
                        }

                        indices.Add(startIndices[i]);
                    }
                }
            }
        }

        static int AddOne(int[] indices, int position, int maxElement)
        {
            //returns the index of the last element that has not been changed

            indices[position]++;
            for (int i = position; i > 0; --i)
            {
                if (indices[i] > maxElement)
                {
                    indices[i] = 0;
                    indices[i - 1]++;
                }
                else
                    return i;
            }
            return 0;
        }

        static IEnumerable<IEnumerable<T>> GetPowerSet<T>(List<T> list)
        {
            var range = 1 << list.Count;
            return from m in Enumerable.Range(0, range)
                   select
                       from i in Enumerable.Range(0, list.Count)
                       where (m & (1 << i)) != 0
                       select list[i];
        }

        static void PowerSetofWordList(List<string> wordlist)
        {

            var result = GetPowerSet<string>(wordlist.ToList());

            Console.Write(string.Join(Environment.NewLine,
                result.Select(subset =>
                    string.Join(",", subset.Select(clr => clr.ToString()).ToArray())).ToArray()));
        }

        static List<T[]> CreateSubsets<T>(T[] originalArray)
        {
            List<T[]> subsets = new List<T[]>();

            for (int i = 0; i < originalArray.Length; i++)
            {
                int subsetCount = subsets.Count;

                subsets.Add(new T[] { originalArray[i] });

                for (int j = 0; j < subsetCount; j++)
                {
                    T[] newSubset = new T[subsets[j].Length + 1];
                    subsets[j].CopyTo(newSubset, 0);
                    newSubset[newSubset.Length - 1] = originalArray[i];
                    subsets.Add(newSubset);
                }
            }
            return subsets;
        }


        static void printSubset<T>(T[] originalArray)
        {
            //List<T[]> subsets = new List<T[]>();
            int subsetsCount = 0;


            for (int i = 0; i < originalArray.Length; i++)
            {
                int subsetCount = subsetsCount; //subsets.Count;

                //subsets.Add(new T[] { originalArray[i] });
                Console.WriteLine(originalArray[i]);

                for (int j = 0; j < subsetCount; j++)
                {
                    //T[] newSubset = new T[subsets[j].Length + 1];
                    //subsets[j].CopyTo(newSubset, 0);
                    //newSubset[newSubset.Length - 1] = originalArray[i];
                    //subsets.Add(newSubset);
                    Console.WriteLine();
                }
            }
        }



        static void go(string cur)
        {
            if (cur.Length > MAX_LENGTH)
            {
                return;
            }
            if (cur.Length >= MIN_LENGTH && cur.Length <= MAX_LENGTH)
            {
                Console.WriteLine(cur);
                results.Add(cur);
            }

            foreach (string t in vals)
            {
                cur += t;
                go(cur);
                cur = cur.Substring(0, cur.Length - 1);
            }
        }



    }
}
