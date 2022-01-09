using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitCoinPriceMonitor.Infrastructure.Persistance.Migrations
{
    public partial class MakeCommentsOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "PriceSnapshots",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);

            migrationBuilder.UpdateData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000001",
                column: "CreatedTimeStamp",
                value: new DateTime(2022, 1, 5, 23, 49, 46, 670, DateTimeKind.Utc).AddTicks(8330));

            migrationBuilder.UpdateData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000002",
                column: "CreatedTimeStamp",
                value: new DateTime(2022, 1, 5, 23, 49, 46, 670, DateTimeKind.Utc).AddTicks(8363));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "PriceSnapshots",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000001",
                column: "CreatedTimeStamp",
                value: new DateTime(2022, 1, 5, 23, 47, 26, 559, DateTimeKind.Utc).AddTicks(5887));

            migrationBuilder.UpdateData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000002",
                column: "CreatedTimeStamp",
                value: new DateTime(2022, 1, 5, 23, 47, 26, 559, DateTimeKind.Utc).AddTicks(5926));
        }
    }
}
