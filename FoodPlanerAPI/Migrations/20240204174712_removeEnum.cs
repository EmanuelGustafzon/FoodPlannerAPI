using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealType",
                table: "Recipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MealType",
                table: "Recipes",
                type: "nvarchar(9)",
                nullable: true);
        }
    }
}
