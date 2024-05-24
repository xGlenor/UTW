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
    
    public DbSet<Student> Students { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Enrolllment>().ToTable("Enrolllments");
        modelBuilder.Entity<Fee>().ToTable("Fees");
        modelBuilder.Entity<Lesson>().ToTable("Lessons");
        modelBuilder.Entity<Session>().ToTable("Sessions");
        modelBuilder.Entity<Student>().ToTable("Students");
        base.OnModelCreating(modelBuilder);
    }
    
}