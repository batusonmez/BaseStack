﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Person.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class bio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Person");
        }
    }
}
