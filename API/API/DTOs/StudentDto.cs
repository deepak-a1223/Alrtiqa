using API.Entities;

namespace API.DTOs
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public int Cpr { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public DateTime DOB { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Gender { get; set; }
        public int? PhoneNumber { get; set; }
        public int? HomePhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public DateTime? DOJ { get; set; }
        public ICollection<Person>? Persons { get; set; }
        public ICollection<StudentAddress>? Addresses { get; set; }
        public StudentFamilyInfo? StudentFamily { get; set; }
    }
}
