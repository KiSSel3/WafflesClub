using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waffles_Club.DataManagment.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Waffle_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Waffles",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Waffles");
        }
    }
}
