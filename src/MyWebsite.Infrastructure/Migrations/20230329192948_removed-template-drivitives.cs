using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebsite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removedtemplatedrivitives : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "dbo",
                table: "MainInfos");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc6daba2-b71e-4da6-833f-090a3d3c5824",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SignUpDateTime" },
                values: new object[] { "d3779b87-11aa-4957-b6c6-0d53c09abb6a", "eeb09fd8-2da8-4ded-bafd-e6f32843f1f3", new DateTime(2023, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "MainInfos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "MainInfos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "dbo",
                table: "MainInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc6daba2-b71e-4da6-833f-090a3d3c5824",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SignUpDateTime" },
                values: new object[] { "2da4626c-d398-43b6-bafc-92b215ca862f", "2744d855-59d4-4431-8a6a-d43140de585a", new DateTime(2023, 2, 16, 17, 18, 28, 515, DateTimeKind.Local).AddTicks(6840) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MainInfos",
                columns: new[] { "Id", "DarkImagePath", "Description", "Discriminator", "IsActive", "IsDelete", "LangId", "LightImagePath" },
                values: new object[,]
                {
                    { 1, "no image", "no desc", "FirstTempInfo", true, false, 1, "no image" },
                    { 2, "no image", "no desc", "FirstTempInfo", true, false, 2, "no image" }
                });
        }
    }
}
