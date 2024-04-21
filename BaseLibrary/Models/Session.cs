using BaseLibrary.enums;

namespace BaseLibrary.Models;

public class Session
{
    public int Id { get; set; }
    
    public int SessionYear { get; set; }
    
    public SessionType SessionType { get; set; }
    
    public ICollection<User>? Students { get; set; } = new List<User>();
    
    public ICollection<Lesson>? Lessons { get; set; } = new List<Lesson>();
}