using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context;

public class OnlineSchoolStoreContext: DbContext
{
    public OnlineSchoolStoreContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Subject> Subjects { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<SubjectMember> SubjectsMembers { get; set; } = null!;
}
