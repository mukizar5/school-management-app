using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedClassStreamsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClassStreamId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassStreams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    LastUpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassStreams", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassStreamId",
                table: "Students",
                column: "ClassStreamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_ClassStreams_ClassStreamId",
                table: "Students",
                column: "ClassStreamId",
                principalTable: "ClassStreams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_ClassStreams_ClassStreamId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "ClassStreams");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassStreamId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassStreamId",
                table: "Students");
        }
    }
}
