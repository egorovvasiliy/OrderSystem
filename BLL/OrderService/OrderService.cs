using BLL.LoggerService;
using BLL.Models;
using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BLL.OrderService
{
    public class OrderService
    {
        ILogerService logerService;
        public OrderService(ILogerService _logerService) {
            logerService = _logerService;
        }
        Dictionary<System_type, AbstractServiceHandler> handlersOrderService = new Dictionary<System_type, AbstractServiceHandler>()
        {
            {
                System_type.talabat,
                new TalabatServiceHandler()
            },
            {
                System_type.zomato,
                new ZomatoServiceHandler()
            },
            {
                System_type.uber,
                new UberServiceHandler()
            }
        };
        public void AddOrderProductToDB(OrderProducts orderProducts,System_type System_Type) {
            using (var db = new OrderDbContext())
            {
                var order = new Order()
                {
                    order_number = int.Parse(orderProducts.orderNumber),
                    system_type = System_Type.ToString(),
                    source_order = JsonSerializer.Serialize(orderProducts),
                    created_at = DateTime.Now
                };
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
        public void StartHandlersOrders() {
            try {
                using (var db = new OrderDbContext())
                {
                    while (true)
                    {
                        Task.Delay(5000).Wait();

                        var orders = db.Orders.ToArray();
                        for (int i = 0; i < orders.Length; i++)
                        {
                            Enum.TryParse(orders[i].system_type, out System_type system_type);
                            handlersOrderService[system_type].HandleOrder(ref orders[i]);//foreach не понимает ref
                        }
                        db.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                logerService.WriteTextToLog($"{DateTime.Now.ToString()}: {ex.Message}\n");
            }
        }
    }
}
