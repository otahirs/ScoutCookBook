using System.ComponentModel.DataAnnotations;
using DataAccessLibrary.Models;

namespace ScoutCookBook.Models
{
    public class Portion
    {
        public PersonModel Person { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Count { get; set; }
        
    }
}