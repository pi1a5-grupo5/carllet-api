using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleNS
{
    [Table("marca")]
    public class VehicleBrand
    {
        [Key]
        [Column("id_marca", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleBrandId { get; set; }

        public string Name { get; set; }

        public List<VehicleType> VehicleTypes { get; set; }
    }
}
