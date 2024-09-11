namespace BaseLibrary.DTOs;

public class ApplicationUserDTO
{
    public string Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Address { get; set; }
    
    public string Email { get; set; }
    
    public DateOnly Birthdate { get; set; }
    
    public string? Skills { get; set; }
    
    public int? Code { get; set; }
}