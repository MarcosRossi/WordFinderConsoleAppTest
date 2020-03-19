using System;
using System.Linq;

namespace WorldFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new string[] { "chill", "wind", "snow", "cold" };
            var src = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var wFinder = new WordFinder(dictionary);

            if (wFinder.Exceed2048 || wFinder.Exceed64x64(src))
                Console.Write(wFinder.Exceed2048 ? "The number of items in the dictionary does not exceed 2048" : "The size of the matrix does not exceed 64x64");
            else
            {
                var result = wFinder.Find(src);
                result.ToList().ForEach(c => Console.Write("'{0}' ", c));
            }
            Console.ReadKey();
        }
    }
}
