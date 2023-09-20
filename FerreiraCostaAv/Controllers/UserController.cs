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
      try
      {
        //UserService.NewUser(user);
        return Ok();
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public IActionResult RecoverPassword()
    {
      return Ok();
    }

    [HttpPost]
    public IActionResult NewUser([FromBody] UserDTO userDTO)
    {

      try
      {
        UserService.NewUser(userDTO);
        return Ok("New user added successfully");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
      try
      {
        return Ok(UserService.GetUsers());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut]
    public IActionResult Edituser([FromBody] UserDTO userDTO)
    {
      try
      {
        UserService.EditUser(userDTO);
        return Ok("User edited successfully.");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete]
    public IActionResult DeleteUser([FromBody] List<UserDTO> usersDTO)
    {
      try
      {
        UserService.DeleteUsers(usersDTO);
        return Ok("Users deleted successfully.");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
