using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Northwind.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class outbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      

            migrationBuilder.CreateTable(
                name: "Outbox",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outbox", x => x.ID);
                });
             
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
 

            migrationBuilder.DropTable(
                name: "Outbox");
 
        }
    }
}
