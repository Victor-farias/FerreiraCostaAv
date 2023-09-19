using FerreiraCostaAv.Enums;
using FerreiraCostaAv.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Models
{
  public class User : IUser
  {
    public User(Credential credencials, string email, string phoneNumber, int cpf, DateTime birthDate, string mothersName, StatusEnum status, DateTime inclusionDate, DateTime changeDate)
    {
      Credencials = credencials;
      Email = email;
      PhoneNumber = phoneNumber;
      Cpf = cpf;
      BirthDate = birthDate;
      MothersName = mothersName;
      Status = status;
      InclusionDate = inclusionDate;
      ChangeDate = changeDate;
    }

    public int Id { get; set; }
    public Credential  Credencials { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public string MothersName { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime InclusionDate { get; set; }
    public DateTime ChangeDate { get; set; }
  }
}
