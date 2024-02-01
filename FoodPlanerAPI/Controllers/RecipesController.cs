using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodPlannerAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FoodPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly FoodPlannerDbContext _context;
        private readonly UserManager<User> _userManager;

        public RecipesController(FoodPlannerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: api/Recipes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(recipe.UserID != userId)
                return Unauthorized();

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            recipe.UserID = userId;
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
                return NotFound();

            if (recipe.UserID != userId)
                return Unauthorized();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("add-to-favorite/{recipeId}")]
        public async Task<IActionResult> AddRecipeToFavorite (int recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId);
            var recipe = await _context.Recipes.FindAsync(recipeId);

            if(recipe == null || user == null)
            {
                return NotFound();
            }

            if(user.FavoriteRecipes == null)
            {
                user.FavoriteRecipes = new List<Recipe>();
            }

            user.FavoriteRecipes.Add(recipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("get-favorite-recipes")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetFavoriteRecipes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.Users
                .Include(u => u.FavoriteRecipes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.FavoriteRecipes == null)
                return NotFound();

            return user.FavoriteRecipes.ToList();
        }

        [HttpDelete("remove-favorite-recipe/{id}")]
        public async Task<IActionResult> RemoveFavoriteRecipe(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _context.Users
                .Include(u => u.FavoriteRecipes)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("User not found.");

            if (user.FavoriteRecipes == null)
                return NotFound("avorite recipe not found.");

            var recipeToRemove = user.FavoriteRecipes.FirstOrDefault(r => r.Id == id);

            if (recipeToRemove == null)
                return NotFound("Favorite recipe not found.");

            user.FavoriteRecipes.Remove(recipeToRemove);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
