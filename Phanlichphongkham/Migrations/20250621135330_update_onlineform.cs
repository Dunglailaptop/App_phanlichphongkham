using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phanlichphongkham.Migrations
{
    /// <inheritdoc />
    public partial class update_onlineform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayOfTheWeeks",
                columns: table => new
                {
                    DayOfTheWeek_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfTheWeek_Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfTheWeeks", x => x.DayOfTheWeek_Id);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Examination_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Examination_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Examination_Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    TimeSlot_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSlot_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Examination_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.TimeSlot_Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Examinations_Examination_Id",
                        column: x => x.Examination_Id,
                        principalTable: "Examinations",
                        principalColumn: "Examination_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentalAppointmentSchedulings",
                columns: table => new
                {
                    DepartmentalAppointmentScheduling_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_Id = table.Column<int>(type: "int", nullable: false),
                    Specialty_Id = table.Column<int>(type: "int", nullable: false),
                    DepartmentHospital_Id = table.Column<int>(type: "int", nullable: false),
                    DoctorDepartmentHospital_Id = table.Column<int>(type: "int", nullable: false),
                    DateInWeek = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DayInWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOfTheWeek_Id = table.Column<int>(type: "int", nullable: false),
                    Examination_Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Doctor_Id = table.Column<int>(type: "int", nullable: false),
                    TimeSlot_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentalAppointmentSchedulings", x => x.DepartmentalAppointmentScheduling_Id);
                    table.ForeignKey(
                        name: "FK_DepartmentalAppointmentSchedulings_DayOfTheWeeks_DayOfTheWeek_Id",
                        column: x => x.DayOfTheWeek_Id,
                        principalTable: "DayOfTheWeeks",
                        principalColumn: "DayOfTheWeek_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentalAppointmentSchedulings_Examinations_Examination_Id",
                        column: x => x.Examination_Id,
                        principalTable: "Examinations",
                        principalColumn: "Examination_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentalAppointmentSchedulings_TimeSlots_TimeSlot_Id",
                        column: x => x.TimeSlot_Id,
                        principalTable: "TimeSlots",
                        principalColumn: "TimeSlot_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentalAppointmentSchedulings_DayOfTheWeek_Id",
                table: "DepartmentalAppointmentSchedulings",
                column: "DayOfTheWeek_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentalAppointmentSchedulings_Examination_Id",
                table: "DepartmentalAppointmentSchedulings",
                column: "Examination_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentalAppointmentSchedulings_TimeSlot_Id",
                table: "DepartmentalAppointmentSchedulings",
                column: "TimeSlot_Id");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_Examination_Id",
                table: "TimeSlots",
                column: "Examination_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentalAppointmentSchedulings");

            migrationBuilder.DropTable(
                name: "DayOfTheWeeks");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Examinations");
        }
    }
}
