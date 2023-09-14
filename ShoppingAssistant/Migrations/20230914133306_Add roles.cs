using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingAssistant.Migrations
{
    public partial class Addroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Products_ProductID",
                table: "Packages");

            migrationBuilder.AddColumn<long>(
                name: "CustomRoleID",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ProductID",
                table: "Packages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CustomRoles",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClearanceLevel = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomRoles", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomRoleID",
                table: "Users",
                column: "CustomRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Products_ProductID",
                table: "Packages",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CustomRoles_CustomRoleID",
                table: "Users",
                column: "CustomRoleID",
                principalTable: "CustomRoles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Products_ProductID",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CustomRoles_CustomRoleID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CustomRoles");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomRoleID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CustomRoleID",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "ProductID",
                table: "Packages",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Products_ProductID",
                table: "Packages",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID");
        }
    }
}
