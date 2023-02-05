﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWallker.API.Migrations
{
    /// <inheritdoc />
    public partial class fixusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Users_RoleId",
                table: "Users_Roles");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Roles_UserId",
                table: "Users_Roles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Users_UserId",
                table: "Users_Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Users_UserId",
                table: "Users_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_Roles_UserId",
                table: "Users_Roles");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Users_RoleId",
                table: "Users_Roles",
                column: "RoleId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
