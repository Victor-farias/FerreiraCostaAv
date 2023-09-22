using FerreiraCostaAv.Data;
using FerreiraCostaAv.DTO;
using FerreiraCostaAv.Interfaces;
using FerreiraCostaAv.Services;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IUserService userService;

    public UserController(ApplicationDbContext dbContext, IUserService userService)
    {
      this.dbContext = dbContext;
      this.userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] string userName, string password)
    {
      try
      {
        var loginResult = this.userService.Login(userName, password);

        return Ok(loginResult);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("recoverPassword")]
    public IActionResult RecoverPassword([FromBody] RecoverPasswordDTO recoverPasswordDTO)
    {
      try
      {
        return Ok(this.userService.RecoverPassword(recoverPasswordDTO));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("newUser")]
    public IActionResult NewUser([FromBody] UserDTO userDTO)
    {
      try
      {
        var newUserResult = this.userService.NewUser(userDTO);
        return Ok(newUserResult);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("getUsers")]
    [Authorize]
    public IActionResult GetUsers()
    {
      try
      {
        return Ok(this.userService.GetUsers());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("editUser")]
    [Authorize]
    public IActionResult Edituser([FromBody] UserDTO userDTO)
    {
      try
      {
        var editUserResult = this.userService.EditUser(userDTO);
        return Ok(editUserResult);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("deleteUser")]
    [Authorize]
    public IActionResult DeleteUser([FromBody] List<UserDTO> usersDTO)
    {
      try
      {
        var deleteResult = this.userService.DeleteUsers(usersDTO);
        return Ok(deleteResult);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}
