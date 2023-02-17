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

        [HttpPost]
        public ActionResult AddUser(User newUser)
        {
            var UserID = Guid.NewGuid();
            var UserName = newUser.user_name;
            var UserRole = newUser.user_role_id;

            // Database
            var db = new AzurePostgres();

            db.AddUser(UserID, UserName, UserRole);

            return RedirectToAction("Index");
        }

    }
}
