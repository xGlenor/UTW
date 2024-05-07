namespace BaseLibrary.Models;

public class Fee : BaseEntity
{
    public int StudentId { get; set; }
    
    public DateTime IssueDate { get; set; }
    
    public DateTime? PaymentDate { get; set; }
    
    public string Details { get; set; }
    
    public bool? isPaid { get; set; }
    
    public Student Student { get; set; }
    
}