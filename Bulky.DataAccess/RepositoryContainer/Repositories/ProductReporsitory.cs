using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Modals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.Repositories
{
    public class ProductReporsitory : Repository<Product>, IProductReporistory
    {
        private readonly ApplicationDbContext _db;
    
        public ProductReporsitory(ApplicationDbContext db):base(db)
        {
            _db = db;
          
        }  
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Updata(Product pro)
        {
            var productObj = _db.Products.FirstOrDefault(x => x.Id == pro.Id);
            if(productObj != null)
            {
                productObj.Description = pro.Description;
                productObj.ISBN = pro.ISBN;
                productObj.Author = pro.Author;
                productObj.Price100 = pro.Price100;
                productObj.Price = pro.Price;
                productObj.ListPrice = pro.ListPrice;
                productObj.Price50 = pro.Price50;
                productObj.Title = pro.Title;
                productObj.CategoryId = pro.CategoryId;
                if(productObj.ImageUrl != null)
                {
                    productObj.ImageUrl = pro.ImageUrl;
                }

                
            }

            
        }
    }
}
