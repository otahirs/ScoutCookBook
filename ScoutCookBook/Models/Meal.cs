using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccessLibrary.Models;
using SharedLibrary.Enums;
using System.Linq;

namespace ScoutCookBook.Models
{
    public class Meal
    {
        public DisplayRecipeModel Recipe { get; set; } = new DisplayRecipeModel();
        public List<Portion> Portions { get; set; } = new List<Portion>();

        public List<IngredientAmount> IngredientsAmount 
        { 
            get 
            {
                var ia = new List<IngredientAmount>();
                if (Recipe == null) return ia;
                foreach(var ingredient in Recipe.Ingredients)
                {
                    double totalAmount = 0;
                    double normalizedAmount = (double)ingredient.Amount / Recipe.NumberOfPortions;
                    foreach(var portion in Portions)
                    {
                        totalAmount += portion.Person.Coefficient * portion.Count * normalizedAmount;
                    }
                    ia.Add(new IngredientAmount()
                    {   
                        Ingredient = ingredient,  
                        TotalAmount = totalAmount,
                        Recipes = new List<DisplayRecipeModel>() { Recipe }
                    });
                }
               return ia;
            }
        }

        public override string ToString() 
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine(Recipe.Name);
            sb.AppendLine();
            sb.AppendLine("Ingredients:");
            foreach (var ia in IngredientsAmount)
            {
                sb.AppendLine($"  {ia.Ingredient.Name}: {ia.Ingredient.UnitAmountFormated(ia.TotalAmount, ia.Ingredient.Unit) }");
            }
            sb.AppendLine();
            sb.AppendLine("Recipe:");
            sb.AppendLine(Recipe.Recipe);

            return sb.ToString();
        }
    }
}

