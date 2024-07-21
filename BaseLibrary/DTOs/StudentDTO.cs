using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class StudentDTO
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }

    [Required]
    public string Address { get; set; }
    
    [Required]
    public DateOnly Birthdate { get; set; }
}