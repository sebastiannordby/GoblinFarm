using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TelldusAccumulatedSensorLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectricityCost_KwH_Ore = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelldusAccumulatedSensorLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelldusAccumulatedSensorLogs");
        }
    }
}
