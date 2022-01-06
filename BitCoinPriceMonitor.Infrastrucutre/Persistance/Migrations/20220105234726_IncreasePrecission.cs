using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitCoinPriceMonitor.Infrastrucutre.Persistance.Migrations
{
    public partial class IncreasePrecission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "PriceSnapshots",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldPrecision: 2);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "PriceSnapshots",
                type: "decimal(2,2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000001",
                column: "CreatedTimeStamp",
                value: new DateTime(2022, 1, 5, 5, 48, 49, 140, DateTimeKind.Utc).AddTicks(4481));

            migrationBuilder.UpdateData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000002",
                column: "CreatedTimeStamp",
                value: new DateTime(2022, 1, 5, 5, 48, 49, 140, DateTimeKind.Utc).AddTicks(4507));
        }
    }
}
