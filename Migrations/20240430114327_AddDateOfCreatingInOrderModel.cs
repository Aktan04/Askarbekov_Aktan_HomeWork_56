using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthProduct.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOfCreatingInOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreating",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreating",
                table: "Orders");
        }
    }
}
