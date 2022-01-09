using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitCoinPriceMonitor.Infrastructure.Persistance.Migrations
{
    public partial class AddComentsAndHeaderParametersFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "31f6847c-ea20-4210-895b-fbcb7dbb21b7");

            migrationBuilder.DeleteData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "ff019736-989b-4197-912f-9298554cd939");

            migrationBuilder.AddColumn<string>(
                name: "HeaderParameters",
                table: "PriceSources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "PriceSnapshots",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PriceSources",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatorId", "HeaderParameters", "Name", "UpdateTimeStamp", "UpdaterId", "Url" },
                values: new object[] { "627a8a25-0000-0000-0000-000000000001", new DateTime(2022, 1, 5, 5, 48, 49, 140, DateTimeKind.Utc).AddTicks(4481), "00000000-0000-0000-0000-000000000000", null, "Bit Stamp", null, null, "https://www.bitstamp.net/api/ticker/" });

            migrationBuilder.InsertData(
                table: "PriceSources",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatorId", "HeaderParameters", "Name", "UpdateTimeStamp", "UpdaterId", "Url" },
                values: new object[] { "627a8a25-0000-0000-0000-000000000002", new DateTime(2022, 1, 5, 5, 48, 49, 140, DateTimeKind.Utc).AddTicks(4507), "00000000-0000-0000-0000-000000000000", null, "Coin Base", null, null, "https://api.pro.coinbase.com/products/ADA-USD/ticker" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000001");

            migrationBuilder.DeleteData(
                table: "PriceSources",
                keyColumn: "Id",
                keyValue: "627a8a25-0000-0000-0000-000000000002");

            migrationBuilder.DropColumn(
                name: "HeaderParameters",
                table: "PriceSources");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "PriceSnapshots");

            migrationBuilder.InsertData(
                table: "PriceSources",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatorId", "Name", "UpdateTimeStamp", "UpdaterId", "Url" },
                values: new object[] { "31f6847c-ea20-4210-895b-fbcb7dbb21b7", new DateTime(2022, 1, 4, 15, 54, 32, 331, DateTimeKind.Utc).AddTicks(3036), "00000000-0000-0000-0000-000000000000", "Coin Base", null, null, "https://api.pro.coinbase.com/products/ADA-USD/ticker" });

            migrationBuilder.InsertData(
                table: "PriceSources",
                columns: new[] { "Id", "CreatedTimeStamp", "CreatorId", "Name", "UpdateTimeStamp", "UpdaterId", "Url" },
                values: new object[] { "ff019736-989b-4197-912f-9298554cd939", new DateTime(2022, 1, 4, 15, 54, 32, 331, DateTimeKind.Utc).AddTicks(2927), "00000000-0000-0000-0000-000000000000", "Bit Stamp", null, null, "https://www.bitstamp.net/api/ticker/" });
        }
    }
}
