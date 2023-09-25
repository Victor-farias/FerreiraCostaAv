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

    public UserDTO(int? id, CredentialDTO credential, string email, string phoneNumber, long cpf, DateTime birthDate, string mothersName, string status)
    {
      Id = id;
      Credential = credential;
      Email = email;
      PhoneNumber = phoneNumber;
      Cpf = cpf;
      BirthDate = birthDate;
      MothersName = mothersName;
      Status = status;
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
