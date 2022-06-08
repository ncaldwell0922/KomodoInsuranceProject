
public class Developer
{
    public Developer(){}
    public Developer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool PluralsightAccess { get; set; }
}
