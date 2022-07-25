namespace API.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public int Cpr { get; set; }
        public string Nationality { get; set; }
        public string Qualification { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int RelationShipTypeId { get; set; }
        public RelationShipType Relation { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
