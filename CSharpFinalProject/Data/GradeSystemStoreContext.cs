using Microsoft.EntityFrameworkCore;
namespace CSharpFinalProject.Data;

public class GradeSystemStoreContext: DbContext
{
    public GradeSystemStoreContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Subject> Subjects { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<StudentGrade> StudentGrades { get; set; }
    
    public DbSet<SubjectMember> SubjectsMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
