using BaseLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ServerUTW.Data;

public class AppDbContext: IdentityDbContext<IdentityUser>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Enrolllment> Enrolllments { get; set; }
    
    public DbSet<Fee> Fees { get; set; }
    
    public DbSet<Lesson> Lessons { get; set; }
    
    public DbSet<Session> Sessions { get; set; }
    
    public DbSet<Session> Students { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
}