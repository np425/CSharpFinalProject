using ApiData.Models;
using Backend.Models;

namespace Backend.Helpers;

public class UserConversion: IDtoConversion<User, UserDto>
{
    public UserDto ToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email ?? "",
            Username = user.Username ?? ""
        };
    }

    public User ToInternal(UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Email = userDto.Email,
            Username = userDto.Username
        };
    }

    public void LoadDtoData(User internalData, UserDto dtoData)
    {
        internalData.Id = dtoData.Id;
        internalData.Email = dtoData.Email;
        internalData.Username = dtoData.Username;
    }
}
