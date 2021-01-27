using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Inferastructure.IRepository;
using Shop.Models;
using System.Collections.Generic;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserAuthenticationManager userauthenticationManager;
        public UserController(IUserAuthenticationManager userAuthenticationManager)
        {
            this.userauthenticationManager = userAuthenticationManager;
        }
        [Authorize]
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenricate([FromBody] User user)
        {
            var token = userauthenticationManager.Authenticate(user.Username, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
        [Authorize(Roles = "Administrator, User")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize(Policy = "AdminAndPoweruser")]
        [HttpPost]
        public void Post([FromBody] User value)
        {

        }
    }
}
