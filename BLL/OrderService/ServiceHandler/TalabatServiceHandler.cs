using BLL.Models;
using DAL.Entities;
using System;
using System.Linq;
using System.Text.Json;

namespace BLL.OrderService
{
    public class TalabatServiceHandler:AbstractServiceHandler
    {
        public override System_type System_Type => System_type.talabat;
        public override string HandleOrder(string _source_order)
        {
            var orderProducts = JsonSerializer.Deserialize<OrderProducts>(_source_order);
            foreach (var product in orderProducts.products.ToList())
            {
                var price = int.Parse(product.paidPrice);
                product.paidPrice = (price > 0 ? -price : price).ToString();
            }
            return JsonSerializer.Serialize(orderProducts);
        }
    }
}
