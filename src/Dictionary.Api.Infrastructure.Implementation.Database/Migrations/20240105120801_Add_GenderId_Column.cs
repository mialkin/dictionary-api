using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dictionary.Api.Infrastructure.Implementation.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_GenderId_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "translation",
                table: "words",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "gender_id",
                table: "words",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender_id",
                table: "words");

            migrationBuilder.AlterColumn<string>(
                name: "translation",
                table: "words",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
