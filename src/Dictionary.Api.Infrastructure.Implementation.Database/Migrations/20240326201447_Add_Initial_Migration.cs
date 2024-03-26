using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dictionary.Api.Infrastructure.Implementation.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "words",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    language_id = table.Column<int>(type: "integer", nullable: false),
                    gender_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    transcription = table.Column<string>(type: "text", nullable: true),
                    gender_masculine = table.Column<bool>(type: "boolean", nullable: false),
                    gender_feminine = table.Column<bool>(type: "boolean", nullable: false),
                    gender_neuter = table.Column<bool>(type: "boolean", nullable: false),
                    translation = table.Column<string>(type: "text", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_words", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_words_language_id_name",
                table: "words",
                columns: new[] { "language_id", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "words");
        }
    }
}
