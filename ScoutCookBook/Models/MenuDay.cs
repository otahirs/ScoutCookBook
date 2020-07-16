using System;
using System.Collections.Generic;
using System.Linq;

namespace ScoutCookBook.Models
{
    public class MenuDay
    {
        public DateTimeOffset Date { get; set; }
        public List<Meal> BreakfastMeals { get; set; } 
        public List<Meal> LunchMeals { get; set; }
        public List<Meal> DinnerMeals { get; set; }

        public List<IngredientAmount> IngredientsAmount 
        { 
            get 
            {
                var meals = Enumerable.Empty<Meal>();
                meals = meals.Concat(BreakfastMeals ?? Enumerable.Empty<Meal>());
                meals = meals.Concat(LunchMeals ?? Enumerable.Empty<Meal>());
                meals = meals.Concat(DinnerMeals ?? Enumerable.Empty<Meal>());

                if(!meals.Any()) return new List<IngredientAmount>();

                return meals
                    .SelectMany(m => m.IngredientsAmount)
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

            var slots = new string[] {"Breakfast", "Lunch", "Dinner"};
            foreach(var slot in slots) 
            {
                var meals = (List<Meal>)this.GetType().GetProperty(slot + "Meals").GetValue(this, null); // ugly as F 
                if(meals != null)
                {
                    sb.Append("  ");
                    sb.Append(slot.PadRight(mealPadding));
                    var first = meals.FirstOrDefault();
                    foreach (var meal in meals)
                    {
                        if (meal != first) sb.Append(new string(' ', mealPadding + 2));
                        sb.AppendLine(meal.Recipe.Name);
                    }
                }
            }
            return sb.ToString();
        }
    }
}