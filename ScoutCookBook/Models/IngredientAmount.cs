using System.Collections.Generic;

namespace ScoutCookBook.Models
{
    public class IngredientAmount
    {
        public DisplayIngredientModel Ingredient { get; set; }
        public double TotalAmount { get; set; }
        public List<DisplayRecipeModel> Recipes { get; set; }
    }
}