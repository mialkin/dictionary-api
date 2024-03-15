using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dictionary.Api.Infrastructure.Implementation.Database.Migrations
{
    /// <inheritdoc />
    public partial class Make_LanguageId_Name_Index_Unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_words_language_id_name",
                table: "words");

            migrationBuilder.CreateIndex(
                name: "ix_words_language_id_name",
                table: "words",
                columns: new[] { "language_id", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_words_language_id_name",
                table: "words");

            migrationBuilder.CreateIndex(
                name: "ix_words_language_id_name",
                table: "words",
                columns: new[] { "language_id", "name" });
        }
    }
}
