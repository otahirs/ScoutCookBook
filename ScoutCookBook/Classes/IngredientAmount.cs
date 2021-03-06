using System.Collections.Generic;

namespace ScoutCookBook.Classes
{
    public class IngredientAmount
    {
        public Ingredient Ingredient { get; set; }
        public double TotalAmount { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}