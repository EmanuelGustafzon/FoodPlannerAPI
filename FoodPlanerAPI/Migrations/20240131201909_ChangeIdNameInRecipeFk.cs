using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdNameInRecipeFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Userid",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
