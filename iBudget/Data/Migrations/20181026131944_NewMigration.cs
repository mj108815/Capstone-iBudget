using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBudget.Data.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    BudgetID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    BudgetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransactionID = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    MonthlyEarnings = table.Column<double>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.BudgetID);
                    table.ForeignKey(
                        name: "FK_Budget_Transaction_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "Transaction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budget_TransactionID",
                table: "Budget",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BudgetID",
                table: "Transaction",
                column: "BudgetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Budget_BudgetID",
                table: "Transaction",
                column: "BudgetID",
                principalTable: "Budget",
                principalColumn: "BudgetID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budget_Transaction_TransactionID",
                table: "Budget");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Budget");
        }
    }
}
