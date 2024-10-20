using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppAttempt.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsExtratoWorkAspect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeOrder_Contract_ContractID",
                table: "ChangeOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAspect_ChangeOrder_ChangeOrderID",
                table: "WorkAspect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeOrder",
                table: "ChangeOrder");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ChangeOrder");

            migrationBuilder.RenameTable(
                name: "ChangeOrder",
                newName: "ChangeOrders");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeOrder_ContractID",
                table: "ChangeOrders",
                newName: "IX_ChangeOrders_ContractID");

            migrationBuilder.AddColumn<bool>(
                name: "IsExtra",
                table: "WorkAspect",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeOrders",
                table: "ChangeOrders",
                column: "ChangeOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeOrders_Contract_ContractID",
                table: "ChangeOrders",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAspect_ChangeOrders_ChangeOrderID",
                table: "WorkAspect",
                column: "ChangeOrderID",
                principalTable: "ChangeOrders",
                principalColumn: "ChangeOrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeOrders_Contract_ContractID",
                table: "ChangeOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkAspect_ChangeOrders_ChangeOrderID",
                table: "WorkAspect");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeOrders",
                table: "ChangeOrders");

            migrationBuilder.DropColumn(
                name: "IsExtra",
                table: "WorkAspect");

            migrationBuilder.RenameTable(
                name: "ChangeOrders",
                newName: "ChangeOrder");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeOrders_ContractID",
                table: "ChangeOrder",
                newName: "IX_ChangeOrder_ContractID");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ChangeOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeOrder",
                table: "ChangeOrder",
                column: "ChangeOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeOrder_Contract_ContractID",
                table: "ChangeOrder",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ContractID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAspect_ChangeOrder_ChangeOrderID",
                table: "WorkAspect",
                column: "ChangeOrderID",
                principalTable: "ChangeOrder",
                principalColumn: "ChangeOrderID");
        }
    }
}
