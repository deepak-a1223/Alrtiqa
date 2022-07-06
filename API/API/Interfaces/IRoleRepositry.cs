using API.Entities;

namespace API.Interfaces
{
    public interface IRoleRepositry
    {
        Task<Roles> GetRolesByIDAsync(int roleId);
        Task<Roles> GetRolesByNameAsync(string rolename);
    }
}
