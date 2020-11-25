using BLL.Models;
using System;

namespace BLL.OrderService
{
    public class TalabatServiceHandler:AbstractServiceHandler
    {
        public override System_type System_Type => System_type.talabat;
        public override void HandleOrder(OrderProducts _orderProducts)
        {
            throw new NotImplementedException();
        }
    }
}
