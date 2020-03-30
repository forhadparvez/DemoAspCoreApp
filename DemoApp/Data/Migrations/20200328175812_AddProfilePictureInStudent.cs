using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoApp.Data.Migrations
{
    public partial class AddProfilePictureInStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Students");
        }
    }
}
