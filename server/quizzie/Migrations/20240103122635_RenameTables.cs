using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzie.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Question_QuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_Quizzies_QuizId",
                table: "Question");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizSessions_Quizzies_QuizId",
                table: "QuizSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzies_QuizCategories_CategoryId",
                table: "Quizzies");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzies_Users_UserId",
                table: "Quizzies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizzies",
                table: "Quizzies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.RenameTable(
                name: "Quizzies",
                newName: "Quizzes");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzies_UserId",
                table: "Quizzes",
                newName: "IX_Quizzes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzies_CategoryId",
                table: "Quizzes",
                newName: "IX_Quizzes_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuizId",
                table: "Questions",
                newName: "IX_Questions_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizSessions_Quizzes_QuizId",
                table: "QuizSessions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizCategories_CategoryId",
                table: "Quizzes",
                column: "CategoryId",
                principalTable: "QuizCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Users_UserId",
                table: "Quizzes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizzes_QuizId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizSessions_Quizzes_QuizId",
                table: "QuizSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizCategories_CategoryId",
                table: "Quizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Users_UserId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Quizzes",
                newName: "Quizzies");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_UserId",
                table: "Quizzies",
                newName: "IX_Quizzies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Quizzes_CategoryId",
                table: "Quizzies",
                newName: "IX_Quizzies_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizId",
                table: "Question",
                newName: "IX_Question_QuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizzies",
                table: "Quizzies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Question_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Question_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Quizzies_QuizId",
                table: "Question",
                column: "QuizId",
                principalTable: "Quizzies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizSessions_Quizzies_QuizId",
                table: "QuizSessions",
                column: "QuizId",
                principalTable: "Quizzies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzies_QuizCategories_CategoryId",
                table: "Quizzies",
                column: "CategoryId",
                principalTable: "QuizCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzies_Users_UserId",
                table: "Quizzies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
