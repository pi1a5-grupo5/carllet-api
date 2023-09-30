using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Percurso")]
    public class Course
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public Guid Id { get; set; }

        [ForeignKey("id")]
        [Column("id_condutor")]
        public Guid UserVehicleId { get; set; }
        public UserVehicle UserVehicle { get; set; }

        [Column("distancia_percurso")]
        public float CourseLength { get; set; }

        [Column("data_fim_percurso")]
        public DateTime CourseEndTime { get; set; }

    }
}
