using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phanlichphongkham.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentHospitals",
                columns: table => new
                {
                    DepartmentHospital_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentHospital_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentHospital_id_posgres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentHospitals", x => x.DepartmentHospital_Id);
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
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Examination_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sepicaltys",
                columns: table => new
                {
                    Sepicalty_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sepicalty_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sepicalty_id_posgres = table.Column<int>(type: "int", nullable: true),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sepicaltys", x => x.Sepicalty_Id);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Zone_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zone_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zone_Id_posgres = table.Column<int>(type: "int", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Zone_Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doctor_Id_progres = table.Column<int>(type: "int", nullable: false),
                    DepartmentHospital_Id = table.Column<int>(type: "int", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctor_Id);
                    table.ForeignKey(
                        name: "FK_Doctors_DepartmentHospitals_DepartmentHospital_Id",
                        column: x => x.DepartmentHospital_Id,
                        principalTable: "DepartmentHospitals",
                        principalColumn: "DepartmentHospital_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Room_Id = table.Column<int>(type: "int", nullable: false),
                    Room_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zone_id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Room_id_posgres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Room_Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Zones_Room_Id",
                        column: x => x.Room_Id,
                        principalTable: "Zones",
                        principalColumn: "Zone_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SepcialtyJoinZones",
                columns: table => new
                {
                    Zone_Id = table.Column<int>(type: "int", nullable: false),
                    Specialty_id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SepcialtyJoinZones", x => x.Zone_Id);
                    table.ForeignKey(
                        name: "FK_SepcialtyJoinZones_Sepicaltys_Specialty_id",
                        column: x => x.Specialty_id,
                        principalTable: "Sepicaltys",
                        principalColumn: "Sepicalty_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SepcialtyJoinZones_Zones_Zone_Id",
                        column: x => x.Zone_Id,
                        principalTable: "Zones",
                        principalColumn: "Zone_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicePrice",
                columns: table => new
                {
                    ServicePrice_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicePrice_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zone_Id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePrice_Id_progres = table.Column<int>(type: "int", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePrice", x => x.ServicePrice_Id);
                    table.ForeignKey(
                        name: "FK_ServicePrice_Zones_Zone_Id",
                        column: x => x.Zone_Id,
                        principalTable: "Zones",
                        principalColumn: "Zone_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentalAppointmentSchedulings",
                columns: table => new
                {
                    DepartmentalAppointmentScheduling_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentalAppointmentScheduling_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Week = table.Column<int>(type: "int", nullable: false),
                    DayInWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInWeek = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Specialty_id = table.Column<int>(type: "int", nullable: false),
                    Room_id = table.Column<int>(type: "int", nullable: false),
                    Examination_Id = table.Column<int>(type: "int", nullable: false),
                    Doctor_id = table.Column<int>(type: "int", nullable: false),
                    DepartmentHospital_Id = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentalAppointmentSchedulings", x => x.DepartmentalAppointmentScheduling_Id);
                    table.ForeignKey(
                        name: "FK_DepartmentalAppointmentSchedulings_DepartmentHospitals_DepartmentHospital_Id",
                        column: x => x.DepartmentHospital_Id,
                        principalTable: "DepartmentHospitals",
                        principalColumn: "DepartmentHospital_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentalAppointmentSchedulings_Doctors_Doctor_id",
                        column: x => x.Doctor_id,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentalAppointmentSchedulings_Examinations_Examination_Id",
                        column: x => x.Examination_Id,
                        principalTable: "Examinations",
                        principalColumn: "Examination_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentalAppointmentSchedulings_Rooms_Room_id",
                        column: x => x.Room_id,
                        principalTable: "Rooms",
                        principalColumn: "Room_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentalAppointmentSchedulings_Sepicaltys_Specialty_id",
                        column: x => x.Specialty_id,
                        principalTable: "Sepicaltys",
                        principalColumn: "Sepicalty_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentalAppointmentSchedulings_DepartmentHospital_Id",
                table: "DepartmentalAppointmentSchedulings",
                column: "DepartmentHospital_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentalAppointmentSchedulings_Doctor_id",
                table: "DepartmentalAppointmentSchedulings",
                column: "Doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentalAppointmentSchedulings_Examination_Id",
                table: "DepartmentalAppointmentSchedulings",
                column: "Examination_Id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentalAppointmentSchedulings_Room_id",
                table: "DepartmentalAppointmentSchedulings",
                column: "Room_id");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentalAppointmentSchedulings_Specialty_id",
                table: "DepartmentalAppointmentSchedulings",
                column: "Specialty_id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_DepartmentHospital_Id",
                table: "Doctors",
                column: "DepartmentHospital_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SepcialtyJoinZones_Specialty_id",
                table: "SepcialtyJoinZones",
                column: "Specialty_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrice_Zone_Id",
                table: "ServicePrice",
                column: "Zone_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentalAppointmentSchedulings");

            migrationBuilder.DropTable(
                name: "SepcialtyJoinZones");

            migrationBuilder.DropTable(
                name: "ServicePrice");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Sepicaltys");

            migrationBuilder.DropTable(
                name: "DepartmentHospitals");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}
