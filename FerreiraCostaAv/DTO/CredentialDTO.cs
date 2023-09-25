using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.DTO
{
  public class CredentialDTO
  {
    public CredentialDTO()
    {
    }

    public CredentialDTO(string login, string password)
    {
      Login = login;
      Password = password;
    }

    public string Login { get; set; }
    public string Password { get; set; }
  }
}
