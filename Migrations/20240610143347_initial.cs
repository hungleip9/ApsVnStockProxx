using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VnStockproxx.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "Vietnamese_CI_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "Vietnamese_CI_AS"),
                    content = table.Column<string>(type: "nvarchar(299)", maxLength: 299, nullable: true, collation: "Vietnamese_CI_AS"),
                    teaser = table.Column<string>(type: "nvarchar(299)", maxLength: 299, nullable: true, collation: "Vietnamese_CI_AS"),
                    viewCount = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CateId = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(299)", maxLength: 299, nullable: false),
                    createdDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_Category",
                        column: x => x.CateId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_CateId",
                table: "Post",
                column: "CateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
