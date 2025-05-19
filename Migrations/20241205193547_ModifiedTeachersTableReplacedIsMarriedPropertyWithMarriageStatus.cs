using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedTeachersTableReplacedIsMarriedPropertyWithMarriageStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMarried",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "MarriageStatus",
                table: "Teachers",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarriageStatus",
                table: "Teachers");

            migrationBuilder.AddColumn<bool>(
                name: "IsMarried",
                table: "Teachers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
