﻿// <auto-generated />
using System;
using Excercise02.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Excercise02.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Excercise02.Models.InvoiceModel", b =>
                {
                    b.Property<int?>("InvoiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("InvoiceID"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PartyID")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceID");

                    b.HasIndex("PartyID");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Excercise02.Models.InvoiceWiseProductModel", b =>
                {
                    b.Property<int?>("InvoiceWiseProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("InvoiceWiseProductID"));

                    b.Property<int?>("InvoiceID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceWiseProductID");

                    b.HasIndex("InvoiceID");

                    b.HasIndex("ProductID");

                    b.ToTable("InvoiceWiseProducts");
                });

            modelBuilder.Entity("Excercise02.Models.PartyModel", b =>
                {
                    b.Property<int?>("PartyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("PartyID"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("PartyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PartyID");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("Excercise02.Models.PartyWiseProductModel", b =>
                {
                    b.Property<int?>("PartyWiseProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("PartyWiseProductID"));

                    b.Property<int?>("PartyID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("PartyWiseProductID");

                    b.HasIndex("PartyID");

                    b.HasIndex("ProductID");

                    b.ToTable("PartyWiseProduct");
                });

            modelBuilder.Entity("Excercise02.Models.ProductModel", b =>
                {
                    b.Property<int?>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ProductID"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Excercise02.Models.ProductRateModel", b =>
                {
                    b.Property<int?>("ProductRateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ProductRateID"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PriceAppliedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.Property<decimal?>("ProductRate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductRateID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductRate");
                });

            modelBuilder.Entity("Excercise02.Models.InvoiceModel", b =>
                {
                    b.HasOne("Excercise02.Models.PartyModel", "Party")
                        .WithMany("Invoices")
                        .HasForeignKey("PartyID");

                    b.Navigation("Party");
                });

            modelBuilder.Entity("Excercise02.Models.InvoiceWiseProductModel", b =>
                {
                    b.HasOne("Excercise02.Models.InvoiceModel", "Invoice")
                        .WithMany("InvoiceWiseProducts")
                        .HasForeignKey("InvoiceID");

                    b.HasOne("Excercise02.Models.ProductModel", "Product")
                        .WithMany("InvoiceWiseProducts")
                        .HasForeignKey("ProductID");

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Excercise02.Models.PartyWiseProductModel", b =>
                {
                    b.HasOne("Excercise02.Models.PartyModel", "Party")
                        .WithMany("PartyWiseProducts")
                        .HasForeignKey("PartyID");

                    b.HasOne("Excercise02.Models.ProductModel", "Product")
                        .WithMany("PartyWiseProducts")
                        .HasForeignKey("ProductID");

                    b.Navigation("Party");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Excercise02.Models.ProductRateModel", b =>
                {
                    b.HasOne("Excercise02.Models.ProductModel", "Product")
                        .WithMany("ProductRates")
                        .HasForeignKey("ProductID");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Excercise02.Models.InvoiceModel", b =>
                {
                    b.Navigation("InvoiceWiseProducts");
                });

            modelBuilder.Entity("Excercise02.Models.PartyModel", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("PartyWiseProducts");
                });

            modelBuilder.Entity("Excercise02.Models.ProductModel", b =>
                {
                    b.Navigation("InvoiceWiseProducts");

                    b.Navigation("PartyWiseProducts");

                    b.Navigation("ProductRates");
                });
#pragma warning restore 612, 618
        }
    }
}
