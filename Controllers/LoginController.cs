using log4net;
using log4net.Plugin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using TaskManagementService.Interface;
using TaskManagementService.Models.LoginManagement;
using TaskManagementService.Models.UserManagement;

namespace TaskManagementService.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginController(ILogin login, IUserManagement userManagement)
        {
            _login = login;
        }

        [ProducesResponseType(typeof(UsersInfo), 200)]
        [Route("Authentication")]
        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] AuthenticateUser authenticateUser)
        {
            UsersInfo usersInfo = await _login.Authentication(authenticateUser);
            return Ok(usersInfo);
        }
    }
}