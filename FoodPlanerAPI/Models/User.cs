using Microsoft.AspNetCore.Identity;

namespace FoodPlannerAPI.Models
{
    public class User: IdentityUser
    {
        public List<Recipe>? CreatedRecipes { get; set; }
    }
}
