using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bloggerServer.Migrations
{
    public partial class deletedBoolAnon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAnon",
                table: "PostTable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAnon",
                table: "PostTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
