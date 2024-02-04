using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddOneTwoManyFieldRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeSchedules_Recipes_recipeId",
                table: "RecipeSchedules");

            migrationBuilder.RenameColumn(
                name: "recipeId",
                table: "RecipeSchedules",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "RecipeSchedules",
                newName: "Date");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeSchedules_recipeId",
                table: "RecipeSchedules",
                newName: "IX_RecipeSchedules_RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeSchedules_Recipes_RecipeId",
                table: "RecipeSchedules",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeSchedules_Recipes_RecipeId",
                table: "RecipeSchedules");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeSchedules",
                newName: "recipeId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "RecipeSchedules",
                newName: "date");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeSchedules_RecipeId",
                table: "RecipeSchedules",
                newName: "IX_RecipeSchedules_recipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeSchedules_Recipes_recipeId",
                table: "RecipeSchedules",
                column: "recipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }
    }
}
