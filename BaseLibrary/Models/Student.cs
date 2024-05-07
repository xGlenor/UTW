namespace BaseLibrary.Models;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Address { get; set; }
    
    public DateOnly Birthdate { get; set; }
}