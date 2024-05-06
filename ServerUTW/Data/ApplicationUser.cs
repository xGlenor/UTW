using BaseLibrary.Models;
using Microsoft.AspNetCore.Identity;

namespace ServerLibrary.Data;

public class ApplicationUser : IdentityUser{
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Address { get; set; }
    
    public DateOnly Birthdate { get; set; }
    
    public string? Skills { get; set; }
    
}