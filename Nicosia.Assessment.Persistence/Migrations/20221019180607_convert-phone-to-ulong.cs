using Microsoft.EntityFrameworkCore.Migrations;

namespace Nicosia.Assessment.Persistence.Migrations
{
    public partial class ConvertPhoneTouLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ulong>(
                name: "PhoneNumber",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER");
        }
    }
}
