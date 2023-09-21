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

    public UserService(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public List<User> NewUser(UserDTO userDTO)
    {
      if (!LoginAlreadyExists(userDTO))
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
      if (!LoginAlreadyExists(userDTO))
      {
        var user = this.dbContext.Users.Include(i => i.Credential).First(w => w.Id == userDTO.Id);

        user.BirthDate = userDTO.BirthDate;
        user.ChangeDate = DateTime.Now;
        user.Cpf = userDTO.Cpf;
        user.Credential.Login = userDTO.Credential.Login;
        user.Credential.Password = userDTO.Credential.Password;
        user.Email = userDTO.Email;
        user.InclusionDate = userDTO.InclusionDate;
        user.ChangeDate = DateTime.Now;
        user.MothersName = userDTO.PhoneNumber;
        user.PhoneNumber = userDTO.PhoneNumber;
        user.Status = userDTO.Status;

        this.dbContext.SaveChanges(); 
      }

      return GetUsers();
    }

    public List<User> DeleteUsers(List<UserDTO> usersDTO)
    {
      var userIds = usersDTO.Select(s => s.Id);
      var usersToDelete = this.dbContext.Users.Where(w => userIds.Contains(w.Id)).ToList();

      usersToDelete.ForEach(user => user.Status = StatusEnum.Inativo);
      this.dbContext.SaveChanges();

      return GetUsers();
    }

    //Since users status filters would be on the front end, sending all the users regardless of theis status 
    public List<User> GetUsers()
    {
      return this.dbContext.Users.ToList();
    }

    public List<User> Login(string userName, string password)
    {
      var user = this.dbContext.Users.Include(i => i.Credential).First(f => f.Credential.Login == userName && f.Credential.Password == password);
      var isActive = user.Status.Equals(StatusEnum.Ativo);
      if (user != null && isActive)
      {
        return GetUsers();
      } else if (user != null && !isActive)
      {
        throw new Exception($"Usuário marcado como {user.Status} no sistema.");
      } 
      throw new Exception("Nome de usuário ou senha incorretos.");
    }

    public string RecoverPassword(RecoverPasswordDTO recoverPasswordDTO)
    {
      var user = this.dbContext.Users.Include(i => i.Credential).First(f => f.Email.Equals(recoverPasswordDTO.Email));
      if (user != null)
      {
        if (recoverPasswordDTO.BirthDate.Equals(user.BirthDate) || recoverPasswordDTO.Cpf.Equals(user.Cpf) || recoverPasswordDTO.MothersName.Equals(user.MothersName) || recoverPasswordDTO.PhoneNumber.Equals(user.PhoneNumber))
        {
          SendPassword(user.Email, user.Credential.Password);
          return "Um email foi enviado para o seu endereço de email cadastrado contendo sua senha";
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

    public bool LoginAlreadyExists(UserDTO userDTO)
    {
      if (this.dbContext.Credentials.Any(a => a.Login.Equals(userDTO.Credential.Login)))
      {
        throw new Exception("Nome de usuário já em uso.");
      }
      else if (this.dbContext.Credentials.Any(a => a.Login.Equals(userDTO.Credential.Login)))
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
