using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class DevTeam
    {
        public DevTeam(){}
        public DevTeam(string name)
        {
            Name = name;
        }
        public DevTeam(string name, List<Developer> developer)
        {
            Name = name;
            Developer = developer;
        }

        public int TeamID { get; set; }
        public string Name { get; set; }
        public List<Developer> Developer { get; set; } = new List<Developer>();
    }
