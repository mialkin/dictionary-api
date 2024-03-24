using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dictionary.Api.Infrastructure.Implementation.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Gender_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "gender_feminine",
                table: "words",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "gender_masculine",
                table: "words",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "gender_neuter",
                table: "words",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender_feminine",
                table: "words");

            migrationBuilder.DropColumn(
                name: "gender_masculine",
                table: "words");

            migrationBuilder.DropColumn(
                name: "gender_neuter",
                table: "words");
        }
    }
}
