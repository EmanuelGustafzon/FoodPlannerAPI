
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodPlannerAPI.Models
{
    public class FoodPlannerDbContext : IdentityDbContext<User>
    {
        public FoodPlannerDbContext(DbContextOptions<FoodPlannerDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
