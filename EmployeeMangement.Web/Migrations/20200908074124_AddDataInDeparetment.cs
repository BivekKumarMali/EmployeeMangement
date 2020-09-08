using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeMangement.Web.Migrations
{
    public partial class AddDataInDeparetment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Did", "DepartmentName" },
                values: new object[] { 1, "Default" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Did",
                keyValue: 1);
        }
    }
}
