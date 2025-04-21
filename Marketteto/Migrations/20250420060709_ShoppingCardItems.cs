using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketteto.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCardItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shoppingCardItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ShoppingCardId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCardItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shoppingCardItems_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCardItems_productId",
                table: "shoppingCardItems",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoppingCardItems");
        }
    }
}
