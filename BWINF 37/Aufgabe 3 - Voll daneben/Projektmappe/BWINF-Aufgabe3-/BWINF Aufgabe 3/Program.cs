using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BWINF_Aufgabe_3
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            Console.WriteLine("Dateiname der Datei mit den Glückszahlen: (ex. beispiel2.txt)");
            string[] lines = LoadLines();

            int[] numbers = ConvertToInteger(lines);

            List<int> numbersList = new List<int>(numbers);
            numbersList.Sort();
            Stopwatch s = new Stopwatch();
            s.Start();
            Section.Create(numbersList);
            s.Stop();

            Console.WriteLine("Markers: ");
            foreach (int n in Section.Markers)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine("Errechnet in " + s.ElapsedMilliseconds + "ms");
            Console.ReadKey();
        }

        private static string[] LoadLines()
        {
            try
            {
                string fileName = Console.ReadLine();
                return File.ReadAllLines(fileName);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Datei nicht gefunden, Eingabe bitte wiederholen:");
                return LoadLines();
            }
        }

        private static int[] ConvertToInteger(string[] lines)
        {
            int[] numbers = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                numbers[i] = Convert.ToInt32(lines[i]);
            }

            return numbers;
        }
    }
}