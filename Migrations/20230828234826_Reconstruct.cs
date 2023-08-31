using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class Reconstruct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Movies",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Movies",
                newName: "id");
        }
    }
}
