﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarEnergyStore.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeriodActive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodActive", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    InternalTemperature = table.Column<double>(type: "float", nullable: false),
                    ExternalTemperature = table.Column<double>(type: "float", nullable: false),
                    MeasurementTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DevicePeriodActiveId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureRecords_PeriodActive_DevicePeriodActiveId",
                        column: x => x.DevicePeriodActiveId,
                        principalTable: "PeriodActive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureRecords_DevicePeriodActiveId",
                table: "TemperatureRecords",
                column: "DevicePeriodActiveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemperatureRecords");

            migrationBuilder.DropTable(
                name: "PeriodActive");
        }
    }
}
