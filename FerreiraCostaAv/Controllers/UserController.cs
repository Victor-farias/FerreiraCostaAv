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
  [Route("[controller]")]
  public class UserController : Controller
  {
    private readonly ApplicationDbContext dbContext;
    private readonly IUserService userService;

    public UserController(ApplicationDbContext dbContext, IUserService userService)
    {
      this.dbContext = dbContext;
      this.userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginInfoDTO loginInfoDTO)
    {
      try
      {
        var loginResult = this.userService.Login(loginInfoDTO);

        var token = this.userService.GenerateJwtToken(loginInfoDTO.UserName);

        var response = new
        {
          Login = loginResult,
          Token = token
        };

        return RedirectToAction("Users");
      }
      catch (Exception e)
      {
        TempData["ErrorMessage"] = e.Message;
        return RedirectToAction("Index", "Home");
      }
    }

    [HttpGet("recoverPassword")]
    public IActionResult RecoverPassword(RecoverPasswordDTO recoverPasswordDTO)
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

    [HttpGet("newUser")]
    public IActionResult NewUser()
    {
      try
      {
        return View();
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("saveUser")]
    public IActionResult SaveUser(UserDTO userDTO)
    {
      try
      {
        this.userService.SaveUser(userDTO);

        return RedirectToAction("Users");
      }
      catch (Exception e)
      {
        TempData["ErrorMessage"] = e.Message;
        return RedirectToAction("Users");
      }
    }

    [HttpGet("Users")]
    public IActionResult Users()
    {
      try
      {
        var users = this.userService.GetUsers();
        return View(users);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("EditUser")]
    public IActionResult Edituser(UserDTO userDTO)
    {
      try
      {
        return View();
      }
      catch (Exception e)
      {
        TempData["ErrorMessage"] = e.Message;
        return RedirectToAction("Users");
      }
    }

    [HttpPost("deleteUsers")]
    public IActionResult DeleteUsers([FromBody] List<int> ids)
    {
      try
      {
        var deleteResult = this.userService.DeleteUsers(ids);
        return RedirectToAction("Users");
      }
      catch (Exception e)
      {
        TempData["ErrorMessage"] = e.Message;
        return RedirectToAction("Users");
      }
    }
  }
}
