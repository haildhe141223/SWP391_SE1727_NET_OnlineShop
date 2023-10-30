using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.OnlineShop.Core.Migrations
{
    public partial class Update_Relationship_Voucher_ProductVoucher_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVouchers_Vouchers_ProductId",
                table: "ProductVouchers");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVouchers_VoucherId",
                table: "ProductVouchers",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVouchers_Vouchers_VoucherId",
                table: "ProductVouchers",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVouchers_Vouchers_VoucherId",
                table: "ProductVouchers");

            migrationBuilder.DropIndex(
                name: "IX_ProductVouchers_VoucherId",
                table: "ProductVouchers");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVouchers_Vouchers_ProductId",
                table: "ProductVouchers",
                column: "ProductId",
                principalTable: "Vouchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
