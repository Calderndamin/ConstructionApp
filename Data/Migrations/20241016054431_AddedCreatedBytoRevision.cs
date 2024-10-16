using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppAttempt.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreatedBytoRevision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Revision",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Revision");
        }
    }
}
