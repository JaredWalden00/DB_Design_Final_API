using System.Net;

namespace DB_Design_Final_API.Models
{
    // Models/Customer.cs
    public class Customer
    {
        public long Cust_ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public ICollection<CCInfo> CCInfos { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }

    public class CCInfo
    {
        public long CC_Num { get; set; }
        public int Sec_Num { get; set; }
        public string BillingAddress { get; set; }
        public long Cust_ID { get; set; }
        public Customer Customer { get; set; }
    }

    public class Address
    {
        public long AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public long Cust_ID { get; set; }
        public Customer Customer { get; set; }
    }

    public class Employee
    {
        public long Empl_ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Salary { get; set; }
        public string Title { get; set; }
    }

    public class Product
    {
        public long Prod_ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Size { get; set; }
        public decimal Price { get; set; }
    }

    public class Warehouse
    {
        public long Ware_ID { get; set; }
        public string Address { get; set; }
        public decimal Capacity { get; set; }
    }

    public class Stock
    {
        public long Prod_ID { get; set; }
        public Product Product { get; set; }
        public long Ware_ID { get; set; }
        public Warehouse Warehouse { get; set; }
        public int Count { get; set; }
    }

    public class StockDto
    {
        public long Prod_ID { get; set; }
        public long Ware_ID { get; set; }
        public int Count { get; set; }
    }

    public class Order
    {
        public long Ordr_ID { get; set; }
        public long Prod_ID { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
    }

    public class Supplier
    {
        public long Supp_ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Supply> Supplies { get; set; }
    }

    public class Supply
    {
        public long Supp_ID { get; set; }
        public Supplier Supplier { get; set; }
        public long Prod_ID { get; set; }
        public Product Product { get; set; }
        public decimal Cost { get; set; }
    }

}
