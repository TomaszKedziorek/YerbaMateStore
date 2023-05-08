using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YerbaMateStore.Migrations
{
    public partial class DropOrderDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductOrderDetailCup_ProductId = table.Column<int>(name: "ProductOrderDetail<Cup>_ProductId", type: "int", nullable: true),
                    ProductOrderDetailYerbaMate_ProductId = table.Column<int>(name: "ProductOrderDetail<YerbaMate>_ProductId", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Bombilla_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Bombilla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Cup_ProductOrderDetail<Cup>_ProductId",
                        column: x => x.ProductOrderDetailCup_ProductId,
                        principalTable: "Cup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_OrderHeader_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_YerbaMate_ProductOrderDetail<YerbaMate>_ProductId",
                        column: x => x.ProductOrderDetailYerbaMate_ProductId,
                        principalTable: "YerbaMate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductOrderDetail<Cup>_ProductId",
                table: "OrderDetail",
                column: "ProductOrderDetail<Cup>_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductOrderDetail<YerbaMate>_ProductId",
                table: "OrderDetail",
                column: "ProductOrderDetail<YerbaMate>_ProductId");
        }
    }
}
