using BachelorOppgaveBackend.Model;
using Microsoft.AspNetCore.Mvc;
using BachelorOppgaveBackend.PostgreSQL;

namespace BachelorOppgaveBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class TempController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TempController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public ActionResult InitDb()
    {
        new ApplicationDbInitializer().Initialize(_context);
        return Ok("Init db");
    }

}