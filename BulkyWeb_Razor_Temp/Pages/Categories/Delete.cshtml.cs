using BulkyWeb_Razor_Temp.Data;
using BulkyWeb_Razor_Temp.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public Category Category { get; set; }
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if(id != null)
            {
            Category = _db.Categories.FirstOrDefault(x => x.CategoryId == id);
            }
        }

        public IActionResult OnPost()
        {
           
            _db.Categories.Remove(Category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";


            return RedirectToPage("Index");
        }
    }
}
