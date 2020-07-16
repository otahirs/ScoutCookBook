namespace DataAccessLibrary.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Directions { get; set; }
        public int NumberOfPortions { get; set; }
        public string Type { get; set; }
    }
}