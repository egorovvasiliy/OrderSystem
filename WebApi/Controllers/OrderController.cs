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
        public async Task<IActionResult> Post([FromBody] OrderProducts orderProducts, System_type system_type) {
            var isAdded = await orderService.AddOrderToDBAsync(orderProducts, system_type);
            if (isAdded)
            {
                return new OkResult();
            }
            else
                return new StatusCodeResult(400);

        }
    }
}