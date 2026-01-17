using Microsoft.EntityFrameworkCore;
using OrderManagementApp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OrderManagementApp.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique constraints
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name).IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Sku).IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber).IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.CustomerEmail).IsUnique();

            // Seeding Products
            modelBuilder.Entity<Product>().HasData(
                Enumerable.Range(1, 15).Select(i => new Product
                {
                    Id = i,
                    Name = $"Product {i}",
                    Sku = $"SKU{i:000}",
                    Price = 100 + i,
                    StockQuantity = 50,
                    Category = "General",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                })
            );

            // Seeding Orders
            modelBuilder.Entity<Order>().HasData(
                Enumerable.Range(1, 30).Select(i => new Order
                {
                    Id = i,
                    ProductId = (i % 15) + 1,
                    OrderNumber = $"ORD-20260117-{i:0000}",
                    CustomerName = $"Customer {i}",
                    CustomerEmail = $"customer{i}@mail.com",
                    Quantity = 1,
                    OrderDate = DateTime.Today,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                })
            );
        }
    }
}
