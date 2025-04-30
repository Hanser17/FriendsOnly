using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastrucutre_Persistance_Presentation.Migrations
{
    /// <inheritdoc />
    public partial class BenifisiaryModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Beneficiaries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Beneficiaries");
        }
    }
}
