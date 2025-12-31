using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextGen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Quiz_To_Json_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Quiz",
                table: "GeneratedTexts",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quiz",
                table: "GeneratedTexts");
        }
    }
}
