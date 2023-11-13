using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.OnlineShop.Core.Migrations
{
    public partial class Add_Column_To_Table_Emails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Emails");
        }
    }
}
