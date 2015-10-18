using System.Collections.Generic;
using System.Linq;
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

    [HttpGet]
    [Route("Product/getby")]
    public IEnumerable<Product> GetProductBy([FromUri] Product p)
    {
      return (from x in productRepository.Data
              where (x.AmountDefault == p.AmountDefault && x.AmountDefault != 0 ||
              x.CategoryID == p.CategoryID && x.CategoryID != 0 ||
              x.UnitMeasureID == p.UnitMeasureID && x.UnitMeasureID != 0 ||
              x.Name == p.Name && x.Name != "" ||
              x.ProductID == p.ProductID && x.ProductID != 0)
              select x);
    }
  }
}
