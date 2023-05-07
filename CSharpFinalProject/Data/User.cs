namespace CSharpFinalProject.Data;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    public User(string username, string email, int id)
    {
        Username = username;
        Email = email;
        Id = id;
    }
}
