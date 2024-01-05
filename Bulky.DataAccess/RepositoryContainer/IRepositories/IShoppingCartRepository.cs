using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.Repositories;
using Bulky.Modals;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Save();
        void Update(ShoppingCart item);
    }
}
