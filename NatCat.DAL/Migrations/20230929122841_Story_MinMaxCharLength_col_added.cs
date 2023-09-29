using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatCat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Story_MinMaxCharLength_col_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxWordsPerStoryPart",
                table: "StoryTypes");

            migrationBuilder.DropColumn(
                name: "MinWordsPerStoryPart",
                table: "StoryTypes");

            migrationBuilder.AddColumn<int>(
                name: "MaxCharLengthPerStoryPart",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinCharLengthPerStoryPart",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCharLengthPerStoryPart",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "MinCharLengthPerStoryPart",
                table: "Stories");

            migrationBuilder.AddColumn<int>(
                name: "MaxWordsPerStoryPart",
                table: "StoryTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinWordsPerStoryPart",
                table: "StoryTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
