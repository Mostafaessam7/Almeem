using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class updateEntitiesForMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "NameInArabic");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "DescriptionInEnglish");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionInArabic",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameInEnglish",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizesColors_ProductColorId",
                table: "ProductSizesColors",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizesColors_ProductSizeId",
                table: "ProductSizesColors",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_NameInEnglish",
                table: "Products",
                column: "NameInEnglish");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizesColors_ProductColors_ProductColorId",
                table: "ProductSizesColors",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizesColors_ProductSizes_ProductSizeId",
                table: "ProductSizesColors",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizesColors_ProductColors_ProductColorId",
                table: "ProductSizesColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizesColors_ProductSizes_ProductSizeId",
                table: "ProductSizesColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizesColors_ProductColorId",
                table: "ProductSizesColors");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizesColors_ProductSizeId",
                table: "ProductSizesColors");

            migrationBuilder.DropIndex(
                name: "IX_Products_NameInEnglish",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DescriptionInArabic",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NameInEnglish",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "NameInArabic",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DescriptionInEnglish",
                table: "Products",
                newName: "Description");
        }
    }
}
