namespace CSharpFinalProject.Data;

public class SeedData
{
    public static void Initialize(GradeSystemStoreContext db)
    {
        var subjects = new Subject[]
        {
            new()
            {
                Name = "Math",
                Description = "I hate this"
            },
            new()
            {
                Name = "Coding",
                Description = "It's okay"
            },
            new()
            {
                Name = "English",
                Description = "It's nice"
            }
        };
        
        db.Subjects.AddRange(subjects);
        db.SaveChanges();
    }
}
