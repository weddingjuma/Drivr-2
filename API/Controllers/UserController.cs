using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Services;
using ClassLibrary.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager )
        {
            _userManager = userManager;
        }
        // GET api/user
        [HttpGet]
        [Authorize]
        [CustomExceptionFilter]
        public async Task<User> Get()
        {
            var userName = _userManager.GetUserId(User);
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);
            return new User
            {
                UserName = userName,
                Role = roles.FirstOrDefault()
            };
        }
    }
}