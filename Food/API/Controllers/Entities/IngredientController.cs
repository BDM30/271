using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

    [Route("Ingredient/all")]
    [HttpGet]
    public IEnumerable<Ingredient> GetIngredients()
    {
      return ingredientRepository.Data;
    }

    [HttpGet]
    [Route("Ingredient/getby")]
    public IEnumerable<Ingredient> GetIngredientBy([FromUri] Ingredient i)
    {
      return (from x in ingredientRepository.Data
              where (x.CategoryID == i.CategoryID && x.CategoryID != 0 ||
              x.Amount == i.Amount && x.Amount != 0 ||
              x.ImportanceLevel == i.ImportanceLevel && x.ImportanceLevel != 0 ||
              x.RecipeID == i.RecipeID && x.RecipeID != 0 ||
              x.ReplaceabilityLevel == i.ReplaceabilityLevel && x.ReplaceabilityLevel != 0 ||
              x.IngredientID == i.IngredientID && x.IngredientID != 0)
              select x);
    }
  }
}
