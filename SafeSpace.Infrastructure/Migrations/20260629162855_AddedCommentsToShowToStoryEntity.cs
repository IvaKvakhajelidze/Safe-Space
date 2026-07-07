using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentsToShowToStoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentsToShow",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsToShow",
                table: "Stories");
        }
    }
}
