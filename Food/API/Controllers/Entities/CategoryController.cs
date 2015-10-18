﻿using System.Web.Http;
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
  }
}
