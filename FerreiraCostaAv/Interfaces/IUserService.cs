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
    void DeleteUsers(List<int> ids);
    void UpdateUser(UserDTO userDTO);
    string GenerateJwtToken(string userName);
    List<User> GetUsers();
    List<User> Login(LoginInfoDTO loginInfoDTO);
    bool LoginAlreadyInUse(UserDTO userDTO);
    void SaveUser(UserDTO userDTO);
    string RecoverPassword(RecoverPasswordDTO recoverPasswordDTO);
    void SendPassword(string email, string password);
    UserDTO GetUserById(int id);
    UserDTO ConvertToDTO(User user);
  }
}
