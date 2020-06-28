using System.ComponentModel.DataAnnotations;
using SharedLibrary.Enums;
using System;
using System.Collections.Generic;

namespace ScoutCookBook.Models
{
    public class DisplayRecipeModel
    {
        public string Id { get; set; }
        [Required]
        //[StringLength(15, ErrorMessage = "First Name is too long.")]
        //[MinLength(5, ErrorMessage = "First Name is too short.")]
        public string Name { get; set; }

        [Required]
        //[StringLength(15, ErrorMessage = "Last Name is too long.")]
        //[MinLength(5, ErrorMessage = "Last Name is too short.")]
        public string Recipe { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public string NumberOfPortions { get; set; }
        [Required]
        public DishType Type { get; set; } = Enum.Parse<DishType>("Main");

        public List<DisplayIngredientModel> Ingredients { get; set; } = new List<DisplayIngredientModel>();
    }
}