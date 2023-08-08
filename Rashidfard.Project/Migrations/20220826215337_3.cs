using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rashidfard.Project.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sum",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sum",
                table: "OrderDetails");
        }
    }
}
