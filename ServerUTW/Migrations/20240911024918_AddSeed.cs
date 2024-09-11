using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerUTW.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccoudCodes",
                table: "AccoudCodes");

            migrationBuilder.RenameTable(
                name: "AccoudCodes",
                newName: "AccountCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountCodes",
                table: "AccountCodes",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AccountCodes",
                columns: new[] { "Id", "Code", "CreatedAt", "Email", "UpdatedAt" },
                values: new object[] { 1, "TEST", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountCodes",
                table: "AccountCodes");

            migrationBuilder.DeleteData(
                table: "AccountCodes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "AccountCodes",
                newName: "AccoudCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccoudCodes",
                table: "AccoudCodes",
                column: "Id");
        }
    }
}
