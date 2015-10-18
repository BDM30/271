using System.Collections.Generic;
using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;

namespace API.Controllers.Entities
{
  public class RecipeController : ApiController
  {
    private ICommonRepository<Recipe> recipeRepository;

    public RecipeController(ICommonRepository<Recipe> recipes)
    {
      recipeRepository = recipes;
    }

    [Route("Recipe/all")]
    [HttpGet]
    public IEnumerable<Recipe> GetRecipes()
    {
      return recipeRepository.Data;
    }
  }
}
