using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class FeeDTO
{
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public DateTime IssueDate { get; set; }
    
    [Required]
    public DateTime? PaymentDate { get; set; }
    
    [Required]
    public string Details { get; set; }
    
    [Required]
    public bool isPaid { get; set; }
    
    [Required]
    public string Student { get; set; }
}