using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class RecipeScheduleTypoChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeSchedules_AspNetUsers_userId",
                table: "RecipeSchedules");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "RecipeSchedules",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeSchedules_userId",
                table: "RecipeSchedules",
                newName: "IX_RecipeSchedules_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeSchedules_AspNetUsers_UserId",
                table: "RecipeSchedules",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeSchedules_AspNetUsers_UserId",
                table: "RecipeSchedules");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RecipeSchedules",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeSchedules_UserId",
                table: "RecipeSchedules",
                newName: "IX_RecipeSchedules_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeSchedules_AspNetUsers_userId",
                table: "RecipeSchedules",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
