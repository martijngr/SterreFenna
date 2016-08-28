using SterreFenna.Business.Contacts.Views;
using SterreFenna.Domain;
using System.Linq;

namespace SterreFenna.Business.Contacts.Queries
{
    public class GetContactQuery
    {
    }

    public class GetContactQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetContactQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ContactView Handle(GetContactQuery query)
        {
            var result = _unitOfWork.ContactRepository.GetAll().FirstOrDefault();
            var view = new ContactView
            {
                ContactMe = result?.ContactMe,
                AboutMe = result?.AboutMe,
                Education= result?.Education
            };

            return view;
        }
    }
}