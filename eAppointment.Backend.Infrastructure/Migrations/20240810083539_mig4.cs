using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAppointment.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Method = table.Column<string>(type: "varchar(7)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    QueryString = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    RequestBody = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    ResponseBody = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    Headers = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    IPAddress = table.Column<string>(type: "varchar(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");
        }
    }
}
