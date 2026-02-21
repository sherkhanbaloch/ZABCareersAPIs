using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZABCareersAPIs.Migrations
{
    /// <inheritdoc />
    public partial class addednavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Users_CampusId",
                table: "Tbl_Users",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Users_RoleId",
                table: "Tbl_Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ResumeAnalysis_AppliedJobId",
                table: "Tbl_ResumeAnalysis",
                column: "AppliedJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Jobs_CampusId",
                table: "Tbl_Jobs",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Jobs_DepartmentId",
                table: "Tbl_Jobs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_AppliedJobs_CandidateId",
                table: "Tbl_AppliedJobs",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_AppliedJobs_JobId",
                table: "Tbl_AppliedJobs",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_AppliedJobs_Tbl_Candidates_CandidateId",
                table: "Tbl_AppliedJobs",
                column: "CandidateId",
                principalTable: "Tbl_Candidates",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_AppliedJobs_Tbl_Jobs_JobId",
                table: "Tbl_AppliedJobs",
                column: "JobId",
                principalTable: "Tbl_Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Jobs_Tbl_Campus_CampusId",
                table: "Tbl_Jobs",
                column: "CampusId",
                principalTable: "Tbl_Campus",
                principalColumn: "CampusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Jobs_Tbl_Departments_DepartmentId",
                table: "Tbl_Jobs",
                column: "DepartmentId",
                principalTable: "Tbl_Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_ResumeAnalysis_Tbl_AppliedJobs_AppliedJobId",
                table: "Tbl_ResumeAnalysis",
                column: "AppliedJobId",
                principalTable: "Tbl_AppliedJobs",
                principalColumn: "AppliedJobId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Users_Tbl_Campus_CampusId",
                table: "Tbl_Users",
                column: "CampusId",
                principalTable: "Tbl_Campus",
                principalColumn: "CampusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Users_Tbl_Roles_RoleId",
                table: "Tbl_Users",
                column: "RoleId",
                principalTable: "Tbl_Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_AppliedJobs_Tbl_Candidates_CandidateId",
                table: "Tbl_AppliedJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_AppliedJobs_Tbl_Jobs_JobId",
                table: "Tbl_AppliedJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Jobs_Tbl_Campus_CampusId",
                table: "Tbl_Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Jobs_Tbl_Departments_DepartmentId",
                table: "Tbl_Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_ResumeAnalysis_Tbl_AppliedJobs_AppliedJobId",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Users_Tbl_Campus_CampusId",
                table: "Tbl_Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Users_Tbl_Roles_RoleId",
                table: "Tbl_Users");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Users_CampusId",
                table: "Tbl_Users");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Users_RoleId",
                table: "Tbl_Users");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_ResumeAnalysis_AppliedJobId",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Jobs_CampusId",
                table: "Tbl_Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Jobs_DepartmentId",
                table: "Tbl_Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_AppliedJobs_CandidateId",
                table: "Tbl_AppliedJobs");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_AppliedJobs_JobId",
                table: "Tbl_AppliedJobs");
        }
    }
}
