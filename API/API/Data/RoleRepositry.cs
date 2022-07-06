using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RoleRepositry : IRoleRepositry
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public RoleRepositry(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Roles> GetRolesByIDAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<Roles> GetRolesByNameAsync(string rolename)
        {
            return await _context.Roles.SingleOrDefaultAsync(x => x.RoleName.ToLower() == rolename.ToLower());
        }
    }
}
