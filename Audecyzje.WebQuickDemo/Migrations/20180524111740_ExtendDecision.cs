using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Audecyzje.WebQuickDemo.Migrations
{
    public partial class ExtendDecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceLink",
                table: "Decision",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedTime",
                table: "Decision",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceLink",
                table: "Decision");

            migrationBuilder.DropColumn(
                name: "UploadedTime",
                table: "Decision");
        }
    }
}
