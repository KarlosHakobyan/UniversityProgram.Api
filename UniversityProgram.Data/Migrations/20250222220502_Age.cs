using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class Age : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Age",
                table: "Students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Address", "Age", "Email", "LibraryId", "Money", "Name" },
                values: new object[] { 18, "Monument", 0L, "Kar@mail.ru", null, 0m, "Karen" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Students");
        }
    }
}
