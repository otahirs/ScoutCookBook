using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ScoutCookBook.Models
{
    public class MenuFormModel
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        [Required]
        public string StartMeal { get; set; }
        [Required]
        public string EndMeal { get; set; }

        public List<Portion> Portions { get; set; } = new List<Portion>();
    }
}