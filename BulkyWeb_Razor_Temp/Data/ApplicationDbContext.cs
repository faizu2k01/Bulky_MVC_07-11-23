using BulkyWeb_Razor_Temp.Modals;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb_Razor_Temp.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt):base(opt)
        {
            

        }


       public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId=1,
                    Name="Action",
                    DisplayName=1
                },
                new Category
                {
                    CategoryId=2,
                    Name="Sci-Fi",
                    DisplayName=2
                },
                new Category
                {
                    CategoryId=3,
                    Name="History",
                    DisplayName=3
                }
            );
        }
    }
}
