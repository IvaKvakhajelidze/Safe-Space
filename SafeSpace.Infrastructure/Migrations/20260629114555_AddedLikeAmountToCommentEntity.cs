using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SafeSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLikeAmountToCommentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "likeAmount",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "likeAmount",
                table: "Comments");
        }
    }
}
