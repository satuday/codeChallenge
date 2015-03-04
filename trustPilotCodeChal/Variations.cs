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
        public static string FindPhase(int k, List<string> elements)
        {
            string phase = "";
            int count = 0;
            Parallel.ForEach(elements, (e1, loopstate1) => 
            {
                List<string> ele2 = Helper.RemoveSingleChar(e1, elements);

                Parallel.ForEach(ele2, (e2, loopstate2) => 
                {
                    List<string> ele3 = Helper.RemoveSingleChar(e2, ele2);                    

                    Parallel.ForEach(ele3, (e3, loopstate3) => 
                    {
                        string s = string.Format("{0} {1} {2}", e1, e2, e3);
                        //Console.Write("\r" + count); //performance impact...alot
                        if (Helper.StringHashMatch(s))
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
