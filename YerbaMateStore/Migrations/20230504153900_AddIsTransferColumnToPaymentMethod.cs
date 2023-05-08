using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YerbaMateStore.Migrations
{
    public partial class AddIsTransferColumnToPaymentMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTransfer",
                table: "PaymentMethod",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryMethodId",
                table: "OrderHeader",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_DeliveryMethodId",
                table: "OrderHeader",
                column: "DeliveryMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_DeliveryMethod_DeliveryMethodId",
                table: "OrderHeader",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_DeliveryMethod_DeliveryMethodId",
                table: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_DeliveryMethodId",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "IsTransfer",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "DeliveryMethodId",
                table: "OrderHeader");
        }
    }
}
