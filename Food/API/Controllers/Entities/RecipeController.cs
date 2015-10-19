﻿using System.Collections.Generic;
using System.Linq;
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

    [HttpGet]
    [Route("Recipe/getby")]
    public IEnumerable<Recipe> GetRecipeBy([FromUri] Recipe r)
    {
      return (from x in recipeRepository.Data
              where (x.Name == r.Name && x.Name != "" || x.ProcessDescription != r.ProcessDescription && x.ProcessDescription != ""
              || x.RecipeID != r.RecipeID && x.RecipeID != 0)
              select x);
    }

    // если id валидный - то редактирование, иначе создастся новый
    [HttpGet]
    [Route("Recipe/save")]
    public string SaveRecipe([FromUri] Recipe r)
    {
      recipeRepository.SaveData(r);
      return "ok";
    }
  }
}
