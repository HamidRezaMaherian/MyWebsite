using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyWebsite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGlobalizationToSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "MainInfos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "MainInfos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "LangId",
                schema: "dbo",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc6daba2-b71e-4da6-833f-090a3d3c5824",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SignUpDateTime" },
                values: new object[] { "2da4626c-d398-43b6-bafc-92b215ca862f", "2744d855-59d4-4431-8a6a-d43140de585a", new DateTime(2023, 2, 16, 17, 18, 28, 515, DateTimeKind.Local).AddTicks(6840) });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_LangId",
                schema: "dbo",
                table: "Skills",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Languages_LangId",
                schema: "dbo",
                table: "Skills",
                column: "LangId",
                principalSchema: "dbo",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Languages_LangId",
                schema: "dbo",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_LangId",
                schema: "dbo",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "LangId",
                schema: "dbo",
                table: "Skills");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc6daba2-b71e-4da6-833f-090a3d3c5824",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "SignUpDateTime" },
                values: new object[] { "7cc73cdc-13ce-4fe3-a3fb-d3eb17f7360c", "745cb0ba-e9b9-4562-8a56-619f5ff88e89", new DateTime(2023, 2, 2, 21, 8, 3, 510, DateTimeKind.Local).AddTicks(1691) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MainInfos",
                columns: new[] { "Id", "DarkImagePath", "Description", "Discriminator", "IsActive", "IsDelete", "LangId", "LightImagePath" },
                values: new object[,]
                {
                    { 3, "no image", "no desc", "SecondTempInfo", true, false, 1, "no image" },
                    { 4, "no image", "no desc", "SecondTempInfo", true, false, 2, "no image" }
                });
        }
    }
}
