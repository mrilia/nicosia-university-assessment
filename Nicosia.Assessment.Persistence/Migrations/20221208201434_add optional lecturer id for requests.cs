using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nicosia.Assessment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addoptionallectureridforrequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Lecturers_LecturerId",
                table: "ApprovalRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "LecturerId",
                table: "ApprovalRequests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Lecturers_LecturerId",
                table: "ApprovalRequests",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Lecturers_LecturerId",
                table: "ApprovalRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "LecturerId",
                table: "ApprovalRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Lecturers_LecturerId",
                table: "ApprovalRequests",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
