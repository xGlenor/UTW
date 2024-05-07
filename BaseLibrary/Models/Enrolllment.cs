namespace BaseLibrary.Models;


public class Enrolllment : BaseEntity
{
    public int UserId { get; set; }
    
    public int LessonId { get; set; }
    
    public Student Student { get; set; }
    
    public Lesson Lesson { get; set; }
    
}