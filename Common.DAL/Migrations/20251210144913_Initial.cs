using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FirstNameEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastNameEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProfessionalLevelValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ManagerialLevelValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    HeadId = table.Column<int>(type: "int", nullable: true),
                    RMId = table.Column<int>(type: "int", nullable: true),
                    M1Id = table.Column<int>(type: "int", nullable: true),
                    M2Id = table.Column<int>(type: "int", nullable: true),
                    M3Id = table.Column<int>(type: "int", nullable: true),
                    M4Id = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_M1Id",
                        column: x => x.M1Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_M2Id",
                        column: x => x.M2Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_M3Id",
                        column: x => x.M3Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_M4Id",
                        column: x => x.M4Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Employees_RMId",
                        column: x => x.RMId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ManagerialLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerialLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingRequestStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessConfirmationStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessConfirmationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YesNoOtherOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Translation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YesNoOtherOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HeadId",
                table: "Employees",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_M1Id",
                table: "Employees",
                column: "M1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_M2Id",
                table: "Employees",
                column: "M2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_M3Id",
                table: "Employees",
                column: "M3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_M4Id",
                table: "Employees",
                column: "M4Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RMId",
                table: "Employees",
                column: "RMId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ManagerialLevels");

            migrationBuilder.DropTable(
                name: "MeetingRequestStatuses");

            migrationBuilder.DropTable(
                name: "ProcessConfirmationStatuses");

            migrationBuilder.DropTable(
                name: "ProfessionalLevels");

            migrationBuilder.DropTable(
                name: "SkillLevels");

            migrationBuilder.DropTable(
                name: "YesNoOtherOptions");
        }
    }
}
