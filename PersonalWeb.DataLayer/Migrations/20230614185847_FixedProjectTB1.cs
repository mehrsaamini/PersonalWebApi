using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWeb.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixedProjectTB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Projects",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "Description");
        }
    }
}
