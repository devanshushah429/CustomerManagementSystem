using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Excercise02.Migrations
{
    /// <inheritdoc />
    public partial class ProductTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ChangedPrice",
                table: "ProductRate");

            migrationBuilder.RenameColumn(
                name: "UpdateDateTime",
                table: "ProductRate",
                newName: "PriceAppliedDate");

            migrationBuilder.RenameColumn(
                name: "OldPrice",
                table: "ProductRate",
                newName: "ProductRate");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProductRate",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "ProductRate",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProductRate");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "ProductRate");

            migrationBuilder.RenameColumn(
                name: "ProductRate",
                table: "ProductRate",
                newName: "OldPrice");

            migrationBuilder.RenameColumn(
                name: "PriceAppliedDate",
                table: "ProductRate",
                newName: "UpdateDateTime");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChangedPrice",
                table: "ProductRate",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
