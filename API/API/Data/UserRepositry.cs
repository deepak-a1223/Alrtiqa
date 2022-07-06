using API.DTOs;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepositry : IUserRepositry
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UserRepositry(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUserDto>> GetAllUsers()
        {
            return await _context.Users
                          .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
                          .ToListAsync();
        }

        public async Task<AppUserDto> GetUserByNameAsync(string username)
        {
            return await _context.Users
                          .Where(x => x.UserName == username && x.IsActive == true)
                          .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
                          .SingleOrDefaultAsync();
        }
    }
}
