using System;
using System.Collections.Generic;

namespace Bwinf_Aufgabe_1
{
    internal class Teeniegram
    {
        private static FileReader fr = new FileReader();
        private static int cost = 0;

        /// <summary>
        /// just for testing
        /// </summary>
        public static void Load(string filename)
        {
            Console.WriteLine("Loading file " + filename + "...");
            fr.LoadFile(filename);
            Console.WriteLine("Generating Users...");
            GenerateUser();
            Console.WriteLine("Generating Follower...");
            GenerateFollower();
            Console.WriteLine("All Done!");
            Console.WriteLine();
        }

        /// <summary>
        /// Creates a new Person Object
        /// for each String in the first line
        /// </summary>
        private static void GenerateUser()
        {
            string[] names = fr.GetAllUsernames();
            foreach (string name in names)
            {
                new Person(name);
            }
        }

        //TODO  check both seperatly with multy threading
        private static void GenerateFollower()
        {
            List<string[]> relations = fr.GetRelations();
            //go through all relations and find the objects they refer to
            foreach (string[] person in relations)
            {
                //first name from txt equals to one of the objects
                foreach (Person p1 in Person.AllUsers)
                {
                    if (person[0].Equals(p1.Name))
                    {
                        //second name from txt equals to one of the objects
                        foreach (Person p2 in Person.AllUsers)
                        {
                            if (person[1].Equals(p2.Name))
                            {
                                p2.AddFollower(p1);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Finds the Superstar between all Person objects
        /// </summary>
        /// <returns> Superstar </returns>
        public static Person FindSuperstar() => FindSuperstar(new List<Person>(Person.AllUsers));

        private static Person FindSuperstar(List<Person> listOf_PersonB)
        {
            try
            {
                foreach (Person personA in listOf_PersonB)
                {
                    if (listOf_PersonB.Count != 1)
                    {
                        for (int i = listOf_PersonB.IndexOf(personA) + 1; i < listOf_PersonB.Count; i++)
                        {
                            if (personA.Name != listOf_PersonB[i].Name)
                            {
                                if (A_follows_B(personA, listOf_PersonB[i]))
                                {
                                    Console.WriteLine("entferne: " + personA.Name + "\n");
                                    listOf_PersonB.Remove(personA);
                                    //restart because the list size changed
                                    return FindSuperstar(new List<Person>(listOf_PersonB));
                                }
                                else
                                {
                                    Console.WriteLine("entferne: " + listOf_PersonB[i].Name + "\n");
                                    listOf_PersonB.Remove(listOf_PersonB[i]);
                                    //restart because the list size changed
                                    return FindSuperstar(new List<Person>(listOf_PersonB));
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(personA.Name + " ist wahrscheinlich der superstar!"
                            + "\nÜberprüfe verdacht...\n"
                            );
                        Console.WriteLine(personA.Name +
                            " Index: " + Person.AllUsers.IndexOf(personA));
                        Console.WriteLine("Anzahl aller Benutzer: " + Person.AnzahlUser);

                        bool valid = ValidateStar(personA);
                        if (valid)
                        {
                            //Console.WriteLine(personA.Name + " ist der Superstar");
                            Console.WriteLine("Kosten in Euro:" + cost);
                            return personA;
                        }
                        else
                        {
                            Console.WriteLine(personA.Name + " ist nicht der Superstar");
                            Console.WriteLine("Kosten in Euro:" + cost);
                            return null;
                        }
                    }
                }
            }
            catch (System.InvalidOperationException)
            {
                return FindSuperstar(new List<Person>(listOf_PersonB));
            }

            //niemand ist der Superstar
            return null;
        }

        private static bool ValidateStar(Person person)
        {
            //first validation
            if (EveryOneFollows(person))
            {
                Console.WriteLine("Alle folgen " + person.Name + "\n");
                //second validation
                if (FollowsNoOne(person))
                {
                    Console.WriteLine("\n" + person.Name + " folgt keinem!\n");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{person.Name} folgt jemandem!");
                }
            }
            else {
                Console.WriteLine($"Nicht alle folgen {person.Name}!");
            }
            return false;
        }

        private static bool FollowsNoOne(Person person)
        {
            for (int i = 0; i < Person.AnzahlUser; i++)
            {
                if (person.Name != Person.AllUsers[i].Name)
                {
                    if (!person.IsInspected(Person.AllUsers[i]))
                    {
                        if (A_follows_B(person, Person.AllUsers[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static bool EveryOneFollows(Person star)
        {
            foreach (Person B in Person.AllUsers)
            {
                if (!star.Equals(B))
                {
                    if (!B.IsInspected(star))
                    {
                        if (!A_follows_B(B, star))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static bool A_follows_B(Person a, Person b)
        {
            cost++;
            Console.WriteLine("Aufruf-Nummer: " + cost);
            Console.WriteLine("folgt: {0} : {1} ? [{2}]"
                                , a.Name
                                , b.Name,
                                  a.Follows(b)
                                );

            a.Inspected.Add(b);
            return a.Follows(b);
        }
    }
}