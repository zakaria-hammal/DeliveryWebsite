using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class Delivery : DbContext
    {
        public DbSet<Category> Categories{get; set;}
        public DbSet<Product> Products{get; set;}
        public DbSet<Order> Orders{get; set;}
        public DbSet<Feedback> Feedbacks{get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Delivery.db");
            
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}