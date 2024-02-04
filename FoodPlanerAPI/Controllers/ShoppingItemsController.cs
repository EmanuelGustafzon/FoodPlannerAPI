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
using System.Runtime.CompilerServices;

namespace FoodPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly FoodPlannerDbContext _context;

        public ShoppingItemsController(FoodPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetShoppingItems()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.ShoppingItems.Where(si => si.UserID == userId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingItem>> GetShoppingItem(int id)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(id);

            if (shoppingItem == null)
                return NotFound();

            return shoppingItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingItem(int id, ShoppingItem shoppingItem)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shoppingItemFromDb = await _context.ShoppingItems.FindAsync(id);

            if(shoppingItemFromDb == null)
                return NotFound();

            if (shoppingItemFromDb.UserID != userId)
                return Unauthorized();

            if (id != shoppingItem.Id)
                return BadRequest();

            _context.Entry(shoppingItemFromDb).CurrentValues.SetValues(shoppingItem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingItemExists(id))
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

        [HttpPost]
        public async Task<ActionResult<ShoppingItem>> PostShoppingItem(string item, string quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shoppingItem = new ShoppingItem
            {
                UserID = userId,
                Item = item,
                Quantity = quantity
            };
            _context.ShoppingItems.Add(shoppingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShoppingItem", new { id = shoppingItem.Id }, shoppingItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingItem(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var shoppingItem = await _context.ShoppingItems.FindAsync(id);

            if(shoppingItem == null)
                return NotFound();

            if(shoppingItem.UserID != userId)
                return Unauthorized();

            _context.ShoppingItems.Remove(shoppingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("add-ingredients-from-recipe-to-shoppinglist")]
        public async Task<IActionResult> AddIngredientsFromRecipe(int recipeId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe == null || recipe.Ingredients == null)
                return BadRequest("Recipe not found.");

            var ingredients = recipe.Ingredients.ToList();
            if(ingredients.Count == 0)
                return BadRequest("No ingredients found.");

            foreach (var ingredient in ingredients)
            {
                if(ingredient.Ingredient != null && ingredient.Amount != null)
                    await PostShoppingItem(ingredient.Ingredient, ingredient.Amount);

            }
            return Ok();
        }

        private bool ShoppingItemExists(int id)
        {
            return _context.ShoppingItems.Any(e => e.Id == id);
        }
    }
}
