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
using System.Net;

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
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes([FromQuery] int? maxCookTime = null)
        {
            if (maxCookTime != null)
            {
                return await _context.Recipes
                    .Where(r => r.CookTime <= maxCookTime)
                    .ToListAsync();
            }
            var recipes = await _context.Recipes.ToListAsync();
            return recipes;
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
