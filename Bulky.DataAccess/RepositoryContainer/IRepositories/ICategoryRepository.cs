using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.Repositories;
using Bulky.Modals;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Save();
        void Update(Category item);
    }
}
