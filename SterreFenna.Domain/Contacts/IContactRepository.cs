using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SterreFenna.Domain.Contacts
{
    public interface IContactRepository
    {
        void Add(Contact project);

        Contact GetById(int id);

        IEnumerable<Contact> Find(Expression<Func<Contact, bool>> predicate);

        IEnumerable<Contact> GetAll();

        void RemoveRange(IEnumerable<Contact> serieItems);
    }
}