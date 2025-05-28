using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondoApp.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class Entityfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Apartments_ApartmentId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ApartmentId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ApartmentId",
                table: "Persons");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Persons",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Persons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Persons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApartmentId",
                table: "Persons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Apartments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ApartmentId",
                table: "Persons",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Apartments_ApartmentId",
                table: "Persons",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
