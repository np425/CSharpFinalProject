namespace CSharpFinalProject.Data;

// TODO: Models for server side entity framework
// TODO: data for client side display
// TODO: service retrieves data from API

public class UserService
{
    public Task<User[]> GetUsersAsync()
    {
        return Task.FromResult(new User[]
        {
            new("jack"),
            new("james")
        });
    }
}
