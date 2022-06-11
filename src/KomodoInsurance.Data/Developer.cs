using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Developer
{
    public Developer(){}
    public Developer(int id, string firstName, string lastName, Pluralsight pluralsight)
    {
        ID = id;
        FirstName = firstName;
        LastName = lastName;
        Pluralsight = pluralsight;
    }

    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Pluralsight Pluralsight { get; set; }
    public bool Access 
    {
        get
        {
            switch(Pluralsight)
            {
                case Pluralsight.Has_Access:
                    return true;
                case Pluralsight.No_Access:
                default:
                    return false;
            }
        }
    }

}
