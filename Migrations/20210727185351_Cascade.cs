using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelance_System.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rate_AspNetUsers_FreelancerId",
                table: "Rate");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedPosts_AspNetUsers_FreelancerId",
                table: "SavedPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_AspNetUsers_FreelancerId",
                table: "Rate",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPosts_AspNetUsers_FreelancerId",
                table: "SavedPosts",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rate_AspNetUsers_FreelancerId",
                table: "Rate");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedPosts_AspNetUsers_FreelancerId",
                table: "SavedPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_AspNetUsers_FreelancerId",
                table: "Rate",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPosts_AspNetUsers_FreelancerId",
                table: "SavedPosts",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
