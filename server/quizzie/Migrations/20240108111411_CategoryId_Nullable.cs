using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzie.Migrations
{
    /// <inheritdoc />
    public partial class CategoryId_Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizCategories_CategoryId",
                table: "Quizzes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Quizzes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizCategories_CategoryId",
                table: "Quizzes",
                column: "CategoryId",
                principalTable: "QuizCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizCategories_CategoryId",
                table: "Quizzes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Quizzes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizCategories_CategoryId",
                table: "Quizzes",
                column: "CategoryId",
                principalTable: "QuizCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
