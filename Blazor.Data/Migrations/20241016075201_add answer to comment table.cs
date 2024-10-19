using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Data.Migrations
{
    public partial class addanswertocommenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerComment",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2024, 10, 16, 11, 22, 0, 991, DateTimeKind.Local).AddTicks(3481));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerComment",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2024, 10, 12, 12, 51, 40, 323, DateTimeKind.Local).AddTicks(5108));
        }
    }
}
