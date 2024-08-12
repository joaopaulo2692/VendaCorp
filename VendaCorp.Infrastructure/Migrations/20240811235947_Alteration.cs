using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendaCorp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alteration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippningCompanyId",
                table: "DeliveryOrder");

            migrationBuilder.RenameColumn(
                name: "ShippningCompanyName",
                table: "DeliveryOrder",
                newName: "ShippingCompanyName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingCompanyName",
                table: "DeliveryOrder",
                newName: "ShippningCompanyName");

            migrationBuilder.AddColumn<int>(
                name: "ShippningCompanyId",
                table: "DeliveryOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
