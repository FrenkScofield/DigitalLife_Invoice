using DigitalLife_Work.Models.BLL;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLife_Work.Models.DAL
{
    public class MyContext : IdentityDbContext<AppUser>
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
