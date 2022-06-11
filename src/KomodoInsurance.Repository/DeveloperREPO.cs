using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class DeveloperREPO
    {
        private readonly List<Developer> _developerDatabase = new List<Developer>();

        private int _count = 0;

        public bool AddDeveloperToDatabase(Developer developer)
        {
            if(developer != null)
            {
                _count++;
                developer.ID = _count;
                _developerDatabase.Add(developer);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Developer> GetAllDevelopers()
        {
            return _developerDatabase;
        }

        public Developer GetDeveloperByID(int id)
        {
            foreach(Developer d in _developerDatabase)
            {
                if(d.ID == id)
                {
                    return d;
                }
            }

            return null;
        }

    }
    
