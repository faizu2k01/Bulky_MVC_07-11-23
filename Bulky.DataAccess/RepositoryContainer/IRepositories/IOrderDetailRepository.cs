using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.Repositories;
using Bulky.Modals;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Save();
        void Update(OrderDetail item);
    }
}
