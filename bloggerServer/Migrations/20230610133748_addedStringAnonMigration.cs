using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bloggerServer.Migrations
{
    public partial class addedStringAnonMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "isAnon",
                table: "PostTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAnon",
                table: "PostTable");
        }
    }
}
