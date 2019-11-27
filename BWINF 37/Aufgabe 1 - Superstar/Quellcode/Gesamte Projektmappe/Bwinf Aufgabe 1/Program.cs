using System;

namespace Bwinf_Aufgabe_1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Aus welcher Datei soll geladen werden? (ex. superstar1.txt)\n:");
            string filename = Console.ReadLine();

            if (!System.IO.File.Exists(filename)) {
                Console.WriteLine("\nFehler: Die Datei konnte leider nicht gefunden werden.\n");
                Main(null);
            }

            Teeniegram.Load(filename);

            //PrintGeneralInformation();

            Person star = Teeniegram.FindSuperstar();

            Console.WriteLine((star is null) ? "Es gibt keinen Superstar!" : star.Name + " ist der Superstar!");
            Console.ReadKey();
        }

        //private static void PrintGeneralInformation()
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("---------------------------------------------------------------------");
        //    Console.WriteLine("Es wurden insgesamt " + Person.AnzahlUser + " Personen erstellt!\n");
        //    foreach (string s in Person.GetAllFollows())
        //    {
        //        Console.WriteLine(s);
        //    }
        //    Console.WriteLine();
        //    Console.WriteLine("---------------------------------------------------------------------");
        //    Console.WriteLine("\n");
        //}
    }
}