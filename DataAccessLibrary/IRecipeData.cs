using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface IRecipeData
    {
        Task<List<RecipeModel>> GetRecipes();
        Task<int> InsertRecipe(RecipeModel Recipe);
        Task UpdateRecipe(RecipeModel Recipe);
        Task DeleteRecipe(int RecipeId);

        Task<List<IngredientModel>> GetIngredients();
        Task<int> InsertIngredient(IngredientModel Ingredient);
        Task DeleteIngredient(IngredientModel Ingredient);
        Task UpdateIngredient(IngredientModel Ingredient);

        Task<List<IngredientInRecipeModel>> GetIngredientsInRecipe(int RecipeId);
        Task InsertIngredientInRecipe(IngredientInRecipeModel Ingredient);
        Task DeleteAllIngredientsInRecipe(int RecipeId);

        Task<int> InsertPerson(PersonModel Person);
        Task UpdatePerson(PersonModel Person);
        Task<List<PersonModel>> GetPeople();
        Task DeletePerson(PersonModel Person);
    }
}