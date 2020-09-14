using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Web.Migrations
{
    public partial class AddDataInEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Eid", "Address", "ContactNumber", "Did", "Name", "Qualification", "RoleId", "Surname", "UserId" },
                values: new object[] { 1, "assd", 12L, 1, "admin", "yu", "", "Default", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Eid",
                keyValue: 1);
        }
    }
}
