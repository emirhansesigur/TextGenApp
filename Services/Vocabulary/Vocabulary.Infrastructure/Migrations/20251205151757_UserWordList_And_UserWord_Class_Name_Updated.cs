using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vocabulary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserWordList_And_UserWord_Class_Name_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_WordLists_WordListId",
                table: "Words");

            migrationBuilder.DropTable(
                name: "WordLists");

            migrationBuilder.RenameColumn(
                name: "WordListId",
                table: "Words",
                newName: "UserWordListId");

            migrationBuilder.RenameIndex(
                name: "IX_Words_WordListId",
                table: "Words",
                newName: "IX_Words_UserWordListId");

            migrationBuilder.AddColumn<string>(
                name: "Meaning",
                table: "Words",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserWordLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWordLists", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Words_UserWordLists_UserWordListId",
                table: "Words",
                column: "UserWordListId",
                principalTable: "UserWordLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_UserWordLists_UserWordListId",
                table: "Words");

            migrationBuilder.DropTable(
                name: "UserWordLists");

            migrationBuilder.DropColumn(
                name: "Meaning",
                table: "Words");

            migrationBuilder.RenameColumn(
                name: "UserWordListId",
                table: "Words",
                newName: "WordListId");

            migrationBuilder.RenameIndex(
                name: "IX_Words_UserWordListId",
                table: "Words",
                newName: "IX_Words_WordListId");

            migrationBuilder.CreateTable(
                name: "WordLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordLists", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Words_WordLists_WordListId",
                table: "Words",
                column: "WordListId",
                principalTable: "WordLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
