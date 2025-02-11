using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProgram.Api.Migrations
{
    /// <inheritdoc />
    public partial class Add_Library : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_LibraryId",
                table: "Students",
                column: "LibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Libraries_LibraryId",
                table: "Students",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Libraries_LibraryId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Students_LibraryId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Students");
        }
    }
}
