using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Modals
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "Don't enter more charactors then 25")]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Don't enter more values then 100")]
        [DisplayName("Display Order")]
        public int DisplayName { get; set; }
    }
}
