using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Percurso")]
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("id")]
        [Column("id_condutor")]
        public int? OwnerId { get; set; }

        public User? Owner { get; set; }

        [ForeignKey("id")]
        [Column("id_veiculo")]
        public int VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }

        [Column("tipo")]
        public char type { get; set; }

        [Column("distancia_percurso")]
        public int CourseLength { get; set; }

        [Column("data_inicio_percurso")]
        public DateTime CourseStartTime { get; set; }

        [Column("data_fim_percurso")]
        public DateTime CourseEndTime { get; set; }

    }
}
