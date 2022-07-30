using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreIndentityAuth.Entities;
using NetCoreIndentityAuth.Models;

namespace NetCoreIndentityAuth.Services.IRepository
{
    public interface IAuthenticate
    {
        bool isRegisterd(RegisterModel model);


    }
}