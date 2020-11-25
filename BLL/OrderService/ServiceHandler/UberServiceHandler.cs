using BLL.Models;
using DAL.Entities;
using System;

namespace BLL.OrderService
{
    public class UberServiceHandler : AbstractServiceHandler
    {
        public override System_type System_Type => System_type.uber;
        public override string  HandleOrder(string _source_order)
        {
            throw new NotImplementedException("exception from UberServiceHandler");
        }
    }
}
