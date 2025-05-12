using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSequenceNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderSequenceNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderSequenceNumber",
                table: "Orders");
        }
    }
}
