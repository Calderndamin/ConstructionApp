using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppAttempt.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountDueToRevision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountDue",
                table: "Revision",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountDue",
                table: "Revision");
        }
    }
}
