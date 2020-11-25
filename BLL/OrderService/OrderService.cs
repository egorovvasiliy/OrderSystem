using BLL.Models;
using System;

namespace BLL.OrderService
{
    public class OrderService
    {
        AbstractServiceHandler handler;
        public void SetServiceHandler(AbstractServiceHandler _handler) {
            handler = _handler;
        }
        public void HandleOrder(OrderProducts orderProducts)
        {
            handler?.HandleOrder(orderProducts);
        }
    }
}
