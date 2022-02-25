using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceMS.Migrations
{
    public partial class addquantitytoproductOrdertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Orders_OrderNum",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderNum",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Orders_OrderNum",
                table: "Carts",
                column: "OrderNum",
                principalTable: "Orders",
                principalColumn: "OrderNum",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Orders_OrderNum",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductOrders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderNum",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Orders_OrderNum",
                table: "Carts",
                column: "OrderNum",
                principalTable: "Orders",
                principalColumn: "OrderNum",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
