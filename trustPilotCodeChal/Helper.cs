using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace trustPilotCodeChal
{
    public static class Helper
    {
        static string anagram = "poultry outwits ants".Replace(" ", ""); //ailnprwy oossttttuu 
        static string anagramSorted = string.Join("", anagram.OrderBy(a => a));

        static string hash = "4624d200580677270a54ccff86b9610e";

        static IEnumerable<char> anagramChar = anagram.Replace(" ", "").ToCharArray().Distinct().OrderBy(a => a);

        //Match phase with hash.
        public static bool StringHashMatch(string str)
        {
            if (str.Replace(" ", "").Length == anagram.Length)
            {
                if (MD5Hash.VerifyMd5Hash(str, hash))
                {
                    return true;
                }

            }
            return false;
        }

        //Remove Duplicates, sort and remove word that dont have chars in anagram
        public static List<string> CleanList(List<string> wordList)
        {
            List<string> newWordList = new List<string>();
            wordList.Distinct().ToList().ForEach(a =>
            {
                if (a.ToCharArray().ToList().TrueForAll(b => anagram.Contains(b)))
                {
                    newWordList.Add(a);
                }
            });

            return newWordList.OrderByDescending(a => a).ToList();
        }

        //Remove words that have chars that appear once.
        public static List<string> RemoveSingleChar(string word,  List<string> wordList)
        {
            
            List<string> result = wordList.Where(a => !a.Equals(word)).ToList();
            if (word.Contains('a'))
            {
                result.RemoveAll(a => a.Contains('a'));
            }
            if (word.Contains('i'))
            {
                result.RemoveAll(a => a.Contains('i'));
            }
            if (word.Contains('l'))
            {
                result.RemoveAll(a => a.Contains('l'));
            }
            if (word.Contains('n'))
            {
                result.RemoveAll(a => a.Contains('n'));
            }
            if (word.Contains('p'))
            {
                result.RemoveAll(a => a.Contains('p'));
            }
            if (word.Contains('r'))
            {
                result.RemoveAll(a => a.Contains('r'));
            }
            if (word.Contains('w'))
            {
                result.RemoveAll(a => a.Contains('w'));
            }
            if (word.Contains('y'))
            {
                result.RemoveAll(a => a.Contains('y'));
            }

            return result;
        }

        //Remove words that have chars that appear twice.
        public static List<string> Remove2Chars(string word1, string word2, List<string> wordList)
        {
            var result = wordList.Where(a => !a.Equals(word1) || !a.Equals(word2)).ToList();
            if (word1.Contains('o') && word2.Contains('o'))
            {
                result.RemoveAll(a => a.Contains('o'));
            }
            if (word1.Contains('s') && word2.Contains('s'))
            {
                result.RemoveAll(a => a.Contains('s'));
            }
            if (word1.Contains('u') && word2.Contains('u'))
            {
                result.RemoveAll(a => a.Contains('u'));
            }

            return result.ToList();
        }
    }
}
