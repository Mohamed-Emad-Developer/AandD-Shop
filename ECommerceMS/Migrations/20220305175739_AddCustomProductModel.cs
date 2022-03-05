using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceMS.Migrations
{
    public partial class AddCustomProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderNum = table.Column<int>(type: "int", nullable: true),
                    OrderNum1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomProducts_Orders_OrderNum1",
                        column: x => x.OrderNum1,
                        principalTable: "Orders",
                        principalColumn: "OrderNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomProducts_OrderNum1",
                table: "CustomProducts",
                column: "OrderNum1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomProducts");
        }
    }
}
