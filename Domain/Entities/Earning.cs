using MiNET.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Earning
    {
        public Guid EarningId { get; set; }
        public Guid DriverId { get; set; }
    }
}
