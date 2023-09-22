using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.DTO
{
  public class RecoverPasswordDTO
  {
    public RecoverPasswordDTO()
    {
    }

    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public long? Cpf { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? MothersName { get; set; }
  }
}
