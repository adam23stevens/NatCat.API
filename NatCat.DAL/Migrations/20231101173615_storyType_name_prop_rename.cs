using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatCat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class storyType_name_prop_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "StoryTypes",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StoryTypes",
                newName: "TypeName");
        }
    }
}
