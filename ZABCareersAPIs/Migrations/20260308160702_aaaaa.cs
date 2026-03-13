using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZABCareersAPIs.Migrations
{
    /// <inheritdoc />
    public partial class aaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "SkillsMatched",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResumeHash",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequiredSkills",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MissingSkills",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KeySkills",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Experience",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AISuggestions",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SkillsMatched",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ResumeHash",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RequiredSkills",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MissingSkills",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "KeySkills",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Experience",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AISuggestions",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Certifications",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndustryFit",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InferredSkills",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeniorityLevel",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Strengths",
                table: "Tbl_ResumeAnalysis",
                type: "nvarchar(max)",
                nullable: true);

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
                nullable: true);
        }
    }
}
