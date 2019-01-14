using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPIAssignment1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountProfiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProfiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    accountDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ClientAccounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(nullable: false),
                    ClientID = table.Column<int>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAccounts", x => new { x.AccountID, x.ClientID });
                    table.ForeignKey(
                        name: "FK_ClientAccounts_AccountTypes_AccountID",
                        column: x => x.AccountID,
                        principalTable: "AccountTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientAccounts_AccountProfiles_ClientID",
                        column: x => x.ClientID,
                        principalTable: "AccountProfiles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccountProfiles",
                columns: new[] { "ID", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Barb", "Jones" },
                    { 2, "Bob", "Applewood" }
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "ID", "accountDescription" },
                values: new object[,]
                {
                    { 1, null },
                    { 2, null }
                });

            migrationBuilder.InsertData(
                table: "ClientAccounts",
                columns: new[] { "AccountID", "ClientID", "Balance" },
                values: new object[] { 2, 1, 455.53m });

            migrationBuilder.InsertData(
                table: "ClientAccounts",
                columns: new[] { "AccountID", "ClientID", "Balance" },
                values: new object[] { 1, 2, 101.51m });

            migrationBuilder.CreateIndex(
                name: "IX_ClientAccounts_ClientID",
                table: "ClientAccounts",
                column: "ClientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientAccounts");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "AccountProfiles");
        }
    }
}
