using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWeb.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixedServiceTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Services",
                newName: "IconId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "Services",
                newName: "Icon");
        }
    }
}
