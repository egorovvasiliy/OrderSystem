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
        public override string HandleOrder(string _source_order)
        {
            var orderProducts = JsonSerializer.Deserialize<OrderProducts>(_source_order);
            foreach (var product in orderProducts.products.ToList())
                product.paidPrice = (int.Parse(product.paidPrice) / int.Parse(product.quantity)).ToString();
            return JsonSerializer.Serialize(orderProducts);
        }
    }
}
