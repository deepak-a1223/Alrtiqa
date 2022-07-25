namespace API.Entities
{
    public class RelationShipType
    {
        public int RelationShipTypeId { get; set; }
        public string RelationType { get; set; }
        public string Description { get; set; }
        public ICollection<Person> Persons { get; set; }
    }
}
