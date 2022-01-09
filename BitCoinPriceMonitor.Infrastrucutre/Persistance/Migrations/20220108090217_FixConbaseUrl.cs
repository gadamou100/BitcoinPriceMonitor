using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitCoinPriceMonitor.Infrastructure.Persistance.Migrations
{
    public partial class FixConbaseUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000001",
                column: "CreatedTimeStamp",
                value: new DateTime(2022, 1, 8, 9, 2, 16, 828, DateTimeKind.Utc).AddTicks(8151));

            migrationBuilder.UpdateData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000002",
                columns: new[] { "CreatedTimeStamp", "Url" },
                values: new object[] { new DateTime(2022, 1, 8, 9, 2, 16, 828, DateTimeKind.Utc).AddTicks(8191), "https://api.pro.coinbase.com/products/BTC-USD/ticker" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedTimeStamp", "Url" },
                values: new object[] { new DateTime(2022, 1, 5, 23, 49, 46, 670, DateTimeKind.Utc).AddTicks(8363), "https://api.pro.coinbase.com/products/ADA-USD/ticker" });
        }
    }
}
