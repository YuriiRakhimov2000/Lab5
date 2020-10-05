using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace ConsoleLab5
{
    class Program
    {
        static void Main(string[] args)
        {


            Parser p = new Parser();
            Console.WriteLine("before get");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            p.GetAllBookPagesUrls();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("After get");
            Console.WriteLine(p.BookPagesUrls.Count);
        //  p.BookPagesUrls.ForEach(Console.WriteLine);

        }
    }
}