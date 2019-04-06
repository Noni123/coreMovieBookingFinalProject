using Microsoft.EntityFrameworkCore.Migrations;

namespace coreUserPanel.Migrations
{
    public partial class Classes236 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudiName",
                table: "Bookings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShowTiming",
                table: "Bookings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudiName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ShowTiming",
                table: "Bookings");
        }
    }
}
