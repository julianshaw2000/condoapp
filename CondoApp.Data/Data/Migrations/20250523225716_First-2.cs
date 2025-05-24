using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondoApp.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class First2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_AspNetUsers_OwnerId1",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_OwnerId1",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Apartments");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Apartments",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_AspNetUsers_OwnerId",
                table: "Apartments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_AspNetUsers_OwnerId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartments");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Apartments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Apartments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_OwnerId1",
                table: "Apartments",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_AspNetUsers_OwnerId1",
                table: "Apartments",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
