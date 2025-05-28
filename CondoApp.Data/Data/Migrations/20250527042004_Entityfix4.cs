using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondoApp.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class Entityfix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Tenants_TenantId1",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Tenants_TenantId1",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_TenantId1",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_TenantId1",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "TenantId1",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "TenantId1",
                table: "Apartments");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Persons",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Apartments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Persons",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TenantId1",
                table: "Persons",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Apartments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TenantId1",
                table: "Apartments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_TenantId1",
                table: "Persons",
                column: "TenantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_TenantId1",
                table: "Apartments",
                column: "TenantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Tenants_TenantId1",
                table: "Apartments",
                column: "TenantId1",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Tenants_TenantId1",
                table: "Persons",
                column: "TenantId1",
                principalTable: "Tenants",
                principalColumn: "Id");
        }
    }
}
