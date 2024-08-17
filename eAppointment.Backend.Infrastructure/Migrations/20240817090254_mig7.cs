using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAppointment.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AuditLogs_AuditLogId",
                table: "ErrorLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TableLogs_AuditLogs_AuditLogId",
                table: "TableLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_AuditLogId",
                table: "ErrorLogs");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_AuditLogId",
                table: "ErrorLogs",
                column: "AuditLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_AuditLogs_AuditLogId",
                table: "ErrorLogs",
                column: "AuditLogId",
                principalTable: "AuditLogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableLogs_AuditLogs_AuditLogId",
                table: "TableLogs",
                column: "AuditLogId",
                principalTable: "AuditLogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AuditLogs_AuditLogId",
                table: "ErrorLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TableLogs_AuditLogs_AuditLogId",
                table: "TableLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_AuditLogId",
                table: "ErrorLogs");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_AuditLogId",
                table: "ErrorLogs",
                column: "AuditLogId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_AuditLogs_AuditLogId",
                table: "ErrorLogs",
                column: "AuditLogId",
                principalTable: "AuditLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TableLogs_AuditLogs_AuditLogId",
                table: "TableLogs",
                column: "AuditLogId",
                principalTable: "AuditLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
