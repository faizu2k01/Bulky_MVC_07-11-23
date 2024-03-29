﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface IRepository<T> where T : class
    {

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? IncludeProps = null);
        public T Get(Expression<Func<T, bool>> filter, string? IncludeProps = null,bool types=false);
        public void Add(T entity);
        public void Remove(T entity);
        public void RemoveRange(IEnumerable<T> items);
    }
}
