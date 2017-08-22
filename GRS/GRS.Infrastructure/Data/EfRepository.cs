using GRS.ApplicationCore.Entities;
using GRS.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GRS.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly GRSContext _dbContext;

        public EfRepository(GRSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public List<T> List()
        {
            return _dbContext.Set<T>().ToList();
        }

        public List<T> List(ISpecification<T> spec)
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                            (current, include) => current.Include(include));
            return queryableResultWithIncludes
                            .Where(spec.Criteria)
                            .ToList();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
