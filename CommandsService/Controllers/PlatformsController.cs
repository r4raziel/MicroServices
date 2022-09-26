using Microsoft.AspNetCore.Mvc;
using CommandsService.Models;

namespace CommandsService.Controllers;

[ApiController]
[Route("/api/c/[controller]")]
public class PlatformsController : ControllerBase
{
    public PlatformsController()
    {
        
    }


    [HttpPost]
    public ActionResult TestInboundConnection()
    {
        Console.WriteLine("--> Inbound POST # Command Service");
        return Ok("Inbound test of from Platfroms Controller");

    }

}
