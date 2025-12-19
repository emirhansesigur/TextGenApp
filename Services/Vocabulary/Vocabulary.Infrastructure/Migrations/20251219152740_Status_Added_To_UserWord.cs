using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vocabulary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Status_Added_To_UserWord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserWords",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserWords");
        }
    }
}
