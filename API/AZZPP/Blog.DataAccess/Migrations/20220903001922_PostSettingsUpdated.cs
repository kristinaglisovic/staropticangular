using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataAccess.Migrations
{
    public partial class PostSettingsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Heading1",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Heading2",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text1",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text2",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Heading1",
                table: "Posts",
                column: "Heading1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Heading2",
                table: "Posts",
                column: "Heading2");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Text1",
                table: "Posts",
                column: "Text1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Text2",
                table: "Posts",
                column: "Text2");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Heading1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Heading2",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Text1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Text2",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Heading1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Heading2",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Text1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Text2",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
