using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatCat.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MaskingTypes_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaskingType",
                table: "Stories");

            migrationBuilder.AddColumn<Guid>(
                name: "MaskingTypeId",
                table: "Stories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MaskingTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaskingTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_MaskingTypeId",
                table: "Stories",
                column: "MaskingTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_MaskingTypes_MaskingTypeId",
                table: "Stories",
                column: "MaskingTypeId",
                principalTable: "MaskingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_MaskingTypes_MaskingTypeId",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "MaskingTypes");

            migrationBuilder.DropIndex(
                name: "IX_Stories_MaskingTypeId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "MaskingTypeId",
                table: "Stories");

            migrationBuilder.AddColumn<int>(
                name: "MaskingType",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
