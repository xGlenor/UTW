using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class StudentDTO
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    public string Address { get; set; }
    
    [Required]
    public string Telephone { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    public DateOnly? Birthdate { get; set; }
    
    public bool IsEnrolled { get; set; }
}