using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class tryConvertEnumTOString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MealType",
                table: "Recipes",
                type: "nvarchar(9)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MealType",
                table: "Recipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldNullable: true);
        }
    }
}
