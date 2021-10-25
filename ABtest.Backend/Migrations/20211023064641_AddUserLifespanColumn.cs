using Microsoft.EntityFrameworkCore.Migrations;

namespace ABtest.Migrations
{
    public partial class AddUserLifespanColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LifespanInDays",
                table: "User",
                type: "double precision",
                nullable: false,
                computedColumnSql: "\"DateLastActivity\" - \"DateRegistration\"",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_LifespanInDays",
                table: "User",
                column: "LifespanInDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_LifespanInDays",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LifespanInDays",
                table: "User");
        }
    }
}
