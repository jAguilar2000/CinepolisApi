﻿using Cinepolis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.Core.Interface
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> Gets();
    }
}
