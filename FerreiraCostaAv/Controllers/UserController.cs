using FerreiraCostaAv.Data;
using FerreiraCostaAv.DTO;
using FerreiraCostaAv.Services;
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

    [HttpGet]
    public IActionResult RecoverPassword()
    {
      return Ok();
    }

    [HttpPost]
    public IActionResult NewUser([FromBody] UserDTO user)
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
