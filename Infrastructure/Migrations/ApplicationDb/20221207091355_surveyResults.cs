using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ApplicationDb
{
    public partial class surveyResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerText",
                table: "Results",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerText",
                table: "Results");
        }
    }
}
