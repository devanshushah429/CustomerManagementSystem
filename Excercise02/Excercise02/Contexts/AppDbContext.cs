using Excercise02.Models;
using Microsoft.EntityFrameworkCore;

namespace Excercise02.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<PartyModel> Parties { get; set; }

        public DbSet<ProductModel> Products { get; set; }

        public DbSet<ProductRateModel> ProductRate { get; set; }

        public DbSet<PartyWiseProductModel> PartyWiseProduct { get; set; }

        public DbSet<InvoiceModel> Invoices { get; set; }

        public DbSet<InvoiceWiseProductModel> InvoiceWiseProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PartyModel>()
                .HasKey(p => p.PartyID);

            modelBuilder
                .Entity<ProductModel>()
                .HasKey(p => p.ProductID);


            modelBuilder
                .Entity<ProductRateModel>()
                .HasKey(p => p.ProductRateID);

            modelBuilder
                .Entity<InvoiceModel>()
                .HasKey(p => p.InvoiceID);

            modelBuilder
                .Entity<PartyWiseProductModel>()
                .HasKey(p => p.PartyWiseProductID);

            modelBuilder
                .Entity<InvoiceWiseProductModel>()
                .HasKey(p => p.InvoiceWiseProductID);

            // HasOne indicates that each instance of ProductRateModel is associated with one instance of ProductModel.
            // WithMany indicates that each ProductModel can be associated with many instances of ProductRateModel.
            // HasForeignKey specifies which property in ProductRateModel is the foreign key that references the primary key in ProductModel.
            modelBuilder
                .Entity<ProductRateModel>()
                .HasOne(productRate => productRate.Product)
                .WithMany(product => product.ProductRates)
                .HasForeignKey(productRate => productRate.ProductID);

            modelBuilder
                .Entity<PartyWiseProductModel>()
                .HasOne(partyWiseProduct => partyWiseProduct.Product)
                .WithMany(product => product.PartyWiseProducts)
                .HasForeignKey(partyWiseProduct => partyWiseProduct.ProductID);

            modelBuilder
                .Entity<PartyWiseProductModel>()
                .HasOne(partyWiseProduct => partyWiseProduct.Party)
                .WithMany(party => party.PartyWiseProducts)
                .HasForeignKey(partyWiseProduct => partyWiseProduct.PartyID);

            modelBuilder
                .Entity<InvoiceModel>()
                .HasOne(invoice => invoice.Party)
                .WithMany(party => party.Invoices)
                .HasForeignKey(invoice => invoice.PartyID);

            modelBuilder
                .Entity<InvoiceWiseProductModel>()
                .HasOne(invoiceWiseProduct => invoiceWiseProduct.Product)
                .WithMany(product => product.InvoiceWiseProducts)
                .HasForeignKey(invoiceWiseProduct => invoiceWiseProduct.ProductID);

            modelBuilder
                .Entity<InvoiceWiseProductModel>()
                .HasOne(invoiceWiseProduct => invoiceWiseProduct.Invoice)
                .WithMany(invoice => invoice.InvoiceWiseProducts)
                .HasForeignKey(invoiceWiseProduct => invoiceWiseProduct.InvoiceID);


        }
    }
}