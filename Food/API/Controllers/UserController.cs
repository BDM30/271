using System.Linq;
using System.Web.Http;
using System.Web.WebPages;
using Domain.Abstract;
using Domain.Entities;

namespace API.Controllers
{
    public class UserController : ApiController
    {

      private ICommonRepository<User> userRepository;

      public UserController(ICommonRepository<User> users)
      {
        userRepository = users;
      }

      [Route("test")]
      [HttpGet]
      public User Test()
      {
        User x = new User() {Name = "Vlad", Password = "Pass"};
        userRepository.SaveData(x);
        User y = userRepository.Data.FirstOrDefault();
        return y;
      }

  }
}
