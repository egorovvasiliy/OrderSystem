using BLL.Models;
using DAL;
using DAL.Entities;
using System;
using System.Linq;
using System.Text.Json;

namespace BLL.OrderService
{
    public class ZomatoServiceHandler : AbstractServiceHandler
    {
        public override System_type System_Type => System_type.zomato;
        public override void HandleOrder(OrderProducts _orderProducts) {
            var orderProducts = _orderProducts;
            foreach (var product in orderProducts.products.ToList()) {
                var price = int.Parse(product.paidPrice);
                product.paidPrice = (price > 0 ? -price : price).ToString();
            }
            using (var db = new OrderDbContext()) //this.OrderDbContext
            {
                var order = new Order()
                {
                    order_number = int.Parse(orderProducts.orderNumber),
                    system_type = System_Type.ToString(),
                    source_order = JsonSerializer.Serialize(orderProducts),
                    created_at = DateTime.Now
                };

                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
    }
}
