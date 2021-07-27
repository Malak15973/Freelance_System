using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelance_System.Migrations
{
    public partial class Cascade2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
