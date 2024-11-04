using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excercise02.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    PartyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyID = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_Parties_PartyID",
                        column: x => x.PartyID,
                        principalTable: "Parties",
                        principalColumn: "PartyID");
                });

            migrationBuilder.CreateTable(
                name: "PartyWiseProduct",
                columns: table => new
                {
                    PartyWiseProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyWiseProduct", x => x.PartyWiseProductID);
                    table.ForeignKey(
                        name: "FK_PartyWiseProduct_Parties_PartyID",
                        column: x => x.PartyID,
                        principalTable: "Parties",
                        principalColumn: "PartyID");
                    table.ForeignKey(
                        name: "FK_PartyWiseProduct_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "ProductRate",
                columns: table => new
                {
                    ProductRateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ProductRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceAppliedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRate", x => x.ProductRateID);
                    table.ForeignKey(
                        name: "FK_ProductRate_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceWiseProducts",
                columns: table => new
                {
                    InvoiceWiseProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    InvoiceID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceWiseProducts", x => x.InvoiceWiseProductID);
                    table.ForeignKey(
                        name: "FK_InvoiceWiseProducts_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID");
                    table.ForeignKey(
                        name: "FK_InvoiceWiseProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PartyID",
                table: "Invoices",
                column: "PartyID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceWiseProducts_InvoiceID",
                table: "InvoiceWiseProducts",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceWiseProducts_ProductID",
                table: "InvoiceWiseProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyWiseProduct_PartyID",
                table: "PartyWiseProduct",
                column: "PartyID");

            migrationBuilder.CreateIndex(
                name: "IX_PartyWiseProduct_ProductID",
                table: "PartyWiseProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRate_ProductID",
                table: "ProductRate",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceWiseProducts");

            migrationBuilder.DropTable(
                name: "PartyWiseProduct");

            migrationBuilder.DropTable(
                name: "ProductRate");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Parties");
        }
    }
}
