using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class PasswordColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "Movie",
                type: "character varying(2020)",
                maxLength: 2020,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "Movie",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2020)",
                oldMaxLength: 2020);
        }
    }
}
