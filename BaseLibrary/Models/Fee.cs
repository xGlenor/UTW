namespace BaseLibrary.Models;

public class Fee : BaseEntity
{
    public int UserId { get; set; }
    
    public DateTime IssueDate { get; set; }
    
    public DateTime? PaymentDate { get; set; }
    
    public string Details { get; set; }
    
    public bool? isPaid { get; set; }
    
    public User User { get; set; }
    
}