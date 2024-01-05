using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.Repositories;
using Bulky.Modals;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Save();
        void Update(OrderHeader item);
    }
}
