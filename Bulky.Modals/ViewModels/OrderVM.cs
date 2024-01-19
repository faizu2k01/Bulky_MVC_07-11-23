using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Modals.ViewModels
{
	public class OrderVM
	{
        public OrderHeader OrderHeader { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
