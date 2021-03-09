using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
          foreach(var i in PositiveInts(10)) {
                Console.WriteLine(i);
          }
        }

        public static IEnumerable<int> PositiveInts(int max)
        {
            var list = new List<int>();
            for(int i = 1; i < max; ++i) {
                yield return i;
            }
        }
    } 
}
