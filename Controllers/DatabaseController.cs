using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BachelorOppgaveBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(ILogger<DatabaseController> logger)
        {
            _logger = logger;
        }

        public ActionResult AddUser(User newUser)
        {
            var RoleId = newUser.UserRoleId;
            var UserName = newUser.UserName;

            // Database



            return RedirectToAction("Index");
        }

    }
}
