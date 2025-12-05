using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vocabulary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserWord_Class_Name_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_UserWordLists_UserWordListId",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Words",
                table: "Words");

            migrationBuilder.RenameTable(
                name: "Words",
                newName: "UserWords");

            migrationBuilder.RenameIndex(
                name: "IX_Words_UserWordListId",
                table: "UserWords",
                newName: "IX_UserWords_UserWordListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWords",
                table: "UserWords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWords_UserWordLists_UserWordListId",
                table: "UserWords",
                column: "UserWordListId",
                principalTable: "UserWordLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWords_UserWordLists_UserWordListId",
                table: "UserWords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWords",
                table: "UserWords");

            migrationBuilder.RenameTable(
                name: "UserWords",
                newName: "Words");

            migrationBuilder.RenameIndex(
                name: "IX_UserWords_UserWordListId",
                table: "Words",
                newName: "IX_Words_UserWordListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Words",
                table: "Words",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_UserWordLists_UserWordListId",
                table: "Words",
                column: "UserWordListId",
                principalTable: "UserWordLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
