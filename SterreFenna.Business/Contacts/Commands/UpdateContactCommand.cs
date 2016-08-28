using SterreFenna.Domain.Contacts;
using SterreFenna.EfDal;
using System.Linq;

namespace SterreFenna.Business.Contacts.Commands
{
    public class UpdateContactCommand
    {
        public string ContactMe { get; set; }

        public string AboutMe { get; set; }

        public string Education { get; set; }
    }

    public class UpdateContactCommandHandler
    {
        private readonly SFContext _context;

        public UpdateContactCommandHandler(SFContext context)
        {
            _context = context;
        }

        public void Handle(UpdateContactCommand command)
        {
            var contact = GetContact();
            contact.ContactMe = command.ContactMe;
            contact.AboutMe = command.AboutMe;
            contact.Education = command.Education;

            _context.SaveChanges();
        }

        private Contact GetContact()
        {
            var contact = _context.Contacts.FirstOrDefault();

            if (contact == null)
            {
                contact = new Contact();
                _context.Contacts.Add(contact);
            }

            return contact;
        }
    }
}