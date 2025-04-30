using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastrucutre_Persistance_Presentation.Migrations
{
    /// <inheritdoc />
    public partial class RelationsWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Transfer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CashAdvance",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_UserId",
                table: "Transfer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CashAdvance_UserId",
                table: "CashAdvance",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transfer_UserId",
                table: "Transfer");

            migrationBuilder.DropIndex(
                name: "IX_CashAdvance_UserId",
                table: "CashAdvance");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CashAdvance");
        }
    }
}
