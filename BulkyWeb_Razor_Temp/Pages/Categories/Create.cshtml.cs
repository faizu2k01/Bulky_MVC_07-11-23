using BulkyWeb_Razor_Temp.Data;
using BulkyWeb_Razor_Temp.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb_Razor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public Category Category { get; set; }
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {

            if (Category.Name == Category.DisplayName.ToString())
            {
                ModelState.AddModelError("Name", "Category name should not be same as display order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";

                return RedirectToPage("Index");
            }


            return Page();

        }
    }
}
