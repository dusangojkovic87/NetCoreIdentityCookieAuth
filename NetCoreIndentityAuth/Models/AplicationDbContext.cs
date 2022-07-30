using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreIndentityAuth.Entities;

namespace NetCoreIndentityAuth.Models
{
    public class AplicationDbContext : IdentityDbContext<User, UserRole, int>
    {

        public AplicationDbContext()
        {

        }


        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=Dusan; Database=AuthTest; Trusted_Connection=True;");
        }

        public DbSet<User> User { get; set; }



    }



}