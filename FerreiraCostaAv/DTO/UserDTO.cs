using FerreiraCostaAv.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.DTO
{
  public class UserDTO
  {
    public UserDTO()
    {
    }

    public int? Id { get; set; }
    public CredentialDTO Credential { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public long Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public string MothersName { get; set; }
    public string Status { get; set; }
  }
}
