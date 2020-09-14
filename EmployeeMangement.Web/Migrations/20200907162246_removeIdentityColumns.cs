using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Web.Migrations
{
    public partial class removeIdentityColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetRoles_IdentityRoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_IdentityUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdentityRoleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IdentityUserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdentityRoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdentityRoleId",
                table: "Employees",
                column: "IdentityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdentityUserId",
                table: "Employees",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetRoles_IdentityRoleId",
                table: "Employees",
                column: "IdentityRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_IdentityUserId",
                table: "Employees",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
