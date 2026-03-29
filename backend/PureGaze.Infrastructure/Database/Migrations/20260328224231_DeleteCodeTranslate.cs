using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PureGaze.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCodeTranslate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Codes_CodeId1",
                table: "Templates");

            migrationBuilder.DropTable(
                name: "CodeTranslates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_CodeId1",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CodeId1",
                table: "Templates");

            migrationBuilder.RenameColumn(
                name: "Display",
                table: "Codes",
                newName: "Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToGradeId",
                table: "Codes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "GradeId",
                table: "Codes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Codes_GradeId",
                table: "Codes",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Codes_ToGradeId",
                table: "Codes",
                column: "ToGradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_ProfessionalLevels_GradeId",
                table: "Codes",
                column: "GradeId",
                principalTable: "ProfessionalLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_ProfessionalLevels_ToGradeId",
                table: "Codes",
                column: "ToGradeId",
                principalTable: "ProfessionalLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_ProfessionalLevels_GradeId",
                table: "Codes");

            migrationBuilder.DropForeignKey(
                name: "FK_Codes_ProfessionalLevels_ToGradeId",
                table: "Codes");

            migrationBuilder.DropIndex(
                name: "IX_Codes_GradeId",
                table: "Codes");

            migrationBuilder.DropIndex(
                name: "IX_Codes_ToGradeId",
                table: "Codes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Codes",
                newName: "Display");

            migrationBuilder.AddColumn<int>(
                name: "CodeId1",
                table: "Templates",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ToGradeId",
                table: "Codes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GradeId",
                table: "Codes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CodeTranslates",
                columns: table => new
                {
                    CodeId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    LevelVision = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeTranslates", x => new { x.CodeId, x.Language });
                    table.ForeignKey(
                        name: "FK_CodeTranslates_Codes_CodeId",
                        column: x => x.CodeId,
                        principalTable: "Codes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CodeId1",
                table: "Templates",
                column: "CodeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Codes_CodeId1",
                table: "Templates",
                column: "CodeId1",
                principalTable: "Codes",
                principalColumn: "Id");
        }
    }
}
