using System.Collections.Generic;
using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;

namespace API.Controllers.Entities
{
  public class UserProductController : ApiController
  {
    private ICommonRepository<UserProduct> userProductRepository;

    public UserProductController(ICommonRepository<UserProduct> userProducts)
    {
      userProductRepository = userProducts;
    }

    [Route("UserProduct/all")]
    [HttpGet]
    public IEnumerable<UserProduct> GetUserProducts()
    {
      return userProductRepository.Data;
    }
  }
}