using GRS.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRS.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);
        List<T> List();
        List<T> List(ISpecification<T> spec);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
