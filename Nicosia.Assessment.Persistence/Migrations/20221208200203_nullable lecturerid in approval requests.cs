using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nicosia.Assessment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class nullablelectureridinapprovalrequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequest_Lecturers_LecturerId",
                table: "ApprovalRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequest_Sections_SectionId",
                table: "ApprovalRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequest_Students_StudentId",
                table: "ApprovalRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApprovalRequest",
                table: "ApprovalRequest");

            migrationBuilder.RenameTable(
                name: "ApprovalRequest",
                newName: "ApprovalRequests");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRequest_StudentId",
                table: "ApprovalRequests",
                newName: "IX_ApprovalRequests_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRequest_SectionId",
                table: "ApprovalRequests",
                newName: "IX_ApprovalRequests_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRequest_LecturerId",
                table: "ApprovalRequests",
                newName: "IX_ApprovalRequests_LecturerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApprovalRequests",
                table: "ApprovalRequests",
                column: "ApprovalRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Lecturers_LecturerId",
                table: "ApprovalRequests",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Sections_SectionId",
                table: "ApprovalRequests",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Students_StudentId",
                table: "ApprovalRequests",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Lecturers_LecturerId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Sections_SectionId",
                table: "ApprovalRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Students_StudentId",
                table: "ApprovalRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApprovalRequests",
                table: "ApprovalRequests");

            migrationBuilder.RenameTable(
                name: "ApprovalRequests",
                newName: "ApprovalRequest");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRequests_StudentId",
                table: "ApprovalRequest",
                newName: "IX_ApprovalRequest_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRequests_SectionId",
                table: "ApprovalRequest",
                newName: "IX_ApprovalRequest_SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApprovalRequests_LecturerId",
                table: "ApprovalRequest",
                newName: "IX_ApprovalRequest_LecturerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApprovalRequest",
                table: "ApprovalRequest",
                column: "ApprovalRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequest_Lecturers_LecturerId",
                table: "ApprovalRequest",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequest_Sections_SectionId",
                table: "ApprovalRequest",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "SectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequest_Students_StudentId",
                table: "ApprovalRequest",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
