namespace API.Entities
{
    public class StudentFamilyInfo
    {
        public int Id { get; set; }
        public string? NumOfBrothers { get; set; }
        public string? NumOfSisters { get; set; }
        public string? MaidsNumber { get; set; }
        public string? UnEmployeeBrothers { get; set; }
        public string? HisNumberInFamily { get; set; }
        public string? FatherCondition { get; set; }
        public string? MotherCondition { get; set; }
        public string? ParentsCondition { get; set; }
        public string? FamilySituation { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
