using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppAttempt.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalPaidtoContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPaid",
                table: "Contract",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ContractID",
                table: "Transactions",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Contract_ContractID",
                table: "Transactions",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Contract_ContractID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ContractID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalPaid",
                table: "Contract");
        }
    }
}
