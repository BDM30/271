using System.Web.Http;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
  }
}