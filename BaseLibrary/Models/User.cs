using BaseLibrary.enums;



namespace BaseLibrary.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Address { get; set; }
    
    public DateOnly Birthdate { get; set; }
    
    public string? Skills { get; set; }
    
}