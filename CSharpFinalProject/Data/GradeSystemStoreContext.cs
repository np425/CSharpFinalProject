using Microsoft.EntityFrameworkCore;
namespace CSharpFinalProject.Data;

public class GradeSystemStoreContext: DbContext
{
    public GradeSystemStoreContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Subject> Subjects { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Grade> Grades { get; set; }
    
    public DbSet<SubjectMember> SubjectsMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SubjectMember>().HasKey(member => new
        {
            member.SubjectId,
            member.UserId
        });

        // modelBuilder.Entity<User>()
        //     .HasMany(u => u.Subjects)
        //     .WithMany(s => s.Users)
        //     .UsingEntity<SubjectMember>();
    }
}
