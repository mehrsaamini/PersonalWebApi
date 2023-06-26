using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWeb.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedProjectDescUserTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectDescription",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectDescription",
                table: "Users");
        }
    }
}
