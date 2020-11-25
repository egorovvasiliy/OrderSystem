using DAL.Entities;
using System;

namespace BLL.OrderService
{
    public class TalabatServiceHandler:AbstractServiceHandler
    {
        public override System_type System_Type => System_type.talabat;
        public override void HandleOrder(ref Order _order)
        {
            throw new NotImplementedException("TalabatServiceHandler");
        }
    }
}
