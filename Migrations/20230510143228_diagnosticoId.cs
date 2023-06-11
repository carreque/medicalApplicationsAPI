using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medicalAppointmentsAPI.Migrations
{
    public partial class diagnosticoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "diagnostico_id",
                table: "Appointment",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "diagnostico_id",
                table: "Appointment");
        }
    }
}
