using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatCat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class storyTypeName_removed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoryTypeName",
                table: "Stories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StoryTypeName",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
