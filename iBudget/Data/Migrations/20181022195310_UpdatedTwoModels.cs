using Microsoft.EntityFrameworkCore.Migrations;

namespace iBudget.Data.Migrations
{
    public partial class UpdatedTwoModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "FinancialAnalysts",
                newName: "StreetAddress");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "FinancialAnalysts",
                newName: "FinancialAnalystID");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "StreetAddress");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "FinancialAnalysts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityStateZip",
                table: "FinancialAnalysts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "FinancialAnalysts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "FinancialAnalysts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Promotions",
                table: "FinancialAnalysts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityStateZip",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Subscribed",
                table: "Customers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "FinancialAnalysts");

            migrationBuilder.DropColumn(
                name: "CityStateZip",
                table: "FinancialAnalysts");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "FinancialAnalysts");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "FinancialAnalysts");

            migrationBuilder.DropColumn(
                name: "Promotions",
                table: "FinancialAnalysts");

            migrationBuilder.DropColumn(
                name: "CityStateZip",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Subscribed",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "FinancialAnalysts",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "FinancialAnalystID",
                table: "FinancialAnalysts",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Customers",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
