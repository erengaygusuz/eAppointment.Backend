using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAppointment.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuItem_ParentId",
                table: "MenuItem");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Page",
                newName: "PageKey");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "MenuItem",
                newName: "MenuKey");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_MenuItem_ParentId",
                table: "MenuItem",
                column: "ParentId",
                principalTable: "MenuItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_MenuItem_ParentId",
                table: "MenuItem");

            migrationBuilder.RenameColumn(
                name: "PageKey",
                table: "Page",
                newName: "Key");

            migrationBuilder.RenameColumn(
                name: "MenuKey",
                table: "MenuItem",
                newName: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_MenuItem_ParentId",
                table: "MenuItem",
                column: "ParentId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
