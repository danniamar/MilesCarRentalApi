using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MilesCarRental.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LocationName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsOrigin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDestination = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Registration = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LocationOrigin = table.Column<bool>(type: "tinyint(50)", maxLength: 50, nullable: false),
                    LocationDestination = table.Column<bool>(type: "tinyint(50)", maxLength: 50, nullable: false),
                    Registration = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    VehicleName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VehicleCapacity = table.Column<int>(type: "int", nullable: false),
                    QuantityLargeSuitcases = table.Column<int>(type: "int", nullable: false),
                    QuantitySmallSuitcases = table.Column<int>(type: "int", nullable: false),
                    Transmission = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAvailable = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "IsDestination", "IsOrigin", "LocationName", "Registration" },
                values: new object[] { new Guid("b29f780f-c2fc-41e0-a5e5-dab0090a4f09"), false, false, "location", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "IsAvailable", "QuantityLargeSuitcases", "QuantitySmallSuitcases", "Transmission", "VehicleCapacity", "VehicleName" },
                values: new object[,]
                {
                    { new Guid("0a2d473b-fe62-4f66-9537-96848c6d5c4b"), true, 1, 1, "Manual", 5, "Kia Picanto" },
                    { new Guid("0c4c3455-4330-4e35-b0a7-9ec7e42c6490"), true, 1, 1, "Automática", 5, "Hyundai Creta" },
                    { new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"), true, 2, 1, "Automática", 5, "Hyundai Accent" },
                    { new Guid("74180e03-50cb-4bf3-b645-ef538f769493"), true, 3, 0, "Manual", 5, "Renault Duster" },
                    { new Guid("7cb616c3-d5d1-4d34-8dd5-d83e51ea3fa8"), true, 1, 1, "Automática", 5, "Changan E-Sta" },
                    { new Guid("b1820ce5-f0a1-4923-b362-b10ebddc7ad4"), true, 1, 1, "Manual", 5, "Suzuki Swift" },
                    { new Guid("d70f3c46-0055-4b30-946a-98486ec75993"), true, 3, 0, "Automática", 5, "Renault Koleos" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
