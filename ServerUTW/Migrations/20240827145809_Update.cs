using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerUTW.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnrolled",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsEnrolled",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Students");

        }
    }
}