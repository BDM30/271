using System.Web.Http;
using API.Models;

namespace API.Controllers
{
  public class TestController : ApiController
  {
    [Route("Test/ine")]
    [HttpGet]
    public NameDescription one(string id)
    {
      return id == "4606068035723" ? new NameDescription() {Description = "Супер-гирлянда", Name = "Гирлянда" } : null;
    }
  }
}
