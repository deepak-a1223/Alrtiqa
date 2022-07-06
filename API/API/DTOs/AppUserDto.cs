using API.Entities;

namespace API.DTOs
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public Roles Roles { get; set; }
        public bool IsActive { get; set; }
    }
}
