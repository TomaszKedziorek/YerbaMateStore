using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YerbaMateStore.Migrations
{
    public partial class ImagesUrlRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "YerbaMateImages",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "CupImages",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "BombillaImages",
                newName: "ImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "YerbaMateImages",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "CupImages",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "BombillaImages",
                newName: "Url");
        }
    }
}
