using System.Collections.Generic;
using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;

namespace API.Controllers.Entities
{
  public class ProductController : ApiController
  {
    private ICommonRepository<Product> productRepository;

    public ProductController(ICommonRepository<Product> products)
    {
      productRepository = products;
    }

    [Route("Product/all")]
    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
      return productRepository.Data;
    }
  }
}
