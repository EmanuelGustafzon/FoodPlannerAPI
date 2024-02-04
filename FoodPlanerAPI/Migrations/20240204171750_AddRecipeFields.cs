using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CookTime",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MealType",
                table: "Recipes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookTime",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "MealType",
                table: "Recipes");
        }
    }
}
