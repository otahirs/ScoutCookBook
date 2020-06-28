namespace DataAccessLibrary.Models
{
    public class IngredientInRecipeModel : IngredientModel
    {
        public int RecipeId { get; set; }
        public int Amount { get; set; }
    }
}