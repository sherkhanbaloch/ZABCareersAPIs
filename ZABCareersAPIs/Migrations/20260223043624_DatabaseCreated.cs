using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZABCareersAPIs.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Campus",
                columns: table => new
                {
                    CampusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampusLogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CampusLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampusStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Campus", x => x.CampusId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidatePassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CandidateResumeUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResumeLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CandidateStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Candidates", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vacancy = table.Column<int>(type: "int", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsibilities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationAndExperience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherBenefits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobStatus = table.Column<int>(type: "int", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Tbl_Jobs_Tbl_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Tbl_Campus",
                        principalColumn: "CampusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Jobs_Tbl_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Tbl_Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Tbl_Users_Tbl_Campus_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Tbl_Campus",
                        principalColumn: "CampusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_Users_Tbl_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Tbl_Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_AppliedJobs",
                columns: table => new
                {
                    AppliedJobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    ResumeUsedUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrimaryResume = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_AppliedJobs", x => x.AppliedJobId);
                    table.ForeignKey(
                        name: "FK_Tbl_AppliedJobs_Tbl_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Tbl_Candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_AppliedJobs_Tbl_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Tbl_Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ResumeAnalysis",
                columns: table => new
                {
                    ResumeAnalysisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppliedJobId = table.Column<int>(type: "int", nullable: false),
                    MatchedScore = table.Column<float>(type: "real", nullable: false),
                    KeySkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredSkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillsMatched = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissingSkills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AISuggestions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnalyzedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResumeHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ResumeAnalysis", x => x.ResumeAnalysisId);
                    table.ForeignKey(
                        name: "FK_Tbl_ResumeAnalysis_Tbl_AppliedJobs_AppliedJobId",
                        column: x => x.AppliedJobId,
                        principalTable: "Tbl_AppliedJobs",
                        principalColumn: "AppliedJobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_AppliedJobs_CandidateId",
                table: "Tbl_AppliedJobs",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_AppliedJobs_JobId",
                table: "Tbl_AppliedJobs",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Jobs_CampusId",
                table: "Tbl_Jobs",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Jobs_DepartmentId",
                table: "Tbl_Jobs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ResumeAnalysis_AppliedJobId",
                table: "Tbl_ResumeAnalysis",
                column: "AppliedJobId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Users_CampusId",
                table: "Tbl_Users",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Users_RoleId",
                table: "Tbl_Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Messages");

            migrationBuilder.DropTable(
                name: "Tbl_ResumeAnalysis");

            migrationBuilder.DropTable(
                name: "Tbl_Users");

            migrationBuilder.DropTable(
                name: "Tbl_AppliedJobs");

            migrationBuilder.DropTable(
                name: "Tbl_Roles");

            migrationBuilder.DropTable(
                name: "Tbl_Candidates");

            migrationBuilder.DropTable(
                name: "Tbl_Jobs");

            migrationBuilder.DropTable(
                name: "Tbl_Campus");

            migrationBuilder.DropTable(
                name: "Tbl_Departments");
        }
    }
}
