using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
            _dbSet = db.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? IncludeProps = null, bool types = false)
        {
            IQueryable<T> query;
            if (!types)
            {
                query = _dbSet.AsQueryable().AsNoTracking();
            }
            else
            {
                query = _dbSet.AsQueryable();
            }
            query = query.Where(filter);
            if (IncludeProps != null)
            {
                foreach (var porp in IncludeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(porp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? IncludeProps = null)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if (filter != null) { query.Where(filter); }
            if(IncludeProps != null)
            {
                foreach(var porp in IncludeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(porp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }
    }
}
