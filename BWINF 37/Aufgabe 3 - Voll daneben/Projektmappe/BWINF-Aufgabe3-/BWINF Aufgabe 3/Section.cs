using System;
using System.Collections.Generic;

namespace BWINF_Aufgabe_3
{
    internal class Section
    {
        #region Attributes

        private readonly double Average; // Individual base value for the specific section marker
        private readonly byte Level; //Depth of "Split's"
        private const int MAX_DEPTH = 3;

        private readonly List<int> Numbers; //Stores all numbers in this section
        private readonly Section[] Sections = new Section[2]; // Each Section hold 2 more Sections
        public static List<int> Markers { get; private set; } = new List<int>(); // Average of all Sections

        #endregion Attributes

        #region Konstruktor

        public Section(List<int> Numbers, byte Level)
        {
            this.Numbers = Numbers;
            Average = CalcAvg(Numbers);

            if (Level != MAX_DEPTH)
            {
                Tuple<List<int>, List<int>> tuple = Split();
                Sections[0] = new Section(tuple.Item1, (byte)(Level + 1));
                Sections[1] = new Section(tuple.Item2, (byte)(Level + 1));
            }
            else
            {
                Markers.Add((int)Math.Round(Average, 0));
            }
        }

        #endregion Konstruktor

        #region Methods

        /// <summary>
        /// Entry point for a chain reaction of creating 10 sections 
        /// 
        /// 
        /// </summary>
        /// <param name="numbersList"></param>
        internal static void Create(List<int> numbersList)
        {
            Tuple<List<int>, List<int>> tuple = CutAt(numbersList, 20);
            Section Robin = new Section(tuple.Item1, 2); //Creates 2 Sections  (20%)
            Section Batman = new Section(tuple.Item2, 0); //Creates 8 Sections (80%)
        }

        private static Tuple<List<int>, List<int>>
            CutAt(List<int> numbersList, int v)
        {
            List<int> a = new List<int>();
            List<int> b = new List<int>();

            foreach (int num in numbersList)
            {
                if (num < (CalcAvg(numbersList) / 50) * v)
                {
                    a.Add(num);
                }
                else
                {
                    b.Add(num);
                }
            }
            return Tuple.Create(a, b);
        }

        private Tuple<List<int>, List<int>> Split()
        {
            List<int> a = new List<int>();
            List<int> b = new List<int>();

            foreach (int num in Numbers)
            {
                if (num < Average)
                {
                    a.Add(num);
                }
                else
                {
                    b.Add(num);
                }
            }

            return Tuple.Create(a, b);
        }

        /// <summary>
        /// Calculates the average of the given numbers in the list
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static double CalcAvg(List<int> numbers)
        {
            if (numbers.Count != 0) // Else possible [Divide by Zero]
            {
                int sum = 0;
                foreach (int n in numbers)
                {
                    sum += n;
                }

                return sum / numbers.Count;
            }

            return 0;
        }

        #endregion Methods
    }
}