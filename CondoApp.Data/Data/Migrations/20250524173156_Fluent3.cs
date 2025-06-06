﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondoApp.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fluent3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Persons_TenantId",
                table: "Persons",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Tenants_TenantId",
                table: "Persons",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Tenants_TenantId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_TenantId",
                table: "Persons");
        }
    }
}
