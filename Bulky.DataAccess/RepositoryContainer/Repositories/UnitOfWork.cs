using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _db;

        public IApplicationUserRepository ApplicationUserRepo { get; set; }
        public ICategoryRepository Category { get; private set; }
        public IProductReporistory Product { get; private set; }

        public ICompanyRepository Company { get; set; }
        public IShoppingCartRepository ShoppingCart { get; set; }
        public IOrderHeaderRepository OrderHeader { get; set; }
        public IOrderDetailRepository OrderDetail { get; set; }
        public UnitOfWork(ApplicationDbContext db)
        {
           
            _db = db;
            ApplicationUserRepo = new ApplicationUserRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductReporsitory(_db);
            Company = new CompanyRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db); 
            OrderDetail = new OrderDetailRepository(_db);

        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
