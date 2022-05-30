using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtwithidentity.Migrations
{
    public partial class dbpath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DbPath",
                table: "UploadFilestb",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DbPath",
                table: "UploadFilestb");
        }
    }
}
