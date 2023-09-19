using FerreiraCostaAv.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Models
{
  public class Credential : ICredential

  {
    public Credential()
    {
    }

    public Credential(string login, string password)
    {
      Login = login;
      Password = password;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
  }
}
