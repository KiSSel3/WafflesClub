using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waffles_Club.DataManagment.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleUsers",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { new Guid("6ded72b8-8b52-4ecc-81bd-5b29bccaabfe"), new Guid("9e7bf79c-bf94-4b40-a4f6-a3660e9dfd4a"), new Guid("db571f33-8399-4b5d-a210-07ff85b784ee") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Login", "Name", "Password" },
                values: new object[] { new Guid("db571f33-8399-4b5d-a210-07ff85b784ee"), "alsemkovbn@gmail.com", "Admin", "Admin", "123498Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleUsers",
                keyColumn: "Id",
                keyValue: new Guid("6ded72b8-8b52-4ecc-81bd-5b29bccaabfe"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("db571f33-8399-4b5d-a210-07ff85b784ee"));
        }
    }
}
