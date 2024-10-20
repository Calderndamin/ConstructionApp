using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppAttempt.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangeOrderID",
                table: "WorkAspect",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChangeOrder",
                columns: table => new
                {
                    ChangeOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeOrder", x => x.ChangeOrderID);
                    table.ForeignKey(
                        name: "FK_ChangeOrder_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkAspect_ChangeOrderID",
                table: "WorkAspect",
                column: "ChangeOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeOrder_ContractID",
                table: "ChangeOrder",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkAspect_ChangeOrder_ChangeOrderID",
                table: "WorkAspect",
                column: "ChangeOrderID",
                principalTable: "ChangeOrder",
                principalColumn: "ChangeOrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkAspect_ChangeOrder_ChangeOrderID",
                table: "WorkAspect");

            migrationBuilder.DropTable(
                name: "ChangeOrder");

            migrationBuilder.DropIndex(
                name: "IX_WorkAspect_ChangeOrderID",
                table: "WorkAspect");

            migrationBuilder.DropColumn(
                name: "ChangeOrderID",
                table: "WorkAspect");
        }
    }
}
