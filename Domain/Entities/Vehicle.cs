using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities

{
    [Table("veiculos")]
    public class Vehicle
    {
        [Key]
        [Column("id_veiculo", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("marca")]
        public string Brand { get; set; }

        [Column("modelo")]
        public string Model { get; set; }

        [Column("ano_fabricacao")]
        public short FabricationDate { get; set; }

        [Column("hodometro")]
        public int Odometer { get; set; }

        [Column("is_alugado")]
        public bool rented { get; set; }

        // public List<Course>? Courses { get; set; }  =  new List<Course>();

    }
}