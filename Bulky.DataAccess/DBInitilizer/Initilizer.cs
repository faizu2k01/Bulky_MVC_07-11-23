using Bulky.DataAccess.Data;
using Bulky.Modals;
using Bulky.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.DBInitilizer
{
    public class Initilizer : IInitilizer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public Initilizer(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initilize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }



            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();

               var result =  _userManager.CreateAsync(new ApplicationUser
                {
                    UserName="AdminUser",
                    Email = "adminUser@gmail.com",
                    Name="Faizul",
                    PhoneNumber="3424582",
                    StreetName = "Somewhere in india",
                    State="IL",
                    City = "Some",
                    PostalCode=4438493,

                },"admin123!A").GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "adminUser@gmail.com");
                    var res = _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }
              

            }

            return;
        }

        
    }
}
