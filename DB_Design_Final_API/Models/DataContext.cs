using Microsoft.EntityFrameworkCore;

namespace DB_Design_Final_API.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions
        <DataContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CCInfo> CCInfos { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Supply> Supplies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<Customer>().HasKey(c => c.Cust_ID);
            modelBuilder.Entity<CCInfo>().HasKey(cc => new { cc.CC_Num, cc.Sec_Num });
            modelBuilder.Entity<Employee>().HasKey(e => e.Empl_ID);
            modelBuilder.Entity<Product>().HasKey(p => p.Prod_ID);
            modelBuilder.Entity<Warehouse>().HasKey(w => w.Ware_ID);
            modelBuilder.Entity<Stock>().HasKey(s => new { s.Prod_ID, s.Ware_ID });
            modelBuilder.Entity<Order>().HasKey(o => new { o.Ordr_ID, o.Prod_ID });
            modelBuilder.Entity<Supplier>().HasKey(s => s.Supp_ID);
            modelBuilder.Entity<Supply>().HasKey(s => new { s.Supp_ID, s.Prod_ID });

            // Configure relationships
            modelBuilder.Entity<CCInfo>()
                .HasOne(cc => cc.Customer)
                .WithMany(c => c.CCInfos)
                .HasForeignKey(cc => cc.Cust_ID);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.Prod_ID);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Warehouse)
                .WithMany()
                .HasForeignKey(s => s.Ware_ID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.Prod_ID);

            modelBuilder.Entity<Supply>()
                .HasOne(s => s.Supplier)
                .WithMany(s => s.Supplies)
                .HasForeignKey(s => s.Supp_ID);

            modelBuilder.Entity<Supply>()
                .HasOne(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.Prod_ID);
        }
    }

    
}
