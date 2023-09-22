using FerreiraCostaAv.Data;
using FerreiraCostaAv.DTO;
using FerreiraCostaAv.Enums;
using FerreiraCostaAv.Interfaces;
using FerreiraCostaAv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FerreiraCostaAv.Services
{
  public class UserService : IUserService
  {
    private readonly ApplicationDbContext dbContext;
    private readonly IConfiguration configuration;

    public UserService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
      this.dbContext = dbContext;
      this.configuration = configuration;
    }

    public List<User> NewUser(UserDTO userDTO)
    {
      if (!LoginAlreadyInUse(userDTO))
      {
        dbContext.Add(
            new User(
              new Credential(userDTO.Credential.Login,
              userDTO.Credential.Password),
              userDTO.Email,
              userDTO.PhoneNumber,
              userDTO.Cpf,
              userDTO.BirthDate,
              userDTO.MothersName,
              userDTO.Status,
              DateTime.Now,
              DateTime.Now));

        this.dbContext.SaveChanges();
      }

      return GetUsers();
    }

    public List<User> EditUser(UserDTO userDTO)
    {
      //Verifying if new user name ow password passed aren't already being used by another user
      if (!LoginAlreadyInUse(userDTO))
      {
        var user = this.dbContext.Users.Include(i => i.Credential).First(w => w.Id == userDTO.Id);

        user.BirthDate = userDTO.BirthDate;
        user.ChangeDate = DateTime.Now;
        user.Cpf = userDTO.Cpf;
        user.Credential.Login = userDTO.Credential.Login;
        user.Credential.Password = userDTO.Credential.Password;
        user.Email = userDTO.Email;
        user.ChangeDate = DateTime.Now;
        user.MothersName = userDTO.PhoneNumber;
        user.PhoneNumber = userDTO.PhoneNumber;
        user.Status = userDTO.Status;

        this.dbContext.SaveChanges(); 
      }

      return GetUsers();
    }

    public List<User> DeleteUsers(List<int> ids)
    {
      var usersToDelete = this.dbContext.Users.Where(w => ids.Contains(w.Id)).ToList();

      usersToDelete.ForEach(user => user.Status = StatusEnum.Inativo.ToString());
      this.dbContext.SaveChanges();

      return GetUsers();
    }

    //Since users status filters would be on the front end, sending all the users regardless of theis status 
    public List<User> GetUsers()
    {
      return this.dbContext.Users.ToList();
    }

    public List<User> Login(LoginInfoDTO loginInfoDTO)
    {
      var user = dbContext.Users
       .Include(u => u.Credential)
       .FirstOrDefault(u => u.Credential.Login == loginInfoDTO.UserName && u.Credential.Password == loginInfoDTO.Password);

      if (user == null)
      {
        throw new Exception("Nome de usuário ou senha incorretos.");
      }

      if (!user.Status.Equals(StatusEnum.Ativo.ToString()))
      {
        throw new Exception($"Usuário marcado como {user.Status} no sistema.");
      }

      return GetUsers();
    }

    public string RecoverPassword(RecoverPasswordDTO recoverPasswordDTO)
    {
      var user = this.dbContext.Users.Include(i => i.Credential).FirstOrDefault(f => f.Email.Equals(recoverPasswordDTO.Email));
      if (user != null)
      {
        if (recoverPasswordDTO.BirthDate.Equals(user.BirthDate) || recoverPasswordDTO.Cpf.Equals(user.Cpf) || recoverPasswordDTO.MothersName.Equals(user.MothersName) || recoverPasswordDTO.PhoneNumber.Equals(user.PhoneNumber))
        {
          SendPassword(user.Email, user.Credential.Password);
          return "Um email foi enviado para o seu endereço de email cadastrado contendo sua senha";
        } else
        {
          throw new Exception("Informação incorreta");
        }
      } 

      throw new Exception("Nenhum usuário cadastrado com esse email");
    }

    public void SendPassword(string email, string password)
    {
      var smtpClient = new SmtpClient("smtp.gmail.com")
      {
        Port = 587, 
        Credentials = new NetworkCredential("testevictorfc@gmail.com", "segurancaeimportante"),
        EnableSsl = true, 
      };

      var mailMessage = new MailMessage
      {
        From = new MailAddress("testevictorfc@gmail.com"),
        Subject = "Recuperação de Senha",
        Body = $"Sua senha é: {password}",
        IsBodyHtml = false, 
      };

      mailMessage.To.Add(email);

      smtpClient.Send(mailMessage);
    }

    public bool LoginAlreadyInUse(UserDTO userDTO)
    {
      var userId = userDTO.Id ?? 0;
      if (this.dbContext.Users.Include(i => i.Credential).Any(a => a.Credential.Login.Equals(userDTO.Credential.Login) && userId != a.Id)) 
      {
        throw new Exception("Nome de usuário já em uso.");
      }
      else if (this.dbContext.Users.Include(i => i.Credential).Any(a => a.Credential.Password.Equals(userDTO.Credential.Password) && userId != a.Id))
      {
        throw new Exception("Senha já em uso.");
      }

      return false;
    }

    public string GenerateJwtToken(string userName)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
          {
           new Claim(ClaimTypes.Name, userName),
           }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
