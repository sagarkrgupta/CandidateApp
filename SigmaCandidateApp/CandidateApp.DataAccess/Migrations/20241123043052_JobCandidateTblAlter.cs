using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class JobCandidateTblAlter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "JobCandidates",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkedInUrl",
                table: "JobCandidates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GitHubUrl",
                table: "JobCandidates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "JobCandidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidates_Email",
                table: "JobCandidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidates_GitHubUrl",
                table: "JobCandidates",
                column: "GitHubUrl");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidates_LinkedInUrl",
                table: "JobCandidates",
                column: "LinkedInUrl");

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidates_PhoneNumber",
                table: "JobCandidates",
                column: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobCandidates_Email",
                table: "JobCandidates");

            migrationBuilder.DropIndex(
                name: "IX_JobCandidates_GitHubUrl",
                table: "JobCandidates");

            migrationBuilder.DropIndex(
                name: "IX_JobCandidates_LinkedInUrl",
                table: "JobCandidates");

            migrationBuilder.DropIndex(
                name: "IX_JobCandidates_PhoneNumber",
                table: "JobCandidates");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "JobCandidates",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkedInUrl",
                table: "JobCandidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GitHubUrl",
                table: "JobCandidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "JobCandidates",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
