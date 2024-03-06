using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressDaliveryMail.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_branches_EndBranchId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_branches_StartBranchId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_users_UserId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_Package_PackageId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_Package_PackageId",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Package",
                table: "Package");

            migrationBuilder.RenameTable(
                name: "Package",
                newName: "packages");

            migrationBuilder.RenameIndex(
                name: "IX_Package_UserId",
                table: "packages",
                newName: "IX_packages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_StartBranchId",
                table: "packages",
                newName: "IX_packages_StartBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_EndBranchId",
                table: "packages",
                newName: "IX_packages_EndBranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_packages",
                table: "packages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_packages_branches_EndBranchId",
                table: "packages",
                column: "EndBranchId",
                principalTable: "branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_packages_branches_StartBranchId",
                table: "packages",
                column: "StartBranchId",
                principalTable: "branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_packages_users_UserId",
                table: "packages",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_packages_PackageId",
                table: "payments",
                column: "PackageId",
                principalTable: "packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_packages_PackageId",
                table: "transactions",
                column: "PackageId",
                principalTable: "packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_packages_branches_EndBranchId",
                table: "packages");

            migrationBuilder.DropForeignKey(
                name: "FK_packages_branches_StartBranchId",
                table: "packages");

            migrationBuilder.DropForeignKey(
                name: "FK_packages_users_UserId",
                table: "packages");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_packages_PackageId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_packages_PackageId",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_packages",
                table: "packages");

            migrationBuilder.RenameTable(
                name: "packages",
                newName: "Package");

            migrationBuilder.RenameIndex(
                name: "IX_packages_UserId",
                table: "Package",
                newName: "IX_Package_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_packages_StartBranchId",
                table: "Package",
                newName: "IX_Package_StartBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_packages_EndBranchId",
                table: "Package",
                newName: "IX_Package_EndBranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Package",
                table: "Package",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_branches_EndBranchId",
                table: "Package",
                column: "EndBranchId",
                principalTable: "branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_branches_StartBranchId",
                table: "Package",
                column: "StartBranchId",
                principalTable: "branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_users_UserId",
                table: "Package",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_Package_PackageId",
                table: "payments",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_Package_PackageId",
                table: "transactions",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
