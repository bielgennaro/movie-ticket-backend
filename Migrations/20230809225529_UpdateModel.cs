using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieTicketApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Movie_MovieId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_MovieId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Session");

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "Movie",
                type: "character varying(1200)",
                maxLength: 1200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "MovieSessions",
                columns: table => new
                {
                    MovieSessionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    SessionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieSessions", x => x.MovieSessionId);
                    table.ForeignKey(
                        name: "FK_MovieSessions_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieSessions_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieSessions_MovieId",
                table: "MovieSessions",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieSessions_SessionId",
                table: "MovieSessions",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieSessions");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Session",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "Movie",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1200)",
                oldMaxLength: 1200);

            migrationBuilder.CreateIndex(
                name: "IX_Session_MovieId",
                table: "Session",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Movie_MovieId",
                table: "Session",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
