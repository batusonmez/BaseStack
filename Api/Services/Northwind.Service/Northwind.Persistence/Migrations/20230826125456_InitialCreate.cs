using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             
            migrationBuilder.AddColumn<Guid>(
                name: "IdexTrace",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdexTrace",
                table: "Products");
 
        }
    }
}
