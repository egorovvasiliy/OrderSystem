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
        public override void HandleOrder(ref Order _order) {
            var orderProducts = JsonSerializer.Deserialize<OrderProducts>(_order.source_order);
            foreach (var product in orderProducts.products.ToList()) {
                var price = int.Parse(product.paidPrice);
                product.paidPrice = (price > 0 ? -price : price).ToString();
            }
            _order.converted_order = JsonSerializer.Serialize(orderProducts);
        }
    }
}
