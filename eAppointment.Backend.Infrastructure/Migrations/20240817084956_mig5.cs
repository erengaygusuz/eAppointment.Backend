using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAppointment.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "LogEvent",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "Properties",
                table: "ErrorLogs",
                newName: "InnerExceptionStackTrace");

            migrationBuilder.RenameColumn(
                name: "MessageTemplate",
                table: "ErrorLogs",
                newName: "InnerExceptionMessage");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "ErrorLogs",
                newName: "ExceptionStackTrace");

            migrationBuilder.RenameColumn(
                name: "Exception",
                table: "ErrorLogs",
                newName: "ExceptionMessage");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "AuditLogs",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "QueryString",
                table: "AuditLogs",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Headers",
                table: "AuditLogs",
                newName: "ResponseHeaders");

            migrationBuilder.AddColumn<int>(
                name: "AuditLogId",
                table: "ErrorLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "AuditLogs",
                type: "nvarchar(2048)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "AuditLogs",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7)");

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

            migrationBuilder.AddColumn<int>(
                name: "RemotePort",
                table: "AuditLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestHeaders",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "AuditLogs",
                type: "nvarchar(2048)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TableLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableChangeType = table.Column<int>(type: "int", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    AffectedColumns = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    KeyValues = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    AuditLogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TableLogs_AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_AuditLogId",
                table: "ErrorLogs",
                column: "AuditLogId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TableLogs_AuditLogId",
                table: "TableLogs",
                column: "AuditLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_AuditLogs_AuditLogId",
                table: "ErrorLogs",
                column: "AuditLogId",
                principalTable: "AuditLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AuditLogs_AuditLogId",
                table: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "TableLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_AuditLogId",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "AuditLogId",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "LocalIpAddress",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "LocalPort",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "QueryParameters",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RemoteIpAddress",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RemotePort",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "RequestHeaders",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "InnerExceptionStackTrace",
                table: "ErrorLogs",
                newName: "Properties");

            migrationBuilder.RenameColumn(
                name: "InnerExceptionMessage",
                table: "ErrorLogs",
                newName: "MessageTemplate");

            migrationBuilder.RenameColumn(
                name: "ExceptionStackTrace",
                table: "ErrorLogs",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "ExceptionMessage",
                table: "ErrorLogs",
                newName: "Exception");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "AuditLogs",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AuditLogs",
                newName: "QueryString");

            migrationBuilder.RenameColumn(
                name: "ResponseHeaders",
                table: "AuditLogs",
                newName: "Headers");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "ErrorLogs",
                type: "nvarchar(128)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogEvent",
                table: "ErrorLogs",
                type: "xml",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "ErrorLogs",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "AuditLogs",
                type: "nvarchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)");

            migrationBuilder.AlterColumn<string>(
                name: "Method",
                table: "AuditLogs",
                type: "varchar(7)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "AuditLogs",
                type: "varchar(40)",
                nullable: false,
                defaultValue: "");
        }
    }
}
