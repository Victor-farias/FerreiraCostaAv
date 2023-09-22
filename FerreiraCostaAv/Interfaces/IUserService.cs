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
    List<User> DeleteUsers(List<int> ids);
    List<User> EditUser(UserDTO userDTO);
    string GenerateJwtToken(string userName);
    List<User> GetUsers();
    List<User> Login(LoginInfoDTO loginInfoDTO);
    bool LoginAlreadyInUse(UserDTO userDTO);
    List<User> NewUser(UserDTO userDTO);
    string RecoverPassword(RecoverPasswordDTO recoverPasswordDTO);
    void SendPassword(string email, string password);
  }
}
