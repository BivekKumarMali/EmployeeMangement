using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Web.Migrations
{
    public partial class excludedRoleFromDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdentityRoleId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Employees",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
