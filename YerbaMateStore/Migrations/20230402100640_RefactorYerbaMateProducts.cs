using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YerbaMateStore.Migrations
{
    public partial class RefactorYerbaMateProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YerbaMateProducts_Countries_CountryId",
                table: "YerbaMateProducts");

            migrationBuilder.RenameColumn(
                name: "Additives",
                table: "YerbaMateProducts",
                newName: "HasAdditives");

            migrationBuilder.AddForeignKey(
                name: "FK_YerbaMateProducts_Countries_CountryId",
                table: "YerbaMateProducts",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YerbaMateProducts_Countries_CountryId",
                table: "YerbaMateProducts");

            migrationBuilder.RenameColumn(
                name: "HasAdditives",
                table: "YerbaMateProducts",
                newName: "Additives");

            migrationBuilder.AddForeignKey(
                name: "FK_YerbaMateProducts_Countries_CountryId",
                table: "YerbaMateProducts",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
