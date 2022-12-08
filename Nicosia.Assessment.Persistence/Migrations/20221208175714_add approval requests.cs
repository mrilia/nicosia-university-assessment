using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nicosia.Assessment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addapprovalrequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApprovalRequest",
                columns: table => new
                {
                    ApprovalRequestId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: true),
                    StudentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SectionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastChange = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LecturerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalRequest", x => x.ApprovalRequestId);
                    table.ForeignKey(
                        name: "FK_ApprovalRequest_Lecturers_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Lecturers",
                        principalColumn: "LecturerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalRequest_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApprovalRequest_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequest_LecturerId",
                table: "ApprovalRequest",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequest_SectionId",
                table: "ApprovalRequest",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequest_StudentId",
                table: "ApprovalRequest",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovalRequest");
        }
    }
}
