using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspMiniProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSliderInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "SliderInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "SliderInfos");
        }
    }
}
