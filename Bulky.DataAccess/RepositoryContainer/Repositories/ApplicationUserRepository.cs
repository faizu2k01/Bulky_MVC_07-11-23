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
    public class ApplicationUserRepository : Repository<ApplicationUser>,IApplicationUserRepository
    {

        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
       
    }
}
