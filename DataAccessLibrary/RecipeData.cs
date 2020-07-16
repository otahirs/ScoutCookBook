using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
    public class RecipeData : IRecipeData
    {
        private readonly ISqlDataAccess _db;

        public RecipeData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<RecipeModel>> GetRecipes()
        {
            const string sql = "SELECT * FROM recipe;";

            return _db.LoadData<RecipeModel, dynamic>(sql, new { });
        }

        public Task<List<IngredientModel>> GetIngredients()
        {
            const string sql = "SELECT * FROM ingredient ORDER BY name ASC, id DESC;";

            return _db.LoadData<IngredientModel, dynamic>(sql, new { });
        }

        public Task<List<IngredientInRecipeModel>> GetIngredientsInRecipe(int RecipeId)
        {
            const string sql = @"SELECT ingredient.Id, recipe.Id as RecipeId, ingredient.Name, ingredient.Category, ingredient.Unit, recipe_ingredient.Amount FROM ingredient
                            INNER JOIN recipe_ingredient
                                ON recipe_ingredient.ingredient_id = ingredient.id
                            INNER JOIN recipe
                                ON recipe_ingredient.recipe_id = recipe.id
                            WHERE recipe.id = @Id";

            return _db.LoadData<IngredientInRecipeModel, dynamic>(sql, new { Id = RecipeId });
        }

        public Task<List<PersonModel>> GetPeople()
        {
            const string sql = "SELECT * FROM person;";

            return _db.LoadData<PersonModel, dynamic>(sql, new { });
        }

        public Task<int> InsertPerson(PersonModel person)
        {
            const string sql = @"INSERT INTO person (Name, Coefficient)
                           VALUES (@Name, @Coefficient);";

            return _db.SaveData<PersonModel>(sql, person);
        }

        public Task UpdatePerson(PersonModel person)
        {
            const string sql = @"UPDATE person 
                            SET 
                                Name = @Name, 
                                Coefficient = @Coefficient
                            WHERE
                                Id = @Id;";

            return _db.SaveData<PersonModel>(sql, person);
        }

        public Task DeletePerson(PersonModel person)
        {
            const string sql = @"DELETE FROM person WHERE Id = @Id;";

            return _db.SaveData<PersonModel>(sql, person);
        }


        public Task<int> InsertRecipe(RecipeModel recipe)
        {
            const string sql = @"INSERT INTO RECIPE (Name, Directions, NumberOfPortions, Type)
                           VALUES (@Name, @Directions, @NumberOfPortions, @Type);";

            return _db.SaveData<RecipeModel>(sql, recipe);
        }

        public Task UpdateRecipe(RecipeModel recipe)
        {
            const string sql = @"UPDATE recipe 
                            SET 
                                Name = @Name, 
                                Directions = @Directions, 
                                NumberOfPortions = @NumberOfPortions, 
                                Type = @Type
                            WHERE
                                Id = @Id;";

            return _db.SaveData<RecipeModel>(sql, recipe);
        }

        public Task DeleteRecipe(int recipeId)
        {
            DeleteAllIngredientsInRecipe(recipeId);
            
            const string sql = @"DELETE FROM recipe WHERE Id = @Id";
            
            return _db.SaveData(sql, new { Id = recipeId });
        }

        

        public Task<int> InsertIngredient(IngredientModel ingredient)
        {
            const string sql = @"INSERT INTO ingredient (Name, Category, Unit)
                           VALUES (@Name, @Category, @Unit);";

            return _db.SaveData<IngredientModel>(sql, ingredient);
        }

        public Task DeleteIngredient(IngredientModel ingredient)
        {
            const string sql = @"DELETE FROM ingredient WHERE Id = @Id";

            return _db.SaveData(sql, new { Id = ingredient.Id });
        }

        public Task UpdateIngredient(IngredientModel ingredient)
        {
            const string sql = @"UPDATE ingredient
                            SET name = @Name,
                                category = @Category,
                                unit = @Unit
                            WHERE
                                Id = @Id";

            return _db.SaveData(sql, ingredient);
        }

        public Task InsertIngredientInRecipe(IngredientInRecipeModel ingredient)
        {
            const string sql = @"INSERT INTO recipe_ingredient (recipe_id, ingredient_id, amount)
                           VALUES (@RecipeId, @IngredientId, @Amount);";

            return _db.SaveData<dynamic>(sql, new {RecipeId = ingredient.RecipeId, IngredientId = ingredient.Id, ingredient.Amount});
        }
        public Task DeleteAllIngredientsInRecipe(int recipeId)
        {
            const string sql = @"DELETE FROM recipe_ingredient WHERE recipe_id = @RecipeId";

            return _db.SaveData(sql, new { RecipeId = recipeId });
        }

       
        

    }
}