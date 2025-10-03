using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterAlleviationFoundation.Migrations
{
    /// <inheritdoc />
    public partial class AddDonorNameToDonations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DonorName",
                table: "Donations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonorName",
                table: "Donations");
        }
    }
}
