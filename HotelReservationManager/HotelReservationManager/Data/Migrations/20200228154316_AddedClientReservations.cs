using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationManager.Data.Migrations
{
    public partial class AddedClientReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Reservations_ReservationId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ReservationId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "ClientReservation",
                columns: table => new
                {
                    ClientId = table.Column<string>(nullable: false),
                    ReservationId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientReservation", x => new { x.ClientId, x.ReservationId });
                    table.ForeignKey(
                        name: "FK_ClientReservation_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientReservation_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientReservation_ReservationId",
                table: "ClientReservation",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientReservation");

            migrationBuilder.AddColumn<string>(
                name: "ReservationId",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ReservationId",
                table: "Clients",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Reservations_ReservationId",
                table: "Clients",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
