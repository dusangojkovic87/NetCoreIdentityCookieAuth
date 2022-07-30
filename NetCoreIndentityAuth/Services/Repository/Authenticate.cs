using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NetCoreIndentityAuth.Entities;
using NetCoreIndentityAuth.Models;
using NetCoreIndentityAuth.Services.IRepository;

namespace NetCoreIndentityAuth.Services.Repository
{
    public class Authenticate : IAuthenticate
    {
        private AplicationDbContext _context { get; set; }


        public Authenticate(AplicationDbContext context)
        {
            _context = context;


        }


        public bool isRegisterd(RegisterModel model)
        {
            var userRegistered = _context.User.FirstOrDefault(x => x.Email.Equals(model.Email));
            if (userRegistered == null)
            {
                return false;

            }

            return true;

        }

    }
}