using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bloggerServer.Migrations
{
    public partial class deleteListTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hashtags_PostTable_BlogPostId",
                table: "Hashtags");

            migrationBuilder.DropIndex(
                name: "IX_Hashtags_BlogPostId",
                table: "Hashtags");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "Hashtags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogPostId",
                table: "Hashtags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hashtags_BlogPostId",
                table: "Hashtags",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hashtags_PostTable_BlogPostId",
                table: "Hashtags",
                column: "BlogPostId",
                principalTable: "PostTable",
                principalColumn: "Id");
        }
    }
}
