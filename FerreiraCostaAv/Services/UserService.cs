using FerreiraCostaAv.Data;
using FerreiraCostaAv.DTO;
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

    private static void NewUser(UserDTO userDTO)
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
          null));
      
      dbContext.SaveChanges();
    }

    private static void EditUser(UserDTO userDTO)
    {
      var user = dbContext.Users.Include(i => i.Credential).First(w => w.Id == userDTO.Id);

      user.BirthDate = userDTO.BirthDate;
      user.ChangeDate = DateTime.Now;
      user.Cpf = userDTO.Cpf;
      user.Credential.Login = userDTO.Credential.Login;
      user.Credential.Password = userDTO.Credential.Password;
      user.Email = userDTO.Email;
      user.InclusionDate = userDTO.InclusionDate;
      user.MothersName = userDTO.PhoneNumber;
      user.PhoneNumber = userDTO.PhoneNumber;
      user.Status = userDTO.Status;

      dbContext.SaveChanges();
    }

    private static void DeleteUsers(List<UserDTO> usersDTO)
    {
      var userIds = usersDTO.Select(s => s.Id);
      var usersToDelete = dbContext.Users.Where(w => userIds.Contains(w.Id));

      dbContext.Users.RemoveRange(usersToDelete);
      dbContext.SaveChanges();
    }

    private static List<User> GetUsers()
    {
      return dbContext.Users.ToList();
    }
  }
}
