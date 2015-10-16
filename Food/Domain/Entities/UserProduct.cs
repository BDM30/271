

namespace Domain.Entities
{
  public class UserProduct
  {
    public int UserProductID { get; set; }
    public int UserID { get; set; }
    public int ProductID { get; set; }
    public int CategoryID { get; set; }
    public int Amount { get; set; }
    public string ExpirationDate { get; set; }
  }
}
