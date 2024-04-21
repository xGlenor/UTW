﻿namespace BaseLibrary.Models;

public class Lesson
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime LessonDate { get; set; }
    
    public string? Classroom { get; set; }
    
    public string? Address { get; set; }
    
    public int? NumberOfPlaces { get; set; }
    
    public decimal? Price { get; set; }
    
    public ICollection<User>? Teachers { get; set; } = new List<User>();
}