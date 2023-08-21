using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

#pragma warning disable IDE0161
namespace ApplicationRegistry.Data.Migrations
#pragma warning restore IDE0161
{
    /// <inheritdoc />
    public partial class ApplicationRegistryInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ApplicationCode = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => new { x.Code, x.ApplicationCode });
                    table.ForeignKey(
                        name: "FK_Events_Applications_ApplicationCode",
                        column: x => x.ApplicationCode,
                        principalTable: "Applications",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Code", "Name" },
                values: new object[] { "NOTIFICATIONS", "Notifications System" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "ApplicationCode", "Code", "Name" },
                values: new object[,]
                {
                    { "NOTIFICATIONS", "NOTIFICATIONS_WEBHOOK_CREATED", "WebbHook Created" },
                    { "NOTIFICATIONS", "NOTIFICATIONS_WEBHOOK_REMOVED", "WebbHook Removed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ApplicationCode",
                table: "Events",
                column: "ApplicationCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
