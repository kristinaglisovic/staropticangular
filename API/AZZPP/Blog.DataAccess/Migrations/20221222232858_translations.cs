using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.DataAccess.Migrations
{
    public partial class translations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEng = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DesciptionEng = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTranslations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEng = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DescriptionEng = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Heading1Eng = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Text1Eng = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Heading2Eng = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Text2Eng = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTranslations_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEng = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagTranslations_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTranslations_NameEng",
                table: "CategoryTranslations",
                column: "NameEng",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTranslations_DescriptionEng",
                table: "PostTranslations",
                column: "DescriptionEng");

            migrationBuilder.CreateIndex(
                name: "IX_PostTranslations_Heading1Eng",
                table: "PostTranslations",
                column: "Heading1Eng");

            migrationBuilder.CreateIndex(
                name: "IX_PostTranslations_Heading2Eng",
                table: "PostTranslations",
                column: "Heading2Eng");

            migrationBuilder.CreateIndex(
                name: "IX_PostTranslations_PostId",
                table: "PostTranslations",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTranslations_Text1Eng",
                table: "PostTranslations",
                column: "Text1Eng");

            migrationBuilder.CreateIndex(
                name: "IX_PostTranslations_Text2Eng",
                table: "PostTranslations",
                column: "Text2Eng");

            migrationBuilder.CreateIndex(
                name: "IX_PostTranslations_TitleEng",
                table: "PostTranslations",
                column: "TitleEng",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagTranslations_NameEng",
                table: "TagTranslations",
                column: "NameEng",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagTranslations_TagId",
                table: "TagTranslations",
                column: "TagId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTranslations");

            migrationBuilder.DropTable(
                name: "PostTranslations");

            migrationBuilder.DropTable(
                name: "TagTranslations");
        }
    }
}
