using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace coin_api.Migrations
{
    /// <inheritdoc />
    public partial class relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_transaction_TransactionModelId",
                table: "category");

            migrationBuilder.DropIndex(
                name: "IX_category_TransactionModelId",
                table: "category");

            migrationBuilder.RenameColumn(
                name: "TransactionModelId",
                table: "category",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "transaction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "transaction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_CategoryId",
                table: "transaction",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_UserId",
                table: "transaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_category_UserId",
                table: "category",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_category_users_UserId",
                table: "category",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_category_CategoryId",
                table: "transaction",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "IdCategory",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transaction_users_UserId",
                table: "transaction",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_users_UserId",
                table: "category");

            migrationBuilder.DropForeignKey(
                name: "FK_transaction_category_CategoryId",
                table: "transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_transaction_users_UserId",
                table: "transaction");

            migrationBuilder.DropIndex(
                name: "IX_transaction_CategoryId",
                table: "transaction");

            migrationBuilder.DropIndex(
                name: "IX_transaction_UserId",
                table: "transaction");

            migrationBuilder.DropIndex(
                name: "IX_category_UserId",
                table: "category");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "transaction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "transaction");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "category",
                newName: "TransactionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_category_TransactionModelId",
                table: "category",
                column: "TransactionModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_category_transaction_TransactionModelId",
                table: "category",
                column: "TransactionModelId",
                principalTable: "transaction",
                principalColumn: "IdTransaction",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
