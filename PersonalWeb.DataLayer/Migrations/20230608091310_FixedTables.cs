using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalWeb.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WalletSum",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SmsCode",
                table: "Users",
                newName: "EmailCode");

            migrationBuilder.RenameColumn(
                name: "CodeMelli",
                table: "Users",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailCode",
                table: "Users",
                newName: "SmsCode");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "CodeMelli");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WalletSum",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
