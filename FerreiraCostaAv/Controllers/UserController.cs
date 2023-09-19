using FerreiraCostaAv.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly ApplicationDbContext dbContext;

    public UserController(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult Login()
    {
      return Ok();
    }

    [HttpPost]
    public IActionResult NewUser()
    {
      return Ok();
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
      return Ok();
    }

    [HttpPut]
    public IActionResult Edituser()
    {
      return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteUser()
    {
      return Ok();
    }
  }
}
