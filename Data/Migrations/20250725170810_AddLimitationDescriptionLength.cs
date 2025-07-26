using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLimitationDescriptionLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ticket",
                type: "nvarchar(900)",
                maxLength: 900,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ticket",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(900)",
                oldMaxLength: 900);
        }
    }
}
