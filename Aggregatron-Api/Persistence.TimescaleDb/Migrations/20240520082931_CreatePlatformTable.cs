using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.TimescaleDb.Migrations
{
    /// <inheritdoc />
    public partial class CreatePlatformTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "platform",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ident = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    valid_from = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    valid_to = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    requires_api_key = table.Column<bool>(type: "boolean", nullable: false),
                    requires_api_secret = table.Column<bool>(type: "boolean", nullable: false),
                    requires_api_password = table.Column<bool>(type: "boolean", nullable: false),
                    has_crypto_api = table.Column<bool>(type: "boolean", nullable: false),
                    has_equities_api = table.Column<bool>(type: "boolean", nullable: false),
                    has_user_accounts = table.Column<bool>(type: "boolean", nullable: false),
                    allows_trading = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platform", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "platform");
        }
    }
}
