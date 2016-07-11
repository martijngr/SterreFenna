using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SterreFenna.Domain.Projects
{
    public interface IProjectRepository
    {
        void Add(Project project);

        Project GetById(int id);

        IEnumerable<Project> Find(Expression<Func<Project, bool>> predicate);

        IEnumerable<Project> GetAll();

        Project GetByUniqueName(string uniqueName);
    }
}