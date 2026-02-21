using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZABCareersAPIs.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeaturedImage",
                table: "Tbl_Jobs",
                newName: "FeaturedImageUrl");

            migrationBuilder.RenameColumn(
                name: "CandidateResume",
                table: "Tbl_Candidates",
                newName: "CandidateResumeUrl");

            migrationBuilder.RenameColumn(
                name: "CampusLogo",
                table: "Tbl_Campus",
                newName: "CampusLogoUrl");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Tbl_Departments",
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
            migrationBuilder.RenameColumn(
                name: "FeaturedImageUrl",
                table: "Tbl_Jobs",
                newName: "FeaturedImage");

            migrationBuilder.RenameColumn(
                name: "CandidateResumeUrl",
                table: "Tbl_Candidates",
                newName: "CandidateResume");

            migrationBuilder.RenameColumn(
                name: "CampusLogoUrl",
                table: "Tbl_Campus",
                newName: "CampusLogo");

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentName",
                table: "Tbl_Departments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
