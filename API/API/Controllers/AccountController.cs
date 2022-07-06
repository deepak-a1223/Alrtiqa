using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokeService;
        private readonly IUserRepositry _userRepositry;
        private readonly IRoleRepositry _roleRepositry;
        public AccountController(AppDbContext context, ITokenService tokenService, IUserRepositry userRepositry, IRoleRepositry roleRepositry)
        {
            _context = context;
            _tokeService = tokenService;
            _userRepositry = userRepositry;
            _roleRepositry = roleRepositry;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await UserPendingAproval(register.UserName))
            {
                return BadRequest("UserName pending approval");
            }
            else if (await UserExists(register.UserName))
            {
                return BadRequest("UserName is taken");
            }
            else
            {
                var role = await _roleRepositry.GetRolesByNameAsync(register.RoleName);
                if (role == null) return BadRequest("Invalid Role Name");
                using (var hmac = new HMACSHA512())
                {
                    var user = new AppUser
                    {
                        UserName = register.UserName.ToLower(),
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        IsActive = false,
                        RoleId = role.Id,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                        PasswordSalt = hmac.Key
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return Ok(new UserDto
                    {
                        UserName = register.UserName.ToLower(),
                        Token = ""
                    });
                }
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userRepositry.GetUserByNameAsync(loginDto.UserName);
            
            if (user == null) return Unauthorized("Invalid UserName or not yet approved");
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
                }
            }
            return Ok(new UserDto
            {
                UserName = loginDto.UserName.ToLower(),
                RoleName = user.Roles.RoleName,
                Token = _tokeService.CreateToken(user)
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("approve/{username}")]
        public async Task<ActionResult> Approve(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
            if (user == null) return Unauthorized("Invalid UserName");
            user.IsActive = true;

            _context.Entry(user).State = EntityState.Modified;

            if (await _context.SaveChangesAsync() > 0) return NoContent();

            return BadRequest("Failed to approve user");
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }

        private async Task<bool> UserPendingAproval(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower() && x.IsActive == false);
        }
    }
}
