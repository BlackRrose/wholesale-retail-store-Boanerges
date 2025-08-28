using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace WholesaleRetailAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PricingRule> PricingRules { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PricingRule>()
                .Property(o => o.DiscountPercentage)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(o => o.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Apple", Description = "Fresh Apple", Price = 15.5m, StockQuantity = 100, Category = "Fruit" },
                new Product { ProductId = 2, Name = "Banana", Description = "Fresh Banana", Price = 12.0m, StockQuantity = 120, Category = "Fruit" },
                new Product { ProductId = 3, Name = "Milk", Description = "1L Milk", Price = 20.0m, StockQuantity = 80, Category = "Dairy" },
                new Product { ProductId = 4, Name = "Bread", Description = "Whole Grain Bread", Price = 10.5m, StockQuantity = 50, Category = "Bakery" },
                new Product { ProductId = 5, Name = "Eggs", Description = "Dozen Eggs", Price = 25.0m, StockQuantity = 70, Category = "Dairy" }
            );

            // Seed Customers (2 per type)
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "Retail Customer 1", Email = "retail1@test.com", CustomerType = "Retail" },
                new Customer { CustomerId = 2, Name = "Retail Customer 2", Email = "retail2@test.com", CustomerType = "Retail" },
                new Customer { CustomerId = 3, Name = "Wholesale Customer 1", Email = "wholesale1@test.com", CustomerType = "Wholesale" },
                new Customer { CustomerId = 4, Name = "Wholesale Customer 2", Email = "wholesale2@test.com", CustomerType = "Wholesale" }
            );

            // Seed PricingRules
            modelBuilder.Entity<PricingRule>().HasData(
                new PricingRule { PricingRuleId = 1, CustomerType = "Wholesale", MinQuantity = 10, DiscountPercentage = 5, Active = true },
                new PricingRule { PricingRuleId = 2, CustomerType = "Wholesale", MinQuantity = 50, DiscountPercentage = 10, Active = true },
                new PricingRule { PricingRuleId = 3, CustomerType = "Retail", MinQuantity = 5, DiscountPercentage = 2, Active = true }
            );
        }   
    }
}
