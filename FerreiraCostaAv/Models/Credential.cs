using FerreiraCostaAv.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Models
{
  public class Credential : ICredential

  {
    public Credential(string login, string password)
    {
      Login = login;
      Password = password;
    }

    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
  }
}
