namespace SterreFenna.Domain.Contacts
{
    public class Contact : IIdentifyable
    {
        public int Id { get; set; }

        public string ContactMe { get; set; }

        public string AboutMe { get; set; }

        public string Education { get; set; }
    }
}