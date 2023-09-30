using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.VehicleNS

{
    [Table("veiculos")]
    public class Vehicle
    {
        [Key]
        [Column("id_veiculo", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }

        [Column("modelo")]
        public int VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }

        [Column("ano_fabricacao")]
        public short FabricationDate { get; set; }

        [Column("hodometro")]
        public int Odometer { get; set; }

        [Column("is_alugado")]
        public bool Rented { get; set; }

        public List<UserVehicle>? UserVehicles { get; set; }

    }
}