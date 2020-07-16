using System.ComponentModel.DataAnnotations;
using SharedLibrary.Enums;
using System;
using System.Collections.Generic;
using DataAccessLibrary.Models;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary;

namespace ScoutCookBook.Models
{
    public class DisplayRecipeModel
    {

        private IRecipeData _db = new RecipeData(new SqlDataAccess());
        public int Id { get; set; }

        [Required]
        //[StringLength(15, ErrorMessage = "First Name is too long.")]
        //[MinLength(5, ErrorMessage = "First Name is too short.")]
        public string Name { get; set; }

        [Required]
        //[StringLength(15, ErrorMessage = "Last Name is too long.")]
        //[MinLength(5, ErrorMessage = "Last Name is too short.")]
        private string _recipe;
        public string Recipe {  get => _recipe;
                                set
                                {
                                    _recipe = value; 
                                    Rows = new [] { 4, value.Split('\n').Length + 1, value.Split('\r').Length + 1 }.Max(); 
                                }
                            }
        public int Rows { get; set; } = 4;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int NumberOfPortions { get; set; }
        [Required]
        public DishType Type { get; set; } = Enum.Parse<DishType>("Main");

        public List<DisplayIngredientModel> Ingredients { get; set; } = new List<DisplayIngredientModel>();

        public RecipeModel GetRecipeModel()
        {
            return new RecipeModel
            {
                Name = this.Name,
                Recipe = this.Recipe,
                NumberOfPortions = this.NumberOfPortions,
                Type = Enum.GetName(typeof(DishType), this.Type)
            };
        }

        public async Task LoadAsync(int Id) 
        {
            var recipesFromDb = (await _db.GetRecipes()).FirstOrDefault(r => r.Id == Id);
                if(recipesFromDb == null) return;

                this.Id = Id;
                Name = recipesFromDb.Name;
                Type = Enum.Parse<DishType>(recipesFromDb.Type);
                NumberOfPortions = recipesFromDb.NumberOfPortions;
                Recipe = recipesFromDb.Recipe;
                
                Ingredients.Clear();
                foreach(var i in await _db.GetIngredientsInRecipe(Id))
                {
                    var ingredient = new DisplayIngredientModel 
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Category = Enum.Parse<IngredientCategory>(i.Category),
                        Unit = Enum.Parse<IngredientUnit>(i.Unit),
                        Amount = i.Amount
                    };
                    Ingredients.Add(ingredient);
                };   
        }
    }
}