using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatCat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class storyTypes_added_back : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StoryTypes_StoryTypeId",
                table: "Stories");

            migrationBuilder.AddColumn<string>(
                name: "MaskingTypeId",
                table: "StoryTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RhymingPatternId",
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

            migrationBuilder.AddColumn<string>(
                name: "StoryTypeName",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StoryTypes_StoryTypeId",
                table: "Stories",
                column: "StoryTypeId",
                principalTable: "StoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StoryTypes_StoryTypeId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "MaskingTypeId",
                table: "StoryTypes");

            migrationBuilder.DropColumn(
                name: "RhymingPatternId",
                table: "StoryTypes");

            migrationBuilder.DropColumn(
                name: "StoryTypeName",
                table: "Stories");

            migrationBuilder.AlterColumn<Guid>(
                name: "StoryTypeId",
                table: "Stories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StoryTypes_StoryTypeId",
                table: "Stories",
                column: "StoryTypeId",
                principalTable: "StoryTypes",
                principalColumn: "Id");
        }
    }
}
