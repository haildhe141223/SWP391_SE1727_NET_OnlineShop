using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.OnlineShop.Core.Migrations
{
    public partial class Add_Column_VoucherType_And_Column_Value_To_Voucher_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Vouchers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Vouchers");
        }
    }
}
