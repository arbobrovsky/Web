using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addTimeWasted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeWasted",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeWasted",
                table: "Products");
        }
    }
}
