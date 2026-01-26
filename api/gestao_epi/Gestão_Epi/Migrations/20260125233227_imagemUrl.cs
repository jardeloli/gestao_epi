using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestão_Epi.Migrations
{
    /// <inheritdoc />
    public partial class imagemUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagemUrl",
                table: "epi",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagemUrl",
                table: "epi");
        }
    }
}
