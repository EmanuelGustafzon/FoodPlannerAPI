using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Recipes",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                newName: "IX_Recipes_UserID");

            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserID",
                table: "Recipes",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserID",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Recipes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_UserID",
                table: "Recipes",
                newName: "IX_Recipes_UserId");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
