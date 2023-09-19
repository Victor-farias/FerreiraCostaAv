using FerreiraCostaAv.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerreiraCostaAv.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) {}

    public DbSet<Credential> Credentials { get; set; }
    public DbSet<User> Users { get; set; }
  }
}
