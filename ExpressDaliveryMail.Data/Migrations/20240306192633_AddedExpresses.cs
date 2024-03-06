using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressDaliveryMail.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Express_branches_BranchId",
                table: "Express");

            migrationBuilder.DropForeignKey(
                name: "FK_Express_transports_TransportId",
                table: "Express");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_Express_ExpressId",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Express",
                table: "Express");

            migrationBuilder.RenameTable(
                name: "Express",
                newName: "expresses");

            migrationBuilder.RenameIndex(
                name: "IX_Express_TransportId",
                table: "expresses",
                newName: "IX_expresses_TransportId");

            migrationBuilder.RenameIndex(
                name: "IX_Express_BranchId",
                table: "expresses",
                newName: "IX_expresses_BranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_expresses",
                table: "expresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_expresses_branches_BranchId",
                table: "expresses",
                column: "BranchId",
                principalTable: "branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expresses_transports_TransportId",
                table: "expresses",
                column: "TransportId",
                principalTable: "transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_expresses_ExpressId",
                table: "transactions",
                column: "ExpressId",
                principalTable: "expresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expresses_branches_BranchId",
                table: "expresses");

            migrationBuilder.DropForeignKey(
                name: "FK_expresses_transports_TransportId",
                table: "expresses");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_expresses_ExpressId",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_expresses",
                table: "expresses");

            migrationBuilder.RenameTable(
                name: "expresses",
                newName: "Express");

            migrationBuilder.RenameIndex(
                name: "IX_expresses_TransportId",
                table: "Express",
                newName: "IX_Express_TransportId");

            migrationBuilder.RenameIndex(
                name: "IX_expresses_BranchId",
                table: "Express",
                newName: "IX_Express_BranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Express",
                table: "Express",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Express_branches_BranchId",
                table: "Express",
                column: "BranchId",
                principalTable: "branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Express_transports_TransportId",
                table: "Express",
                column: "TransportId",
                principalTable: "transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_Express_ExpressId",
                table: "transactions",
                column: "ExpressId",
                principalTable: "Express",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
