
namespace Domain.Entities
{
  public class Product
  {
    public int ProductID { get; set; }
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public int AmountDefault { get; set; }
    public int UnitMeasureID { get; set; }
  }
}
