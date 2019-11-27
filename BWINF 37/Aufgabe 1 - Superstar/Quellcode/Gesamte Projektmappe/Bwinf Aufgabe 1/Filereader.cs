using System.Collections.Generic;
using System.IO;

namespace Bwinf_Aufgabe_1
{
    internal class FileReader
    {
        private string[] line;

        public void LoadFile(string path)
        {
            try
            {
                line = File.ReadAllLines(path);
            }
            catch (FileNotFoundException)
            {
                System.Console.WriteLine(path + " konnte leider nicht gefunden werden.");
            }
        }

        public string[] GetAllUsernames() => line[0].Split(' ');

        public List<string[]> GetRelations()
        {
            List<string[]> relations = new List<string[]>();
            for (int i = 1; i < line.Length; i++)
            {
                relations.Add(line[i].Split(' '));
            }

            return relations;
        }
    }
}