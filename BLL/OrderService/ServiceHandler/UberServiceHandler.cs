using BLL.Models;
using System;

namespace BLL.OrderService
{
    public class UberServiceHandler : AbstractServiceHandler
    {
        public override System_type System_Type => System_type.uber;
        public override void HandleOrder(OrderProducts _orderProducts)
        {
            throw new NotImplementedException();
        }
    }
}
