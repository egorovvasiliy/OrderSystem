using BLL.Models;
using DAL.Entities;
using System;

namespace BLL.OrderService
{
    public class UberServiceHandler : AbstractServiceHandler
    {
        public override System_type System_Type => System_type.uber;
        public override void HandleOrder(ref Order _order)
        {
            throw new NotImplementedException();
        }
    }
}
