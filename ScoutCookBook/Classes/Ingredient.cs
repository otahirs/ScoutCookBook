using System;
using System.ComponentModel.DataAnnotations;
using DataAccessLibrary.Models;
using SharedLibrary.Enums;

namespace ScoutCookBook.Classes
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IngredientCategory Category { get; set; } = Enum.Parse<IngredientCategory>("Other");
        [Required]
        public IngredientUnit Unit { get; set; } = Enum.Parse<IngredientUnit>("Gram");

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Amount { get; set; }

        public IngredientModel GetIngredientModel()
        {
            return new IngredientModel 
            {
                Id = this.Id,
                Name = this.Name,
                Category = Enum.GetName(typeof(IngredientCategory), this.Category),
                Unit = Enum.GetName(typeof(IngredientUnit), this.Unit)
            };
        }

        public static string UnitAmountFormated(double Amount, IngredientUnit Unit) 
        {
            switch (Unit)
            {
                case IngredientUnit.Gram:
                    if (Amount < 1000)
                    {
                        return $"{Math.Round(Amount, 1)} g";
                    }
                    else 
                    {
                        return $"{Math.Round(Amount / 1000, 1)} kg";
                    }
                case IngredientUnit.Mililiter:
                    if (Amount < 1000)
                    {
                        return $"{Math.Round(Amount, 1)} ml";
                    }
                    else 
                    {
                        return $"{Math.Round(Amount / 1000, 1)} l";
                    }
                case IngredientUnit.Piece:
                    return $"{Math.Round(Amount, 1)} pcs";
                default:
                    return "unknown unit";
            }
        }

        //public string UnitAmountFormated()
        //{
        //    return UnitAmountFormated((double)Amount, Unit); 
        //}
    }
}