using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace BachelorOppgaveBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;
        private NpgsqlConnection conn;

        public DatabaseController(ILogger<DatabaseController> logger)
        {
            _logger = logger;
            conn = new AzurePostgres().getConn();
        }


        [HttpPost]
        public IActionResult AddUser(User newUser)
        {
            var UserID = Guid.NewGuid();
            var UserName = newUser.user_name;
            var UserRole = newUser.user_role_id;

            // Database
            var db = new AzurePostgres();

            db.AddUser(UserID, UserName, UserRole);

            return Ok("User Added");
        }

        [HttpGet]
        public IActionResult GetUsers() {
            var db = new DbUser(conn);
            var res = db.GetUsers();
            return Ok(res);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetUser(Guid id) {
            var db = new AzurePostgres();
            var res = db.GetUser(id);
            return Ok(res);
        }
    }
}
