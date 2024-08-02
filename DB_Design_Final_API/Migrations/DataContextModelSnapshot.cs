﻿// <auto-generated />
using DB_Design_Final_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DB_Design_Final_API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DB_Design_Final_API.Models.CCInfo", b =>
                {
                    b.Property<long>("CC_Num")
                        .HasColumnType("bigint");

                    b.Property<int>("Sec_Num")
                        .HasColumnType("integer");

                    b.Property<string>("BillingAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Cust_ID")
                        .HasColumnType("bigint");

                    b.HasKey("CC_Num", "Sec_Num");

                    b.HasIndex("Cust_ID");

                    b.ToTable("CCInfos");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Customer", b =>
                {
                    b.Property<long>("Cust_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Cust_ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Cust_ID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Employee", b =>
                {
                    b.Property<long>("Empl_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Empl_ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Salary")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Empl_ID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Order", b =>
                {
                    b.Property<long>("Ordr_ID")
                        .HasColumnType("bigint");

                    b.Property<long>("Prod_ID")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.HasKey("Ordr_ID", "Prod_ID");

                    b.HasIndex("Prod_ID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Product", b =>
                {
                    b.Property<long>("Prod_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Prod_ID"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Size")
                        .HasColumnType("numeric");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Prod_ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Stock", b =>
                {
                    b.Property<long>("Prod_ID")
                        .HasColumnType("bigint");

                    b.Property<long>("Ware_ID")
                        .HasColumnType("bigint");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.HasKey("Prod_ID", "Ware_ID");

                    b.HasIndex("Ware_ID");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Supplier", b =>
                {
                    b.Property<long>("Supp_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Supp_ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Supp_ID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Supply", b =>
                {
                    b.Property<long>("Supp_ID")
                        .HasColumnType("bigint");

                    b.Property<long>("Prod_ID")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.HasKey("Supp_ID", "Prod_ID");

                    b.HasIndex("Prod_ID");

                    b.ToTable("Supplies");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Password")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Warehouse", b =>
                {
                    b.Property<long>("Ware_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Ware_ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Capacity")
                        .HasColumnType("numeric");

                    b.HasKey("Ware_ID");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.CCInfo", b =>
                {
                    b.HasOne("DB_Design_Final_API.Models.Customer", "Customer")
                        .WithMany("CCInfos")
                        .HasForeignKey("Cust_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Order", b =>
                {
                    b.HasOne("DB_Design_Final_API.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Prod_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Stock", b =>
                {
                    b.HasOne("DB_Design_Final_API.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Prod_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB_Design_Final_API.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("Ware_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Supply", b =>
                {
                    b.HasOne("DB_Design_Final_API.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Prod_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB_Design_Final_API.Models.Supplier", "Supplier")
                        .WithMany("Supplies")
                        .HasForeignKey("Supp_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Customer", b =>
                {
                    b.Navigation("CCInfos");
                });

            modelBuilder.Entity("DB_Design_Final_API.Models.Supplier", b =>
                {
                    b.Navigation("Supplies");
                });
#pragma warning restore 612, 618
        }
    }
}
