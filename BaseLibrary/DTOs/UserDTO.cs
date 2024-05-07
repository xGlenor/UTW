using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class UserDTO
{
    public string? Id { get; set; } = string.Empty;
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
}