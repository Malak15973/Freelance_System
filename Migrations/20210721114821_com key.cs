using Microsoft.EntityFrameworkCore.Migrations;

namespace Freelance_System.Migrations
{
    public partial class comkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proposal",
                table: "Proposal");

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "Proposal",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proposal",
                table: "Proposal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proposal",
                table: "Proposal");

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "Proposal",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proposal",
                table: "Proposal",
                columns: new[] { "Id", "PostId", "FreelancerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Proposal_AspNetUsers_FreelancerId",
                table: "Proposal",
                column: "FreelancerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
