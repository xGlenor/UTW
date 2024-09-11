namespace BaseLibrary.Models;

public class TeacherEnrollment : BaseEntity
{
    public string TeacherId { get; set; }
    
    public int LessonId { get; set; }
    
    public ApplicationUser Teacher { get; set; }
    
    public Lesson Lesson { get; set; }
    
}