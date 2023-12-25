using BulkyWeb_Razor_Temp.Data;
using BulkyWeb_Razor_Temp.Modals;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public List<Category> CategoryList;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
