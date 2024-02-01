﻿namespace FoodPlannerAPI.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

        public string? category { get; set; }
        public string? Ingredients { get; set; }
        public string? IngredientsAmount { get; set; }
        public string? steps { get; set; }
        public string? image { get; set; }
        public string? UserID { get; set; }

    }
}
