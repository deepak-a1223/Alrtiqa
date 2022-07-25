﻿namespace API.Entities
{
    public class StudentAddress
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
