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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {

        private readonly ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderDetail item)
        {
            _db.Update(item);
        }
    }
}
