using System;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessLibrary.Models
{
    public class IngredientModel : IComparable<IngredientModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }

        public int CompareTo([AllowNull] IngredientModel other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }

}