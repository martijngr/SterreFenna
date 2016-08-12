namespace SterreFenna.Domain.Contacts
{
    public class Contact : IIdentifyable
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}