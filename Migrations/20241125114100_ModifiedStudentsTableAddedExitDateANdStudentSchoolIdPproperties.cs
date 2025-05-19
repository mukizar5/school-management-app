using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStudentsTableAddedExitDateANdStudentSchoolIdPproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExitDate",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentSchoolId",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExitDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentSchoolId",
                table: "Students");
        }
    }
}
