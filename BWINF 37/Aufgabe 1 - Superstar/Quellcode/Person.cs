using System.Collections.Generic;

namespace Bwinf_Aufgabe_1
{
    internal class Person
    {
        public static List<Person> AllUsers { get; private set; } = new List<Person>();
        public static int AnzahlUser => AllUsers.Count;
        public string Name { get; private set; }
        public List<Person> HeFollows { get; private set; } = new List<Person>();
        private List<Person> Follower { get; set; } = new List<Person>();
        public List<Person> Inspected { get; set; } = new List<Person>();

        public Person()
        {
            AllUsers.Add(this);
        }

        public Person(string name)
        {
            if (!(name is null))
            {
                Name = name;
                AllUsers.Add(this);
            }
        }

        public void AddFollows(Person person)
        {
            HeFollows.Add(person);
            person.Follower.Add(this);
        }

        public void AddFollower(Person person)
        {
            Follower.Add(person);
            person.HeFollows.Add(this);
        }

        /// <summary>
        /// Returns a Boolean
        /// if this object follows
        /// the person in the parameter
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool Follows(Person person)
        {
            foreach (Person p in HeFollows)
            {
                if (p.Name.Equals(person.Name))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a string array where
        /// each string says something like:
        /// "A follows B"
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllFollows()
        {
            List<string> relations = new List<string>();
            foreach (Person b in AllUsers)
            {
                foreach (Person a in b.Follower)
                {
                    relations.Add(a.Name + " follows " + b.Name);
                }
            }
            return relations.ToArray();
        }

        //TODO ineffizient aber gets the job done
        public bool IsInspected(Person person)
        {
            foreach (Person p in Inspected)
            {
                if (p.Equals(person))
                {
                    return true;
                }
            }
            return false;
        }
    }
}