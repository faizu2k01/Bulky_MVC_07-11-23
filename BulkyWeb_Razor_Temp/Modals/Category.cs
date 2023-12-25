using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWeb_Razor_Temp.Modals
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]


        [MaxLength(25, ErrorMessage = "Don't enter more charactors then 25")]
        [DisplayName("Category Name")]
        public String Name { get; set; }

        [Range(1, 100, ErrorMessage = "Don't enter more values then 100")]
        [DisplayName("Display Order")]
        public int DisplayName { get; set; }
    }
}
