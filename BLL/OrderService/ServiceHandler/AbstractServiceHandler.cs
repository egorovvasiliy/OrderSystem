using DAL;
using DAL.Entities;
using System;

namespace BLL.OrderService
{
    public abstract class AbstractServiceHandler
    {
        public abstract System_type System_Type { get; }
        public abstract string HandleOrder(string convertered_order);
    }
}
