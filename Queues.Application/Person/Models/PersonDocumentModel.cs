namespace Queues.Application.Person.Models
{
    public class PersonDocumentModel
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string IdentificationNumber { get; set; } = null!;
        public char Gender { get; set; }
    }
}
