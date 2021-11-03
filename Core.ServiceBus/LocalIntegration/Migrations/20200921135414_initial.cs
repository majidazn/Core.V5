using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.ServiceBus.LocalIntegration.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "localIntegrationEventseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "LocalIntegrationEvents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    UniqueId = table.Column<Guid>(nullable: true),
                    ModelName = table.Column<string>(maxLength: 256, nullable: false),
                    ModelNamespace = table.Column<string>(maxLength: 512, nullable: false),
                    JsonBoby = table.Column<string>(nullable: false),
                    BinaryBody = table.Column<byte[]>(nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalIntegrationEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalIntegrationEvents");

            migrationBuilder.DropSequence(
                name: "localIntegrationEventseq");
        }
    }
}
