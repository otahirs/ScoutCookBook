using System.ComponentModel.DataAnnotations;
using SharedLibrary.Enums;
using System;

namespace ScoutCookBook.Models
{
    public class DisplayIngredientModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IngredientCategory Category { get; set; } = Enum.Parse<IngredientCategory>("Other");
        [Required]
        public IngredientUnit Unit { get; set; } = Enum.Parse<IngredientUnit>("Piece");

        public string Amount { get; set; }

    }
}