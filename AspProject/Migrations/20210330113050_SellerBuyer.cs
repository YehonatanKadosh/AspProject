using Microsoft.EntityFrameworkCore.Migrations;

namespace AspProject.Migrations
{
    public partial class SellerBuyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_OwnerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Products",
                newName: "SelerId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Products",
                newName: "BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UserId",
                table: "Products",
                newName: "IX_Products_SelerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_OwnerId",
                table: "Products",
                newName: "IX_Products_BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_BuyerId",
                table: "Products",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SelerId",
                table: "Products",
                column: "SelerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_BuyerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SelerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SelerId",
                table: "Products",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "Products",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SelerId",
                table: "Products",
                newName: "IX_Products_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_BuyerId",
                table: "Products",
                newName: "IX_Products_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_OwnerId",
                table: "Products",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
