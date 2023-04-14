using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YerbaMateStore.Migrations
{
    public partial class RemoveCountryFieldFromBombillaAndCup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bombilla_Countries_CountryId",
                table: "Bombilla");

            migrationBuilder.DropForeignKey(
                name: "FK_Cup_Countries_CountryId",
                table: "Cup");

            migrationBuilder.DropIndex(
                name: "IX_Cup_CountryId",
                table: "Cup");

            migrationBuilder.DropIndex(
                name: "IX_Bombilla_CountryId",
                table: "Bombilla");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Cup");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Bombilla");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Cup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Bombilla",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cup_CountryId",
                table: "Cup",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bombilla_CountryId",
                table: "Bombilla",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bombilla_Countries_CountryId",
                table: "Bombilla",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cup_Countries_CountryId",
                table: "Cup",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
