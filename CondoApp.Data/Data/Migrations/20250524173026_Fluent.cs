using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondoApp.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fluent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "Tenants",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Persons",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_OwnerId",
                table: "Persons",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_AspNetUsers_OwnerId",
                table: "Persons",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_AspNetUsers_OwnerId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_OwnerId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "Tenants",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
