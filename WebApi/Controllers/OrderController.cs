using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.LoggerService;
using BLL.Models;
using BLL.OrderService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/{system_type}")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ILogerService logerService;
        OrderService orderService;
        public OrderController(ILogerService _logerService, OrderService _orderService) {
            logerService = _logerService;
            orderService = _orderService;
        }
        [HttpPost]
        public IActionResult Post([FromBody] OrderProducts orderProducts, System_type system_type) {
            logerService.WriteTextToLog($"{system_type} от {DateTime.Now.ToString()}\n");
            //----------------Вместо Case можно использовать отдельный метод Post(с доп.маршрутом в контроллере)--------
            AbstractServiceHandler serviceHandler = null;
            switch (system_type) {
                case System_type.zomato:
                    serviceHandler = new ZomatoServiceHandler();
                    break;
                default:
                    break;
            }
            orderService.SetServiceHandler(serviceHandler);
            orderService.HandleOrder(orderProducts);
            //----------------------------------------------------------------------------------------------------------
            return new OkResult();
        }
    }
}