using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface IUnitOfWork
    {
        public  IApplicationUserRepository ApplicationUserRepo { get; set; }
        public ICategoryRepository Category { get; }
        public IProductReporistory Product { get;  }

        public ICompanyRepository Company { get; }
        public IShoppingCartRepository ShoppingCart { get; }
        public IOrderDetailRepository OrderDetail { get; set; }
        public IOrderHeaderRepository OrderHeader { get; set; }
        public void Save();
    }
}
