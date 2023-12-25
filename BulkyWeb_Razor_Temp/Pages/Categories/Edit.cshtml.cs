using BulkyWeb_Razor_Temp.Data;
using BulkyWeb_Razor_Temp.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public Category Category { get; set; }
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            Category = _db.Categories.FirstOrDefault(x => x.CategoryId == id);


        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
