using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeStoryTitleRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsToShow",
                table: "Stories");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Stories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Stories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CommentsToShow",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
