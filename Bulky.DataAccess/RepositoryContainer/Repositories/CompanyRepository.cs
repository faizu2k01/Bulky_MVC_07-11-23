using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.Repositories
{
    public class CompanyRepository :Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

       public CompanyRepository(ApplicationDbContext db):base(db) { _db = db; }
       

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Company item)
        {
            _db.Update(item);
        }
    }
}
