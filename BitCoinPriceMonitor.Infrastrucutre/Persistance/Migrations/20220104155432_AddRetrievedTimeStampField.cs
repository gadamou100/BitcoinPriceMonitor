using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitCoinPriceMonitor.Infrastructure.Persistance.Migrations
{
    public partial class AddRetrievedTimeStampField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PriceSources_CreatedTimeStamp",
                table: "PriceSources");

            migrationBuilder.DropIndex(
                name: "IX_PriceSnapshots_CreatedTimeStamp",
                table: "PriceSnapshots");

            migrationBuilder.AddColumn<DateTime>(
                name: "RetrievedTimeStamp",
                table: "PriceSnapshots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "PriceSources",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatorId", "Name", "UpdateTimeStamp", "UpdaterId", "Url" },
                values: new object[] { "31f6847c-ea20-4210-895b-fbcb7dbb21b7", new DateTime(2022, 1, 4, 15, 54, 32, 331, DateTimeKind.Utc).AddTicks(3036), "00000000-0000-0000-0000-000000000000", "Coin Base", null, null, "https://api.pro.coinbase.com/products/ADA-USD/ticker" });

            migrationBuilder.InsertData(
                table: "PriceSources",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatorId", "Name", "UpdateTimeStamp", "UpdaterId", "Url" },
                values: new object[] { "ff019736-989b-4197-912f-9298554cd939", new DateTime(2022, 1, 4, 15, 54, 32, 331, DateTimeKind.Utc).AddTicks(2927), "00000000-0000-0000-0000-000000000000", "Bit Stamp", null, null, "https://www.bitstamp.net/api/ticker/" });

            migrationBuilder.CreateIndex(
                name: "IX_PriceSnapshots_RetrievedTimeStamp",
                table: "PriceSnapshots",
                column: "RetrievedTimeStamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PriceSnapshots_RetrievedTimeStamp",
                table: "PriceSnapshots");

            migrationBuilder.DeleteData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "31f6847c-ea20-4210-895b-fbcb7dbb21b7");

            migrationBuilder.DeleteData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "ff019736-989b-4197-912f-9298554cd939");

            migrationBuilder.DropColumn(
                name: "RetrievedTimeStamp",
                table: "PriceSnapshots");

            migrationBuilder.CreateIndex(
                name: "IX_PriceSources_CreatedTimeStamp",
                table: "PriceSources",
                column: "CreatedTimeStamp");

            migrationBuilder.CreateIndex(
                name: "IX_PriceSnapshots_CreatedTimeStamp",
                table: "PriceSnapshots",
                column: "CreatedTimeStamp");
        }
    }
}
