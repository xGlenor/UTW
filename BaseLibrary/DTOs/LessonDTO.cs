namespace BaseLibrary.DTOs;

public class LessonDTO
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime LessonDate { get; set; }
    
    public string? Classroom { get; set; }
    
    public string? Address { get; set; }
    
    public int? NumberOfPlaces { get; set; }
    
    public decimal? Price { get; set; }

    public int? SessionId { get; set; }
    
    public string Session { get; set; }   
}