using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedMovieCastsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCast_Casts_CastId",
                table: "MovieCast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCast",
                table: "MovieCast");

            migrationBuilder.DropIndex(
                name: "IX_MovieCast_CastId",
                table: "MovieCast");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieCast");

            migrationBuilder.RenameTable(
                name: "MovieCast",
                newName: "MovieCasts");

            migrationBuilder.AlterColumn<string>(
                name: "Character",
                table: "MovieCasts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCasts",
                table: "MovieCasts",
                columns: new[] { "CastId", "MovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCasts_MovieId",
                table: "MovieCasts",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCasts_Casts_CastId",
                table: "MovieCasts",
                column: "CastId",
                principalTable: "Casts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCasts_Movies_MovieId",
                table: "MovieCasts",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCasts_Casts_CastId",
                table: "MovieCasts");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCasts_Movies_MovieId",
                table: "MovieCasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCasts",
                table: "MovieCasts");

            migrationBuilder.DropIndex(
                name: "IX_MovieCasts_MovieId",
                table: "MovieCasts");

            migrationBuilder.RenameTable(
                name: "MovieCasts",
                newName: "MovieCast");

            migrationBuilder.AlterColumn<string>(
                name: "Character",
                table: "MovieCast",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieCast",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCast",
                table: "MovieCast",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCast_CastId",
                table: "MovieCast",
                column: "CastId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCast_Casts_CastId",
                table: "MovieCast",
                column: "CastId",
                principalTable: "Casts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
