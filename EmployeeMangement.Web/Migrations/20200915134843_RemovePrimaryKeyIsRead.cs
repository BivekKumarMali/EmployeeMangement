using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Web.Migrations
{
    public partial class RemovePrimaryKeyIsRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IsReads",
                table: "IsReads");

            migrationBuilder.DropColumn(
                name: "Rid",
                table: "IsReads");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "IsReads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rid",
                table: "IsReads",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "IsReads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IsReads",
                table: "IsReads",
                column: "Rid");
        }
    }
}
