﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityProgram.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_address_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "Address",
            table: "Students",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Students");
        }
    }
}
