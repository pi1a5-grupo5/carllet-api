using Domain.Entities.Budget;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.VehicleNS
{
    [Table("modelo")]
    public class VehicleType 
    {
        [Key]
        [Column("id_modelo", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleTypeId { get; set; }

        public string Name { get; set; }

        [Column("id_marca")]
        public int VehicleBrandId { get; set; }

        public VehicleBrand VehicleBrand { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
    }
}
