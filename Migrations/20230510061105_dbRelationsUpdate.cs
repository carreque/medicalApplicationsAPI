using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medicalAppointmentsAPI.Migrations
{
    public partial class dbRelationsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cita_id",
                table: "Diagnosis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Patient_id",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cita_id",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "Patient_id",
                table: "Appointment");
        }
    }
}
