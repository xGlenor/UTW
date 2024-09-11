using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.DTOs;

public class EnrollmentDTO
{
    [Required]
    public int StudentId { get; set; }
    
    [Required]
    public int LessonId { get; set; }
    
}

public class TeacherEnrollmentDto
{
    [Required]
    public string TeacherId { get; set; }
    
    [Required]
    public int LessonId { get; set; }
    
}