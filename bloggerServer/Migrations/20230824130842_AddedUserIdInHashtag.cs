using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bloggerServer.Migrations
{
    public partial class AddedUserIdInHashtag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Hashtags",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hashtags");
        }
    }
}
