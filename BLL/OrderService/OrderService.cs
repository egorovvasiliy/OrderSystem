using BLL.LoggerService;
using BLL.Models;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
        public bool AddOrderProductToDB(OrderProducts orderProducts,System_type System_Type) {
            EntityState resultState;
            using (var db = new OrderDbContext())
            {
                var order = new Order()
                {
                    order_number = int.Parse(orderProducts.orderNumber),
                    system_type = System_Type.ToString(),
                    source_order = JsonSerializer.Serialize(orderProducts),
                    created_at = DateTime.Now
                };
                resultState = db.Orders.Add(order).State;
                db.SaveChanges();
            }
            return resultState == EntityState.Added;
        }
        public Task StartHandlersOrders()=>
            Task.Run(()=> {
                try
                {
                    using (var db = new OrderDbContext())
                    {
                        while (true)
                        {
                            Task.Delay(5000).Wait();

                            var orders = db.Orders.ToArray();
                            for (int i = 0; i < orders.Length; i++)
                            {
                                Enum.TryParse(orders[i].system_type, out System_type system_type);
                                try
                                {
                                    if (String.IsNullOrEmpty(orders[i].converted_order))
                                        orders[i].converted_order = handlersOrderService[system_type].HandleOrder(orders[i].source_order);
                                }
                                catch (Exception ex) {
                                    logerService.SendMessageToLog(ex.Message);
                                }
                            }
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    logerService.SendMessageToLog(ex.Message);
                }
            });
    }
}
