using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeMangement.Web.Migrations
{
    public partial class RoleIdInDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Departments");
        }
    }
}
