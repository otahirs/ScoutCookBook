namespace DataAccessLibrary.Models
{
    public class RecipeModel
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Recipe { get; set; }
        public int NumberOfPortions { get; set; }
        public string Type { get; set; }
    }
}