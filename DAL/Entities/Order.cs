using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Order
    {
        public int id { get; set; }
        public string system_type { get; set; }
        public int order_number { get; set; }
        public string source_order { get; set; }
        public string converted_order { get; set; }
        public DateTime created_at { get; set; }
    }
}
