using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NotificationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Emails_ErrorMessage_Priority",
                table: "Emails");

            migrationBuilder.AddColumn<int>(
                name: "RetryCount",
                table: "Emails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Status_Priority",
                table: "Emails",
                columns: new[] { "Status", "Priority" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Emails_Status_Priority",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "RetryCount",
                table: "Emails");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ErrorMessage_Priority",
                table: "Emails",
                columns: new[] { "ErrorMessage", "Priority" });
        }
    }
}
