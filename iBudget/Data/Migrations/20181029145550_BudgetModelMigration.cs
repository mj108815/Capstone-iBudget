using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBudget.Data.Migrations
{
    public partial class BudgetModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budget_Transaction_TransactionID",
                table: "Budget");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Budget_TransactionID",
                table: "Budget");

            migrationBuilder.DropColumn(
                name: "TransactionID",
                table: "Budget");

            migrationBuilder.AddColumn<double>(
                name: "AccountNumber",
                table: "Budget",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Transactions",
                table: "Budget",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Budget");

            migrationBuilder.DropColumn(
                name: "Transactions",
                table: "Budget");

            migrationBuilder.AddColumn<int>(
                name: "TransactionID",
                table: "Budget",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BudgetID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transaction_Budget_BudgetID",
                        column: x => x.BudgetID,
                        principalTable: "Budget",
                        principalColumn: "BudgetID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "FK_Budget_Transaction_TransactionID",
                table: "Budget",
                column: "TransactionID",
                principalTable: "Transaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
