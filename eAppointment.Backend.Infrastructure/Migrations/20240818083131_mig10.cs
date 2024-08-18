using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAppointment.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "TableLogs");

            migrationBuilder.DropColumn(
                name: "LocalIpAddress",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "LocalPort",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "QueryParameters",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RemoteIpAddress",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RequestBody",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RequestHeaders",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "ResponseBody",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "ResponseHeaders",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "RemotePort",
                table: "AuditLogs",
                newName: "AuditType");

            migrationBuilder.AddColumn<string>(
                name: "AffectedColumns",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyValues",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewValues",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValues",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffectedColumns",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "KeyValues",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "NewValues",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "OldValues",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "TableName",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "AuditType",
                table: "AuditLogs",
                newName: "RemotePort");

            migrationBuilder.AddColumn<string>(
                name: "LocalIpAddress",
                table: "AuditLogs",
                type: "varchar(45)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LocalPort",
                table: "AuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "AuditLogs",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "AuditLogs",
                type: "nvarchar(2048)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QueryParameters",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RemoteIpAddress",
                table: "AuditLogs",
                type: "nvarchar(45)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequestBody",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequestHeaders",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResponseBody",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResponseHeaders",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "AuditLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "AuditLogs",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "AuditLogs",
                type: "nvarchar(2048)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditLogId = table.Column<int>(type: "int", nullable: false),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    ExceptionStackTrace = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    InnerExceptionMessage = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    InnerExceptionStackTrace = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorLogs_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AuditLogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TableLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditLogId = table.Column<int>(type: "int", nullable: false),
                    AffectedColumns = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    KeyValues = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    TableChangeType = table.Column<int>(type: "int", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableLogs_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AuditLogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_AuditLogId",
                table: "ErrorLogs",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_TableLogs_AuditLogId",
                table: "TableLogs",
                column: "AuditLogId");
        }
    }
}
