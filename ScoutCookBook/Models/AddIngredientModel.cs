using System.ComponentModel.DataAnnotations;
using SharedLibrary.Enums;
using System;
using DataAccessLibrary;
using System.Threading.Tasks;
using System.Linq;

namespace ScoutCookBook.Models
{
    public class AddIngredientModel
    {
        private IRecipeData _db = new RecipeData(new SqlDataAccess());
        [Required]
        public string Id { get; set ; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public string Amount { get; set; }

        public string GetUnit() {
            var ingredients = Task.Run( async () => await _db.GetIngredients()).Result;
            return ingredients.FirstOrDefault(i => i.Id.ToString() == Id)?.Unit ?? "";
        }

    }
}