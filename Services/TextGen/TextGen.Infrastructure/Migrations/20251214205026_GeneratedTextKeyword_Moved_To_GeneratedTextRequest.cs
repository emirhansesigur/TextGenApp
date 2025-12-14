using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextGen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GeneratedTextKeyword_Moved_To_GeneratedTextRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneratedTextKeywords_GeneratedTexts_GeneratedTextId",
                table: "GeneratedTextKeywords");

            migrationBuilder.DropColumn(
                name: "UserWordListId",
                table: "GeneratedTextRequests");

            migrationBuilder.RenameColumn(
                name: "GeneratedTextId",
                table: "GeneratedTextKeywords",
                newName: "GeneratedTextRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_GeneratedTextKeywords_GeneratedTextId",
                table: "GeneratedTextKeywords",
                newName: "IX_GeneratedTextKeywords_GeneratedTextRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneratedTextKeywords_GeneratedTextRequests_GeneratedTextRe~",
                table: "GeneratedTextKeywords",
                column: "GeneratedTextRequestId",
                principalTable: "GeneratedTextRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneratedTextKeywords_GeneratedTextRequests_GeneratedTextRe~",
                table: "GeneratedTextKeywords");

            migrationBuilder.RenameColumn(
                name: "GeneratedTextRequestId",
                table: "GeneratedTextKeywords",
                newName: "GeneratedTextId");

            migrationBuilder.RenameIndex(
                name: "IX_GeneratedTextKeywords_GeneratedTextRequestId",
                table: "GeneratedTextKeywords",
                newName: "IX_GeneratedTextKeywords_GeneratedTextId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserWordListId",
                table: "GeneratedTextRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_GeneratedTextKeywords_GeneratedTexts_GeneratedTextId",
                table: "GeneratedTextKeywords",
                column: "GeneratedTextId",
                principalTable: "GeneratedTexts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
