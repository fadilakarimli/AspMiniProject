using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMiniProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBannerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bannner",
                table: "Bannner");

            migrationBuilder.RenameTable(
                name: "Bannner",
                newName: "Banners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Banners",
                table: "Banners",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Banners",
                table: "Banners");

            migrationBuilder.RenameTable(
                name: "Banners",
                newName: "Bannner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bannner",
                table: "Bannner",
                column: "Id");
        }
    }
}
