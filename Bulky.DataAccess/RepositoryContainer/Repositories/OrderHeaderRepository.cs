using Bulky.DataAccess.Data;
using Bulky.DataAccess.RepositoryContainer.IRepositories;
using Bulky.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.RepositoryContainer.Repositories
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {

        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(OrderHeader item)
        {
            _db.Update(item);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
            var orderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

            if(orderHeader != null)
            {
                orderHeader.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderHeader.PaymentStatus = paymentStatus;
                }
            }
		}

		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
		{
			var orderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

			if (orderHeader != null)
			{
                if (!string.IsNullOrEmpty(sessionId))
                {

				orderHeader.SessionId = sessionId;
                }
				if (!string.IsNullOrEmpty(paymentIntentId))
				{
					orderHeader.PaymentIntentId = paymentIntentId;
				}
			}
		}
	}
}
