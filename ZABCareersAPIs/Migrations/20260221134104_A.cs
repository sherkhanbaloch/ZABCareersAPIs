using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZABCareersAPIs.Migrations
{
    /// <inheritdoc />
    public partial class A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherBenifits",
                table: "Tbl_Jobs",
                newName: "OtherBenefits");

            migrationBuilder.AlterColumn<string>(
                name: "FeaturedImageUrl",
                table: "Tbl_Jobs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherBenefits",
                table: "Tbl_Jobs",
                newName: "OtherBenifits");

            migrationBuilder.AlterColumn<string>(
                name: "FeaturedImageUrl",
                table: "Tbl_Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
