namespace BaseLibrary.Models;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string Telephone { get; set; }
    
    public string Email { get; set; }
    public DateOnly? Birthdate { get; set; }
    
    public bool IsEnrolled { get; set; }

    public ICollection<Enrolllment>? Enrolllments { get; set; } = new List<Enrolllment>();
}