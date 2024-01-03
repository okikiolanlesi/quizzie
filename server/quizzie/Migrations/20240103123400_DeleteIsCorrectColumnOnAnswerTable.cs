using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzie.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIsCorrectColumnOnAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
