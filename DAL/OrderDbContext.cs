using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class OrderDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=VASYA\\SQLEXPRESS;Database=OrderProducts;Trusted_Connection=True;");
            }
        }
        public virtual DbSet<Order> Orders { get; set; }
    }
}
