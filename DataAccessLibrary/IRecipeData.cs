using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IRecipeData
    {
        Task<List<RecipeModel>> GetRecipes();
        Task<int> InsertRecipe(RecipeModel Recipe);

        Task<List<IngredientModel>> GetIngredients();
        Task InsertIngredient(IngredientModel Ingredient);
        Task DeleteIngredient(IngredientModel Ingredient);
        Task UpdateIngredient(IngredientModel Ingredient);

        Task<List<IngredientInRecipeModel>> GetIngredientsInRecipe(int RecipeId);
    }
}