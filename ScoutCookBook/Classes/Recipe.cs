using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using SharedLibrary.Enums;

namespace ScoutCookBook.Classes
{
    public class Recipe
    {

        private IRecipeData _db = new RecipeData(new SqlDataAccess());
        public int Id { get; set; }

        [Required]
        //[StringLength(15, ErrorMessage = "First Name is too long.")]
        //[MinLength(5, ErrorMessage = "First Name is too short.")]
        public string Name { get; set; }

        
        private string _directions ;
        [Required]
        public string Directions  
        {   get => _directions;
            set
            {
                _directions = value; 
                DirectionsRowCount = new [] { 4, value.Split('\n').Length + 1, value.Split('\r').Length + 1 }.Max(); 
            }
        }
        public int DirectionsRowCount { get; set; } = 4;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int NumberOfPortions { get; set; }
        [Required]
        public DishType Type { get; set; } = Enum.Parse<DishType>("Main");

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public RecipeModel GetRecipeModel()
        {
            return new RecipeModel
            {
                Name = this.Name,
                Directions = this.Directions,
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
            Directions = recipesFromDb.Directions;
            
            Ingredients.Clear();
            foreach(var i in await _db.GetIngredientsInRecipe(Id))
            {
                var ingredient = new Ingredient 
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