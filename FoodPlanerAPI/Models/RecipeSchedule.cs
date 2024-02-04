
namespace FoodPlannerAPI.Models
{
    public class RecipeSchedule
    {
        public int Id { get; set; }
        public string?  UserId { get; set; }

        public int? RecipeId { get; set; }

        public Recipe? Recipe { get; set; }
        public DateTime? Date { get; set; }

    }
}
