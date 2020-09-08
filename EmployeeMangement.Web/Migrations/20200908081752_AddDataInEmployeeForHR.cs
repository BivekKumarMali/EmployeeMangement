using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeMangement.Web.Migrations
{
    public partial class AddDataInEmployeeForHR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Eid", "Address", "ContactNumber", "Did", "Name", "Qualification", "RoleId", "Surname", "UserId" },
                values: new object[] { 2, "assd", 12L, 1, "HR", "yu", "", "Default", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Eid",
                keyValue: 2);
        }
    }
}
