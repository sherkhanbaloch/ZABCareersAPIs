using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZABCareersAPIs.Migrations
{
    /// <inheritdoc />
    public partial class EmailOTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerified",
                table: "Tbl_Candidates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTP",
                table: "Tbl_Candidates",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailVerified",
                table: "Tbl_Candidates");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "Tbl_Candidates");
        }
    }
}
