using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzie.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsDeletedToOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Options",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Options");
        }
    }
}
