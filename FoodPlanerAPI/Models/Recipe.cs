using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodPlannerAPI.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int? CookTime { get; set; }
        public List<IngredientAndAmount>? Ingredients { get; set; }
        public List<string>? Steps { get; set; }
        public string? UserID { get; set; }
    }
    [Owned]
    public class IngredientAndAmount
    {
        public string? Ingredient { get; set; }
        public string? Amount { get; set; }
    }
}
