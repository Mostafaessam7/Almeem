using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class updateConfigurationForSomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductSizes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductColors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_Size",
                table: "ProductSizes",
                column: "Size",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_Name",
                table: "ProductColors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_Size",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_Name",
                table: "ProductColors");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductSizes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductColors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizesColors_ProductColorId",
                table: "ProductSizesColors",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizesColors_ProductSizeId",
                table: "ProductSizesColors",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizesColors_ProductColors_ProductColorId",
                table: "ProductSizesColors",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizesColors_ProductSizes_ProductSizeId",
                table: "ProductSizesColors",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
