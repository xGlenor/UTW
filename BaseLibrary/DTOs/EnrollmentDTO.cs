using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class EnrollmentDTO
{
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public int LessonId { get; set; }
    
}