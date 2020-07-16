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
            string sql = "SELECT * FROM recipe;";

            return _db.LoadData<RecipeModel, dynamic>(sql, new { });
        }

        public Task<List<IngredientModel>> GetIngredients()
        {
            string sql = "SELECT * FROM ingredient ORDER BY name ASC, id DESC;";

            return _db.LoadData<IngredientModel, dynamic>(sql, new { });
        }

        public Task<List<IngredientInRecipeModel>> GetIngredientsInRecipe(int RecipeId)
        {
            string sql = @"SELECT ingredient.Id, recipe.Id as RecipeId, ingredient.Name, ingredient.Category, ingredient.Unit, recipe_ingredient.Amount FROM ingredient
                            INNER JOIN recipe_ingredient
                                ON recipe_ingredient.ingredient_id = ingredient.id
                            INNER JOIN recipe
                                ON recipe_ingredient.recipe_id = recipe.id
                            WHERE recipe.id = @Id";

            return _db.LoadData<IngredientInRecipeModel, dynamic>(sql, new { Id = RecipeId });
        }

        public Task<List<PersonModel>> GetPeople()
        {
            string sql = "SELECT * FROM person;";

            return _db.LoadData<PersonModel, dynamic>(sql, new { });
        }

        public Task<int> InsertPerson(PersonModel Person)
        {
            string sql = @"INSERT INTO person (Name, Coefficient)
                           VALUES (@Name, @Coefficient);";

            return _db.SaveData<PersonModel>(sql, Person);
        }

        public Task UpdatePerson(PersonModel Person)
        {
            string sql = @"UPDATE person 
                            SET 
                                Name = @Name, 
                                Coefficient = @Coefficient
                            WHERE
                                Id = @Id;";

            return _db.SaveData<PersonModel>(sql, Person);
        }

        public Task DeletePerson(PersonModel Person)
        {
            string sql = @"DELETE FROM person WHERE Id = @Id;";

            return _db.SaveData<PersonModel>(sql, Person);
        }


        public Task<int> InsertRecipe(RecipeModel Recipe)
        {
            string sql = @"INSERT INTO RECIPE (Name, Recipe, NumberOfPortions, Type)
                           VALUES (@Name, @Recipe, @NumberOfPortions, @Type);";

            return _db.SaveData<RecipeModel>(sql, Recipe);
        }

        public Task UpdateRecipe(RecipeModel Recipe)
        {
            string sql = @"UPDATE recipe 
                            SET 
                                Name = @Name, 
                                Recipe = @Recipe, 
                                NumberOfPortions = @NumberOfPortions, 
                                Type = @Type
                            WHERE
                                Id = @Id;";

            return _db.SaveData<RecipeModel>(sql, Recipe);
        }

        public Task DeleteRecipe(int RecipeId)
        {
            DeleteAllIngredientsInRecipe(RecipeId);
            
            string sql = @"DELETE FROM recipe WHERE Id = @Id";
            
            return _db.SaveData(sql, new { Id = RecipeId });
        }

        

        public Task<int> InsertIngredient(IngredientModel Ingredient)
        {
            string sql = @"INSERT INTO ingredient (Name, Category, Unit)
                           VALUES (@Name, @Category, @Unit);";
            
            return _db.SaveData<IngredientModel>(sql, Ingredient);
        }

        public Task DeleteIngredient(IngredientModel Ingredient)
        {
            string sql = @"DELETE FROM ingredient WHERE Id = @Id";
            
            return _db.SaveData(sql, new { Id = Ingredient.Id });
        }

        public Task UpdateIngredient(IngredientModel Ingredient)
        {
            string sql = @"UPDATE ingredient
                            SET name = @Name,
                                category = @Category,
                                unit = @Unit
                            WHERE
                                Id = @Id";
            
            return _db.SaveData(sql, Ingredient);
        }

        public Task InsertIngredientInRecipe(IngredientInRecipeModel Ingredient)
        {
            string sql = @"INSERT INTO recipe_ingredient (recipe_id, ingredient_id, amount)
                           VALUES (@RecipeId, @IngredientId, @Amount);";
            
            return _db.SaveData<dynamic>(sql, new {RecipeId = Ingredient.RecipeId, IngredientId = Ingredient.Id, Ingredient.Amount});
        }
        public Task DeleteAllIngredientsInRecipe(int RecipeId)
        {
            string sql = @"DELETE FROM recipe_ingredient WHERE recipe_id = @RecipeId";
            
            return _db.SaveData(sql, new { RecipeId = RecipeId });
        }

       
        

    }
}