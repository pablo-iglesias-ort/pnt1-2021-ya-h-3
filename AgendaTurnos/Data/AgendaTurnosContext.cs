﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgendaTurnos.Models;

namespace AgendaTurnos.Data
{
    public class AgendaTurnosContext : DbContext
    {
        public AgendaTurnosContext (DbContextOptions<AgendaTurnosContext> options)
            : base(options)
        {
        }

        public DbSet<AgendaTurnos.Models.Usuario> Usuario { get; set; }
    }
}
