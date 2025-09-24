using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillBridges.Migrations
{
    /// <inheritdoc />
    public partial class workSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkSubmissions",
                columns: table => new
                {
                    WorkSubmissionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfessionalProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSubmissions", x => x.WorkSubmissionId);
                    table.ForeignKey(
                        name: "FK_WorkSubmissions_ProfessionalProfiles_ProfessionalProfileId",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "ProfessionalProfiles",
                        principalColumn: "ProfessionalProfileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSubmissions_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSubmissions_ProfessionalProfileId",
                table: "WorkSubmissions",
                column: "ProfessionalProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSubmissions_TaskId",
                table: "WorkSubmissions",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkSubmissions");
        }
    }
}
