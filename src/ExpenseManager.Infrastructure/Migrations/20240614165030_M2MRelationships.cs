using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class M2MRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Transactions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "TransactionCategory",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoriesUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransactionsUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategory", x => new { x.CategoriesId, x.CategoriesUserId, x.TransactionsId, x.TransactionsUserId });
                    table.ForeignKey(
                        name: "FK_TransactionCategory_Categories_CategoriesId_CategoriesUserId",
                        columns: x => new { x.CategoriesId, x.CategoriesUserId },
                        principalTable: "Categories",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionCategory_Transactions_TransactionsId_Transaction~",
                        columns: x => new { x.TransactionsId, x.TransactionsUserId },
                        principalTable: "Transactions",
                        principalColumns: new[] { "Id", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategory_TransactionsId_TransactionsUserId",
                table: "TransactionCategory",
                columns: new[] { "TransactionsId", "TransactionsUserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionCategory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
