using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dictionary.Api.Infrastructure.Implementation.Database.Migrations
{
    /// <inheritdoc />
    public partial class Use_Id_As_Primary_Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_words",
                table: "words");

            migrationBuilder.AddPrimaryKey(
                name: "pk_words",
                table: "words",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_words_language_id_name",
                table: "words",
                columns: new[] { "language_id", "name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_words",
                table: "words");

            migrationBuilder.DropIndex(
                name: "ix_words_language_id_name",
                table: "words");

            migrationBuilder.AddPrimaryKey(
                name: "pk_words",
                table: "words",
                columns: new[] { "language_id", "name" });
        }
    }
}
