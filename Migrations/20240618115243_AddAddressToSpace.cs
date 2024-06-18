using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace commonspace.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToSpace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Spaces",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Spaces");
        }
    }
}
