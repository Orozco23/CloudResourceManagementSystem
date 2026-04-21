using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudResourceManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InicialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManagedDatabases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    DatabaseEngine = table.Column<string>(type: "TEXT", nullable: false),
                    StorageCapacityGb = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceName = table.Column<string>(type: "TEXT", nullable: false),
                    Region = table.Column<string>(type: "TEXT", nullable: false),
                    BaseHourlyRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Premium = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedDatabases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VirtualMachines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    CpuCores = table.Column<int>(type: "INTEGER", nullable: false),
                    RamMemoryGb = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceName = table.Column<string>(type: "TEXT", nullable: false),
                    Region = table.Column<string>(type: "TEXT", nullable: false),
                    BaseHourlyRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Premium = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualMachines", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagedDatabases");

            migrationBuilder.DropTable(
                name: "VirtualMachines");
        }
    }
}
