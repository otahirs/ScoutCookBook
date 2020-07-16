using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoutCookBook.Models
{
    public class MenuDay
    {
        public DateTimeOffset Date { get; set; }
        public List<KeyValuePair<string, List<Meal>>> Slots { get; set; } = new List<KeyValuePair<string, List<Meal>>>();
        public List<IngredientAmount> IngredientsAmount 
        { 
            get 
            {
                if(!Slots.Any()) return new List<IngredientAmount>();

                return Slots
                    .SelectMany(slot => slot.Value)
                    .SelectMany(meal => meal.IngredientsAmount)
                    .ToLookup(ia => ia.Ingredient.Id)
                    .Select(ia => ia.Aggregate((ia1, ia2) => new IngredientAmount
                    {
                        Ingredient = ia1.Ingredient,
                        TotalAmount = ia1.TotalAmount + ia2.TotalAmount,
                        Recipes = ia1.Recipes.Concat(ia2.Recipes).ToList()
                    }))
                    .ToList();
            }
        }

        public override string ToString()
        {
            var mealPadding = 11;
            var sb = new System.Text.StringBuilder();
            sb.Append(Date.DayOfWeek.ToString());
            sb.Append(" ");
            sb.AppendLine(Date.ToString("d. MMMM"));
            sb.AppendLine();

            foreach(var (slotName, meals) in Slots) 
            {
                sb.Append("  ");
                sb.Append(slotName.PadRight(mealPadding));
                var first = meals.FirstOrDefault();
                foreach (var meal in meals)
                {
                    if (meal != first) sb.Append(new string(' ', mealPadding + 2));
                    sb.AppendLine(meal.Recipe.Name);
                }
            }
            return sb.ToString();
        }
    }
}