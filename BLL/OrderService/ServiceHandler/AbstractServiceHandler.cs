using BLL.Models;
using DAL;
using System;

namespace BLL.OrderService
{
    public abstract class AbstractServiceHandler
    {
        OrderDbContext orderDbContext;
        public abstract System_type System_Type { get; }
        public OrderDbContext OrderDbContext
        {
            get {
                return (orderDbContext is null)
                    ? new OrderDbContext()
                    : orderDbContext;
            }
        }
        public abstract void HandleOrder(OrderProducts orderProducts);
    }
}
