using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextGen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_GeneratedTextRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "GeneratedTexts");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "GeneratedTexts");

            migrationBuilder.DropColumn(
                name: "UserWordListId",
                table: "GeneratedTexts");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "GeneratedTexts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                table: "GeneratedTextKeywords",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "GeneratedTextRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GeneratedTextId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserWordListId = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Topic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedTextRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedTextRequests_GeneratedTexts_GeneratedTextId",
                        column: x => x.GeneratedTextId,
                        principalTable: "GeneratedTexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedTextRequests_GeneratedTextId",
                table: "GeneratedTextRequests",
                column: "GeneratedTextId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedTextRequests");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "GeneratedTexts",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "GeneratedTexts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "GeneratedTexts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserWordListId",
                table: "GeneratedTexts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Keyword",
                table: "GeneratedTextKeywords",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
