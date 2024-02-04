using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodPlannerAPI.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FoodPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeSchedulesController : ControllerBase
    {
        private readonly FoodPlannerDbContext _context;

        public RecipeSchedulesController(FoodPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeSchedule>>> GetRecipeSchedules()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipeSchedules = await _context.RecipeSchedules
                                                .Include(rs => rs.Recipe)
                                                .Where(rs => rs.UserId == userId)
                                                .OrderBy(rs => rs.Date)
                                                .ToListAsync();

            if (recipeSchedules == null || !recipeSchedules.Any())
                return NotFound("You have no recipes scheduled");

            return recipeSchedules;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeSchedule>> GetRecipeSchedule(int id)
        {
            var recipeSchedule = await _context.RecipeSchedules.FindAsync(id);

            if (recipeSchedule == null)
                return NotFound();

            return recipeSchedule;
        }

        [HttpPost]
        public async Task<ActionResult<RecipeSchedule>> PostRecipeSchedule(int recipeid, DateTime date)
        {
            var recipe = await _context.Recipes.FindAsync(recipeid);
            if (recipe == null)
                return BadRequest("Recipe not found.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipeSchedule = new RecipeSchedule
            {
                RecipeId = recipeid,
                Date = date,
                Recipe = recipe,
                UserId = userId
            };

            _context.RecipeSchedules.Add(recipeSchedule);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecipeSchedule), new { id = recipeSchedule.Id }, recipeSchedule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeSchedule(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipeSchedule = await _context.RecipeSchedules.FindAsync(id);
        
            if (recipeSchedule == null)
                return NotFound();


            if (recipeSchedule.UserId != userId)
                return Unauthorized();

            _context.RecipeSchedules.Remove(recipeSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool RecipeScheduleExists(int id)
        {
            return _context.RecipeSchedules.Any(e => e.Id == id);
        }
    }
}
