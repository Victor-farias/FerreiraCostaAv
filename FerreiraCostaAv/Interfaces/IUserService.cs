using FerreiraCostaAv.DTO;
using FerreiraCostaAv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Interfaces
{
  public interface IUserService
  {
    List<User> DeleteUsers(List<UserDTO> usersDTO);
    List<User> EditUser(UserDTO userDTO);
    List<User> GetUsers();
    List<User> Login(string userName, string password);
    bool LoginAlreadyExists(UserDTO userDTO);
    List<User> NewUser(UserDTO userDTO);
    string RecoverPassword(RecoverPasswordDTO recoverPasswordDTO);
    void SendPassword(string email, string password);
  }
}
