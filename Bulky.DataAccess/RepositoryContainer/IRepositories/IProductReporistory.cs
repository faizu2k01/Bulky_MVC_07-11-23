using Bulky.DataAccess.RepositoryContainer.Repositories;
using Bulky.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface IProductReporistory : IRepository<Product>
    {
        void Save();

        void Updata(Product pro);
    }
}
