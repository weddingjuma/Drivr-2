using API.Services;
using ClassLibrary.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET api/user
        [HttpGet]
        [Authorize]
        [CustomExceptionFilter]
        public User Get()
        {
            return new User
            {
                UserName = User.Identity.Name,
                Role = User.Identity.AuthenticationType
            };
        }
    }
}