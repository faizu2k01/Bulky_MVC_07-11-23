using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Modals
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        public int Count { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Products { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUsers { get; set; }

        [NotMapped]
        public double Price { get; set; }

    }
}
