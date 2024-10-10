using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorAppAttempt.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "Subcontractor",
                columns: table => new
                {
                    SubcontractorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcontractor", x => x.SubcontractorID);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    SubcontractorID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Progress = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OtherContractDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contract_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Subcontractor_SubcontractorID",
                        column: x => x.SubcontractorID,
                        principalTable: "Subcontractor",
                        principalColumn: "SubcontractorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Revision",
                columns: table => new
                {
                    RevisionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevisionNumber = table.Column<int>(type: "int", nullable: false),
                    ContractID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revision", x => x.RevisionID);
                    table.ForeignKey(
                        name: "FK_Revision_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkAspect",
                columns: table => new
                {
                    WorkAspectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Progress = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAspect", x => x.WorkAspectID);
                    table.ForeignKey(
                        name: "FK_WorkAspect_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkAspectChange",
                columns: table => new
                {
                    WorkAspectChangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAspectID = table.Column<int>(type: "int", nullable: false),
                    OldProgress = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewProgress = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RevisionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAspectChange", x => x.WorkAspectChangeID);
                    table.ForeignKey(
                        name: "FK_WorkAspectChange_Revision_RevisionID",
                        column: x => x.RevisionID,
                        principalTable: "Revision",
                        principalColumn: "RevisionID");
                    table.ForeignKey(
                        name: "FK_WorkAspectChange_WorkAspect_WorkAspectID",
                        column: x => x.WorkAspectID,
                        principalTable: "WorkAspect",
                        principalColumn: "WorkAspectID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProjectID",
                table: "Contract",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SubcontractorID",
                table: "Contract",
                column: "SubcontractorID");

            migrationBuilder.CreateIndex(
                name: "IX_Revision_ContractID",
                table: "Revision",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAspect_ContractID",
                table: "WorkAspect",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAspectChange_RevisionID",
                table: "WorkAspectChange",
                column: "RevisionID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkAspectChange_WorkAspectID",
                table: "WorkAspectChange",
                column: "WorkAspectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkAspectChange");

            migrationBuilder.DropTable(
                name: "Revision");

            migrationBuilder.DropTable(
                name: "WorkAspect");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Subcontractor");
        }
    }
}
