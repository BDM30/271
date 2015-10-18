using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Domain.Entities;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

      [System.Web.Http.HttpGet]
      [System.Web.Mvc.Route("Test/get")]
      public string getTest(string id)
      {
        return id == "4606068035723" ?  "Гирлянда" : "nope";
      }
  }
}