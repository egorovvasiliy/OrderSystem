using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class OrderProducts
    {
        public int orderNumber { get; set; }
        public IEnumerable<Product> products { get; set; }
        public string createdAt { get; set; }
    }
}
