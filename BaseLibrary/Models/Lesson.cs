namespace BaseLibrary.Models;

public class Lesson : BaseEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime LessonDate { get; set; }
    
    public string? Classroom { get; set; }
    
    public string? Address { get; set; }
    
    public int? NumberOfPlaces { get; set; }
    
    public decimal? Price { get; set; }

    public int? SessionId { get; set; }
    
    public Session? Session { get; set; }
    
    public ICollection<Enrolllment>? Enrolllments { get; set; } = new List<Enrolllment>();
    public ICollection<ApplicationUser>? Teachers { get; set; } = new List<ApplicationUser>();
}