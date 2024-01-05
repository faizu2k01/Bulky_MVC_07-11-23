using Bulky.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.IRepositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Save();
        void Update(Company item);
    }
}
