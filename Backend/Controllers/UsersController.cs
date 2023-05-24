using ApiData.Models;
using Backend.Context;
using Backend.Helpers;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController
{
    private readonly OnlineSchoolStoreContext _db;
    private readonly UserConversion _userConversion;

    public UsersController(OnlineSchoolStoreContext db)
    {
        _db = db;
        _userConversion = new UserConversion();
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        return await _db.Users.Select(user => _userConversion.ToDto(user)).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult?> PostUser([FromBody]UserDto userDto)
    {
        _db.Users.Add(_userConversion.ToInternal(userDto));
        await _db.SaveChangesAsync();

        return null;
    }

    [HttpPut("{userId:int}")]
    public async Task<ActionResult?> PutUser(int userId, [FromBody]UserDto userDto)
    {
        if (userId != userDto.Id)
        {
            return null;
        }

        var item = await _db.Users.FindAsync(userId);
        if (item == null)
        {
            return null;
        }

        _userConversion.LoadDtoData(item, userDto);
        _db.Entry(item).State = EntityState.Modified;

        await _db.SaveChangesAsync();

        return null;
    }

    [HttpDelete("{userId:int}")]
    public async Task<ActionResult?> DeleteUser(int userId)
    {
        var item = await _db.Users.FindAsync(userId);
        if (item == null)
        {
            return null;
        }

        _db.Users.Remove(item);
        await _db.SaveChangesAsync();

        return null;
    }
}
