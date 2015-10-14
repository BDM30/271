using System.Web.Http;
using System.Web.WebPages;

namespace API.Controllers
{
    public class UserController : ApiController
    {

    [HttpGet]
    public string Test (int x)
    {
      return x.ToString();
    }

  }
}
