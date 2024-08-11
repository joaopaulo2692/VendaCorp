using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendaCorp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderDateToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesOrderId",
                table: "Orders",
                newName: "DeliveryOrderId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryOrderId",
                table: "Orders",
                newName: "SalesOrderId");

            migrationBuilder.AlterColumn<string>(
                name: "OrderDate",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
