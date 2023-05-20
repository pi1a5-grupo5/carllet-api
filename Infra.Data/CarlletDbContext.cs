using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data
{
    public class CarlletDbContext : DbContext
    {
        public CarlletDbContext (DbContextOptions<CarlletDbContext> options): base(options)
        {}
    }
}
