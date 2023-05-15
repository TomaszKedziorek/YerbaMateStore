using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YerbaMateStore.Migrations
{
    public partial class RefactorImageTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YerbaMateImages_YerbaMate_ProductId",
                table: "YerbaMateImages");

            migrationBuilder.DropTable(
                name: "BombillaImages");

            migrationBuilder.DropTable(
                name: "CupImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YerbaMateImages",
                table: "YerbaMateImages");

            migrationBuilder.RenameTable(
                name: "YerbaMateImages",
                newName: "Images");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Images",
                newName: "YerbaMateProductId");

            migrationBuilder.RenameIndex(
                name: "IX_YerbaMateImages_ProductId",
                table: "Images",
                newName: "IX_Images_YerbaMateProductId");

            migrationBuilder.AlterColumn<int>(
                name: "YerbaMateProductId",
                table: "Images",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BombillaProductId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CupProductId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Images",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BombillaProductId",
                table: "Images",
                column: "BombillaProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_CupProductId",
                table: "Images",
                column: "CupProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Bombilla_BombillaProductId",
                table: "Images",
                column: "BombillaProductId",
                principalTable: "Bombilla",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Cup_CupProductId",
                table: "Images",
                column: "CupProductId",
                principalTable: "Cup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_YerbaMate_YerbaMateProductId",
                table: "Images",
                column: "YerbaMateProductId",
                principalTable: "YerbaMate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Bombilla_BombillaProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Cup_CupProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_YerbaMate_YerbaMateProductId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_BombillaProductId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_CupProductId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "BombillaProductId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CupProductId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "YerbaMateImages");

            migrationBuilder.RenameColumn(
                name: "YerbaMateProductId",
                table: "YerbaMateImages",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_YerbaMateProductId",
                table: "YerbaMateImages",
                newName: "IX_YerbaMateImages_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "YerbaMateImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_YerbaMateImages",
                table: "YerbaMateImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BombillaImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BombillaImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BombillaImages_Bombilla_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Bombilla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CupImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CupImages_Cup_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Cup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BombillaImages_ProductId",
                table: "BombillaImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CupImages_ProductId",
                table: "CupImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_YerbaMateImages_YerbaMate_ProductId",
                table: "YerbaMateImages",
                column: "ProductId",
                principalTable: "YerbaMate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
