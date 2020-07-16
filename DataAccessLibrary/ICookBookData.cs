using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public interface ICookBookData
    {
        Task<List<RecipeModel>> GetRecipes();
        Task<int> InsertRecipe(RecipeModel recipe);
        Task UpdateRecipe(RecipeModel recipe);
        Task DeleteRecipe(int recipeId);

        Task<List<IngredientModel>> GetIngredients();
        Task<int> InsertIngredient(IngredientModel ingredient);
        Task DeleteIngredient(IngredientModel ingredient);
        Task UpdateIngredient(IngredientModel ingredient);

        Task<List<IngredientInRecipeModel>> GetIngredientsInRecipe(int recipeId);
        Task InsertIngredientInRecipe(IngredientInRecipeModel ingredient);
        Task DeleteAllIngredientsInRecipe(int recipeId);

        Task<int> InsertPerson(PersonModel person);
        Task UpdatePerson(PersonModel person);
        Task<List<PersonModel>> GetPeople();
        Task DeletePerson(PersonModel person);
    }
}