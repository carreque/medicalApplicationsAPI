using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medicalAppointmentsAPI.Migrations
{
    public partial class labelsDBFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemeberShipNumber",
                table: "Users",
                newName: "MemberShipNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberShipNumber",
                table: "Users",
                newName: "MemeberShipNumber");
        }
    }
}
