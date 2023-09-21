using FerreiraCostaAv.Data;
using FerreiraCostaAv.DTO;
using FerreiraCostaAv.Enums;
using FerreiraCostaAv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Services
{
  public static class UserService
  {
    private static ApplicationDbContext dbContext;

    //Dates field would be ommited on fron end
    public static List<User> NewUser(UserDTO userDTO)
    {
      var usersDTO = new List<UserDTO>();
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

        dbContext.SaveChanges(); 
      }

      return GetUsers();
    }

    public static List<User> EditUser(UserDTO userDTO)
    {
      if (!LoginAlreadyExists(userDTO))
      {
        var user = dbContext.Users.Include(i => i.Credential).First(w => w.Id == userDTO.Id);

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

        dbContext.SaveChanges(); 
      }

      return GetUsers();
    }

    public static List<User> DeleteUsers(List<UserDTO> usersDTO)
    {
      var userIds = usersDTO.Select(s => s.Id);
      var usersToDelete = dbContext.Users.Where(w => userIds.Contains(w.Id)).ToList();

      usersToDelete.ForEach(user => user.Status = StatusEnum.Inativo);
      dbContext.SaveChanges();

      return GetUsers();
    }

    //Since users status filters would be on the front end, sending all the users regardless of theis status 
    public static List<User> GetUsers()
    {
      return dbContext.Users.ToList();
    }

    public static List<User> Login(string userName, string password)
    {
      var user =  dbContext.Users.Include(i => i.Credential).First(f => f.Credential.Login == userName && f.Credential.Password == password);
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

    private static bool LoginAlreadyExists(UserDTO userDTO)
    {
      if (dbContext.Credentials.Any(a => a.Login.Equals(userDTO.Credential.Login)))
      {
        throw new Exception("Nome de usuário já em uso.");
      }
      else if (dbContext.Credentials.Any(a => a.Login.Equals(userDTO.Credential.Login)))
      {
        throw new Exception("Senha já em uso.");
      }

      return false;
    }
  }
}
