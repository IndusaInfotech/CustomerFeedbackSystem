using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class surveys1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Surveys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Surveys");
        }
    }
}
