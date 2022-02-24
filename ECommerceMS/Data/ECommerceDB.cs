using ECommerceMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceMS.Data
{
    public class ECommerceDB:IdentityDbContext<ApplicationUser>
    {
        public ECommerceDB()
        {

        }
        public ECommerceDB(DbContextOptions<ECommerceDB> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Make Primary Key Constrain On StudentCourses Model

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductOrders>().HasKey(po => new {po.ProductId, po.OrderNum});

            modelBuilder.Entity<ProductOrders>()
                 .HasOne<Product>(po=> po.Product)
                 .WithMany(p=>p.ProductOrders)
                 .HasForeignKey(po=>po.ProductId);


            modelBuilder.Entity<ProductOrders>()
                 .HasOne<Order>(po => po.Order)
                 .WithMany(o=>o.ProductOrders)
                 .HasForeignKey(po=>po.OrderNum);
           //====================================================================================== 
           
            modelBuilder.Entity<ProductCarts>().HasKey(pc => new {pc.ProductId, pc.CartId});

            modelBuilder.Entity<ProductCarts>()
                 .HasOne<Product>(pc=> pc.Product)
                 .WithMany(p=>p.ProductCarts)
                 .HasForeignKey(pc=>pc.ProductId);


            modelBuilder.Entity<ProductCarts>()
                 .HasOne<Cart>(pc => pc.Cart)
                 .WithMany(c=>c.ProductCarts)
                 .HasForeignKey(pc=>pc.CartId);
            //====================================================================================== 
           
            modelBuilder.Entity<FavouriteList>().HasKey(f => new {f.ProductId, f.CustomerId});

            modelBuilder.Entity<FavouriteList>()
                 .HasOne<Product>(f=> f.Product)
                 .WithMany(p=>p.FavouriteList)
                 .HasForeignKey(f=>f.ProductId);


            modelBuilder.Entity<FavouriteList>()
                 .HasOne<Customer>(f => f.Customer)
                 .WithMany(c=>c.FavouriteList)
                 .HasForeignKey(f=>f.CustomerId);

            #endregion
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCarts> ProductCarts { get; set; }
        public DbSet<ProductOrders> ProductOrders { get; set; }
        public DbSet<FavouriteList> FavouriteLists { get; set; }
    }
}
