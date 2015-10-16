namespace Domain.Entities
{
  public class Ingredient
  {
    public int IngredientID { get; set; }
    public int CategoryID { get; set; }
    public int RecipeID { get; set; }
    public int ImportanceLevel { get; set; }
    public int ReplaceabilityLevel { get; set; }
    public int Amount { get; set; }
  }
}
