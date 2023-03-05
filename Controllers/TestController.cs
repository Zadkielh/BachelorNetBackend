using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;


namespace BachelorOppgaveBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private NpgsqlConnection conn;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
            conn = new AzurePostgres().getConn();
        }

          [HttpPost]
          
        public IActionResult test([FromBody] string body)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(body);
            Console.Out.WriteLine(body);
            return Ok();

        }

    }

}
