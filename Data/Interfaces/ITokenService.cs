﻿using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserAplication usuario);
    }
}
