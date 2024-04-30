using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionAndDateOfEditing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfEditing",
                table: "Phones",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Phones",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfEditing",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Phones");
        }
    }
}
