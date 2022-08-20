using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01WstepLINQ
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] napisy = { "BACHLEDA", "MATEJA", "HERR" };

            string[] wynik = napisy.Where(x => x.Length > 4).ToArray();

            // Console.WriteLine(string.Join(" ", wynik));

            int[] liczby = { 4, 76, 8, 21, 2, 5 };
            int[] wynik2 = liczby
                .Where(x => x > 10)
                .OrderBy(x => x)
                .ToArray();

            ManagerZawodnikow mz = new ManagerZawodnikow();
            string[] wiersze = File.ReadAllLines(@"C:\dane\zawodnicy.txt");

            Zawodnik[] zrodlo = mz.Wczytaj(wiersze);

            Console.ReadKey();
        }
    }
}