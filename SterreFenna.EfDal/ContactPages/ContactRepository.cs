using SterreFenna.Domain.Contacts;

namespace SterreFenna.EfDal.ContactPages
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(SFContext context)
            :base(context)
        {
        }
    }
}