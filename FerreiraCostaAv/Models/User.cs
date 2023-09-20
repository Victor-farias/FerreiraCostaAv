using FerreiraCostaAv.Enums;
using FerreiraCostaAv.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Models
{
  public class User : IUser
  {
    public User()
    {
    }

    public User(Credential credential, string email, string phoneNumber, int cpf, DateTime birthDate, string mothersName, StatusEnum status, DateTime inclusionDate, DateTime? changeDate)
    {
      Credential = credential;
      Email = email;
      PhoneNumber = phoneNumber;
      Cpf = cpf;
      BirthDate = birthDate;
      MothersName = mothersName;
      Status = status;
      InclusionDate = inclusionDate;
      ChangeDate = changeDate;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Credential Credential { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    public string MothersName { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime InclusionDate { get; set; }
    public DateTime? ChangeDate { get; set; }
  }
}
