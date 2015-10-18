using System.Collections.Generic;
using System.Web.Http;
using Domain.Abstract;
using Domain.Entities;
using System.Linq;

/*
Todo: сделать более рациональную Route System
*/

namespace API.Controllers
{
    public class UserController : ApiController
    {

      private ICommonRepository<User> userRepository;

      public UserController(ICommonRepository<User> users)
      {
        userRepository = users;
      }

    [Route("User/all")]
    [HttpGet]
    public IEnumerable<User> GetUsers()
    {
      return userRepository.Data;
    }

      [HttpGet]
      [Route("User/getby")]
      public IEnumerable<User> GetUserBy([FromUri]User user)
      {
        return (from x in userRepository.Data
          where (x.UserID == user.UserID && x.UserID != 0  || x.Name == user.Name && x.Name != ""
          || x.Password == user.Password && x.Password != "")
          select x);
      } 
  }
}
