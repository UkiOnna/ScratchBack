using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class ChangeTaskFieldDescriptionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decription",
                table: "Task");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Task",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Task");

            migrationBuilder.AddColumn<string>(
                name: "Decription",
                table: "Task",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
