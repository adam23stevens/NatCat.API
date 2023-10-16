using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatCat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RhymingPatterns_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StoryTypes_StoryTypeId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "IsRhymingRequired",
                table: "StoryTypes");

            migrationBuilder.DropColumn(
                name: "RhymingPattern",
                table: "StoryTypes");

            migrationBuilder.AlterColumn<Guid>(
                name: "StoryTypeId",
                table: "Stories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "MaskingType",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreviousTextVisibilityPercentage",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RhymingPatternId",
                table: "Stories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RhymingPatterns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatternStr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RhymingPatterns", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_RhymingPatternId",
                table: "Stories",
                column: "RhymingPatternId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_RhymingPatterns_RhymingPatternId",
                table: "Stories",
                column: "RhymingPatternId",
                principalTable: "RhymingPatterns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StoryTypes_StoryTypeId",
                table: "Stories",
                column: "StoryTypeId",
                principalTable: "StoryTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_RhymingPatterns_RhymingPatternId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StoryTypes_StoryTypeId",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "RhymingPatterns");

            migrationBuilder.DropIndex(
                name: "IX_Stories_RhymingPatternId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "MaskingType",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "PreviousTextVisibilityPercentage",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "RhymingPatternId",
                table: "Stories");

            migrationBuilder.AddColumn<bool>(
                name: "IsRhymingRequired",
                table: "StoryTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RhymingPattern",
                table: "StoryTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StoryTypeId",
                table: "Stories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StoryTypes_StoryTypeId",
                table: "Stories",
                column: "StoryTypeId",
                principalTable: "StoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
