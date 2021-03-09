using System;
using System.Collections.Generic;
using System.Linq;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args) {
            foreach(var el in PositiveInts(10).Select(i => i * i)) {
                System.Console.WriteLine(el);
            }

            foreach(var n in PlayerNames) {
                System.Console.WriteLine(n);
            }

            Console.ReadKey();
        }

        public static IEnumerable<string> PlayerNames {
            get {
                yield return "world of john";
                yield return "world of alex";
                yield return "world of kevin";
                yield return "world of sami";
            }
        }

        public static IEnumerable<int> PositiveInts(int n) {
            int i = 1;
            while(true) {
                yield return i++;
                if(i >= n) {
                    yield break;
                }
            }
        }
    }
}
