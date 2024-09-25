using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace coin_api.Migrations
{
    /// <inheritdoc />
    public partial class TransactionWithCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "transaction");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "transaction",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "transaction",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "idTransaction",
                table: "transaction",
                newName: "IdTransaction");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: true),
                    TransactionModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.IdCategory);
                    table.ForeignKey(
                        name: "FK_category_transaction_TransactionModelId",
                        column: x => x.TransactionModelId,
                        principalTable: "transaction",
                        principalColumn: "IdTransaction",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_TransactionModelId",
                table: "category",
                column: "TransactionModelId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "transaction",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "transaction",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "IdTransaction",
                table: "transaction",
                newName: "idTransaction");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "transaction",
                type: "text",
                nullable: true);
        }
    }
}
