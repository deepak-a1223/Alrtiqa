using API.Data;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokeService;
        private readonly IUserRepositry _userRepositry;
        public UsersController(AppDbContext context, ITokenService tokenService, IUserRepositry userRepositry)
        {
            _context = context;
            _tokeService = tokenService;
            _userRepositry = userRepositry;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetAllUsers()
        {
            var users = await _userRepositry.GetAllUsers();
            return Ok(users);
        }
    }
}
