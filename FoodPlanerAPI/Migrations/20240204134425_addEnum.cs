using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class addEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ShoppingItems",
                newName: "Item");

            migrationBuilder.RenameColumn(
                name: "steps",
                table: "Recipes",
                newName: "Steps");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Recipes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Recipes",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "Region",
                table: "Recipes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Item",
                table: "ShoppingItems",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Steps",
                table: "Recipes",
                newName: "steps");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Recipes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Recipes",
                newName: "description");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
