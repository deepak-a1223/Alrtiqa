using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepositry
    {
        Task<AppUserDto> GetUserByNameAsync(string username);
        Task<IEnumerable<AppUserDto>> GetAllUsers();
    }
}
