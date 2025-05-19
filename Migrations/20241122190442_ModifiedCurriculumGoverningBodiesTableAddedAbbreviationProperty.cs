using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCurriculumGoverningBodiesTableAddedAbbreviationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CurriculumGoverningBodies",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "CurriculumGoverningBodies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "CurriculumGoverningBodies");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "CurriculumGoverningBodies",
                newName: "Name");
        }
    }
}
