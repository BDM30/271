using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;

namespace API.Controllers.Entities
{
  public class IngredientController : ApiController
  {
    private ICommonRepository<Ingredient> ingredientRepository;

    public IngredientController(ICommonRepository<Ingredient> ingredients)
    {
      ingredientRepository = ingredients;
    }
  }
}
