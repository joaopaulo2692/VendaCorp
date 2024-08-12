using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendaCorp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterationOnOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Enterprises_EnterpriseId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EnterpriseId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EnterpriseId1",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "EnterpriseId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EnterpriseId",
                table: "Orders",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Enterprises_EnterpriseId",
                table: "Orders",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Enterprises_EnterpriseId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EnterpriseId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "EnterpriseId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId1",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EnterpriseId1",
                table: "Orders",
                column: "EnterpriseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Enterprises_EnterpriseId1",
                table: "Orders",
                column: "EnterpriseId1",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
