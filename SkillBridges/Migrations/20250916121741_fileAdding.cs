using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillBridges.Migrations
{
    /// <inheritdoc />
    public partial class fileAdding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "TaskApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "TaskApplications");
        }
    }
}
