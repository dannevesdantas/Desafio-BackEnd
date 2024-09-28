using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioMottu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "jsonb", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name_value = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    cnpj = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    drivers_license_id = table.Column<int>(type: "integer", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    model = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    license_plate = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    last_rented_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "drivers_licenses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    types = table.Column<List<char>>(type: "character(1)[]", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_drivers_licenses", x => x.id);
                    table.ForeignKey(
                        name: "fk_drivers_licenses_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "rentals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    moto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    duration_start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    duration_end = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    predicted_end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    returned_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    plan_days = table.Column<int>(type: "integer", nullable: false),
                    plan_daily_price = table.Column<decimal>(type: "numeric", nullable: false),
                    plan_daily_fee_percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    plan_aditional_fee_per_day = table.Column<decimal>(type: "numeric", nullable: false),
                    total_price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    total_price_currency = table.Column<string>(type: "text", nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rentals", x => x.id);
                    table.ForeignKey(
                        name: "fk_rentals_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rentals_vehicle_moto_id",
                        column: x => x.moto_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_drivers_licenses_number",
                table: "drivers_licenses",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_drivers_licenses_user_id",
                table: "drivers_licenses",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rentals_moto_id",
                table: "rentals",
                column: "moto_id");

            migrationBuilder.CreateIndex(
                name: "ix_rentals_user_id",
                table: "rentals",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_cnpj",
                table: "users",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "drivers_licenses");

            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "rentals");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vehicles");
        }
    }
}
