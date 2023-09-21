using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.DTO
{
  public class RecoverPasswordDTO
  {
    public RecoverPasswordDTO(string email, string phoneNumber, int? cpf, DateTime? birthDate, string mothersName)
    {
      Email = email;
      PhoneNumber = phoneNumber;
      Cpf = cpf;
      BirthDate = birthDate;
      MothersName = mothersName;
    }

    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? Cpf { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? MothersName { get; set; }
  }
}
