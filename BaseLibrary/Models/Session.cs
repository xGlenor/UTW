using BaseLibrary.enums;

namespace BaseLibrary.Models;

public class Session : BaseEntity
{
    public int SessionYear { get; set; }
    
    public SessionType SessionType { get; set; }
    
    public ICollection<Student>? Students { get; set; } = new List<Student>();
    
    public ICollection<Lesson>? Lessons { get; set; } = new List<Lesson>();
}