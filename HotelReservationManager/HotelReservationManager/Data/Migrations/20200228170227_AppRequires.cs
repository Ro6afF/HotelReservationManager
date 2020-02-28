using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationManager.Data.Migrations
{
    public partial class AppRequires : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientReservation_Clients_ClientId",
                table: "ClientReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientReservation_Reservations_ReservationId",
                table: "ClientReservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientReservation",
                table: "ClientReservation");

            migrationBuilder.RenameTable(
                name: "ClientReservation",
                newName: "ClientReservations");

            migrationBuilder.RenameIndex(
                name: "IX_ClientReservation_ReservationId",
                table: "ClientReservations",
                newName: "IX_ClientReservations_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientReservations",
                table: "ClientReservations",
                columns: new[] { "ClientId", "ReservationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientReservations_Clients_ClientId",
                table: "ClientReservations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientReservations_Reservations_ReservationId",
                table: "ClientReservations",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientReservations_Clients_ClientId",
                table: "ClientReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientReservations_Reservations_ReservationId",
                table: "ClientReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientReservations",
                table: "ClientReservations");

            migrationBuilder.RenameTable(
                name: "ClientReservations",
                newName: "ClientReservation");

            migrationBuilder.RenameIndex(
                name: "IX_ClientReservations_ReservationId",
                table: "ClientReservation",
                newName: "IX_ClientReservation_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientReservation",
                table: "ClientReservation",
                columns: new[] { "ClientId", "ReservationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientReservation_Clients_ClientId",
                table: "ClientReservation",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientReservation_Reservations_ReservationId",
                table: "ClientReservation",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
