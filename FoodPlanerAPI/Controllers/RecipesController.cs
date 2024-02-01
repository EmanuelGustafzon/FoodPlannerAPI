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
using NuGet.Packaging.Signing;

namespace FoodPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecipesController : ControllerBase
    {
        private readonly FoodPlannerDbContext _context;

        public RecipesController(FoodPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                ModelState.AddModelError("Id", "The ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipeFromDb = await _context.Recipes.FindAsync(id);

            if (recipeFromDb == null)
            {
                return NotFound("Recipe not found.");
            }

            if (recipeFromDb.UserID != userId)
            {
                return Unauthorized("You are not authorized to modify this recipe.");
            }

            _context.Entry(recipeFromDb).CurrentValues.SetValues(recipe);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound("Recipe not found.");
                }
                else
                {
                    throw;
                }
            }
        }


        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            recipe.UserID = userId;

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

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

            if(user == null)
                return NotFound("USer not Found");

            if(recipe == null)
                return NotFound("Recipe not Found");

            if(user.FavoriteRecipes == null)
                user.FavoriteRecipes = new List<Recipe>();

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

            if (user == null)
                return NotFound("User not found.");

            if (user.FavoriteRecipes == null)
                return NotFound("avorite recipe not found.");

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

        [HttpGet("get-recipes-by-user")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipes = await _context.Recipes
                .Where(r => r.UserID == userId)
                .ToListAsync();

            return recipes;
        }
        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
