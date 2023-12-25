using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public IProductReporistory Product { get;  }
        public void Save();
    }
}
