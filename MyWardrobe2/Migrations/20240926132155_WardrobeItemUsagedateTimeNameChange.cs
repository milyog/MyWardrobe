using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWardrobe.Migrations
{
    /// <inheritdoc />
    public partial class WardrobeItemUsagedateTimeNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemUsageDateTime",
                table: "WardrobeItemsUsage",
                newName: "WardrobeItemUsageDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WardrobeItemUsageDateTime",
                table: "WardrobeItemsUsage",
                newName: "ItemUsageDateTime");
        }
    }
}
