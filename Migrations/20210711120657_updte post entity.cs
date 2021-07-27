using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelance_System.Migrations
{
    public partial class updtepostentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Post",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
