using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZABCareersAPIs.Migrations
{
    /// <inheritdoc />
    public partial class NewCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Certifications",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IndustryFit",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InferredSkills",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeniorityLevel",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Strengths",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalYearsExperience",
                table: "Tbl_ResumeAnalysis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Weaknesses",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certifications",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropColumn(
                name: "IndustryFit",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropColumn(
                name: "InferredSkills",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropColumn(
                name: "SeniorityLevel",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropColumn(
                name: "Strengths",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropColumn(
                name: "TotalYearsExperience",
                table: "Tbl_ResumeAnalysis");

            migrationBuilder.DropColumn(
                name: "Weaknesses",
                table: "Tbl_ResumeAnalysis");
        }
    }
}
