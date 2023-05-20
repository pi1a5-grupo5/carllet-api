using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class CarlletDbContext : DbContext
    {
        public CarlletDbContext (DbContextOptions<CarlletDbContext> options): base(options)
        {}

        public DbSet<User> User { get; set; }
    }
}
