﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;

namespace API.Controllers.Entities
{
  public class CategoryController : ApiController
  {
    private ICommonRepository<Category> categoryRepository;

    public CategoryController(ICommonRepository<Category> categories)
    {
      categoryRepository = categories;
    }

    [Route("Category/all")]
    [HttpGet]
    public IEnumerable<Category> GetCategories()
    {
      return categoryRepository.Data;
    }

    [HttpGet]
    [Route("Category/getby")]
    public IEnumerable<Category> GetCategoryBy([FromUri] Category c)
    {
      return (from x in categoryRepository.Data
              where (x.CategoryID == c.CategoryID && x.CategoryID != 0 ||
              x.Name == c.Name && x.Name != "")
              select x);
    }
  }
}