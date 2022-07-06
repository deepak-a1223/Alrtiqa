namespace API.Entities
{
    public class Roles
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public List<AppUser> Users { get; set; }
    }
}
