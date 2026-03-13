using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZABCareersAPIs.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_EmailAccounts",
                columns: table => new
                {
                    EmailAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailHost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailPort = table.Column<int>(type: "int", nullable: false),
                    EmailUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    EmailAccountStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_EmailAccounts", x => x.EmailAccountId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_EmailAccounts");
        }
    }
}
