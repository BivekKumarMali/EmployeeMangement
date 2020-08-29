using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeMangement.Web.Migrations
{
    public partial class frist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Did = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Did);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Eid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Surname = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Qualification = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<long>(nullable: false),
                    Did = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Eid);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_Did",
                        column: x => x.Did,
                        principalTable: "Departments",
                        principalColumn: "Did",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Did",
                table: "Employees",
                column: "Did");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
