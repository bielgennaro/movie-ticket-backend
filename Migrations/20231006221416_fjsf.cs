using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class fjsf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "develop",
                table: "Sessions",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "AvailableTickets",
                schema: "develop",
                table: "Sessions",
                newName: "available_tickets");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "develop",
                table: "Movies",
                newName: "title");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                schema: "develop",
                table: "Movies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                schema: "develop",
                table: "Sessions",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "available_tickets",
                schema: "develop",
                table: "Sessions",
                newName: "AvailableTickets");

            migrationBuilder.RenameColumn(
                name: "title",
                schema: "develop",
                table: "Movies",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "develop",
                table: "Movies",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
