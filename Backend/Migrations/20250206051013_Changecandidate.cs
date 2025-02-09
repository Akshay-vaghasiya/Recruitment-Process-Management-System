using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Changecandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FkCandidateId",
                table: "user_role",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "candidate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_FkCandidateId",
                table: "user_role",
                column: "FkCandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_candidate_FkCandidateId",
                table: "user_role",
                column: "FkCandidateId",
                principalTable: "candidate",
                principalColumn: "pk_candidate_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_role_candidate_FkCandidateId",
                table: "user_role");

            migrationBuilder.DropIndex(
                name: "IX_user_role_FkCandidateId",
                table: "user_role");

            migrationBuilder.DropColumn(
                name: "FkCandidateId",
                table: "user_role");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "candidate");
        }
    }
}
