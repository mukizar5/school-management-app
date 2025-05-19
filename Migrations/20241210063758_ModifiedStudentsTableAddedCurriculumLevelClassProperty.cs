using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStudentsTableAddedCurriculumLevelClassProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurriculumLevelClassId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CurriculumLevelClassId",
                table: "Students",
                column: "CurriculumLevelClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CurriculumLevelClasses_CurriculumLevelClassId",
                table: "Students",
                column: "CurriculumLevelClassId",
                principalTable: "CurriculumLevelClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_CurriculumLevelClasses_CurriculumLevelClassId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CurriculumLevelClassId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CurriculumLevelClassId",
                table: "Students");
        }
    }
}
